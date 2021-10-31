//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Linq;

namespace GTA
{
	public abstract class Entity : PoolObject, ISpatial
	{
		#region Fields
		EntityBoneCollection _bones;
		EntityDamageRecordCollection _damageRecords;
		#endregion

		internal Entity(int handle) : base(handle)
		{
		}

		private enum EntityTypeInternal
		{
			Building = 1,
			Vehicle = 3,
			Ped = 4,
			Object = 5
		}

		/// <summary>
		/// Creates a new instance of an <see cref="Entity"/> from the given handle.
		/// </summary>
		/// <param name="handle">The entity handle.</param>
		/// <returns>Returns a <see cref="Ped"/> if this handle corresponds to a Ped.
		/// Returns a <see cref="Vehicle"/> if this handle corresponds to a Vehicle.
		/// Returns a <see cref="Prop"/> if this handle corresponds to a Prop.
		/// Returns <see langword="null" /> if no <see cref="Entity"/> exists this the specified <paramref name="handle"/></returns>
		public static Entity FromHandle(int handle)
		{
			var address = SHVDN.NativeMemory.GetEntityAddress(handle);
			if (address == IntPtr.Zero)
			{
				return null;
			}

			// Read the same field as GET_ENTITY_TYPE does
			var entityType = (EntityTypeInternal)SHVDN.NativeMemory.ReadByte(address + 0x28);

			switch (entityType)
			{
				case EntityTypeInternal.Ped:
					return new Ped(handle);
				case EntityTypeInternal.Vehicle:
					return new Vehicle(handle);
				case EntityTypeInternal.Object:
					return new Prop(handle);
			}

			return null;
		}

		/// <summary>
		/// Gets the memory address where the <see cref="Entity"/> is stored in memory.
		/// </summary>
		public IntPtr MemoryAddress => SHVDN.NativeMemory.GetEntityAddress(Handle);

		/// <summary>
		/// Gets the type of the current <see cref="Entity"/>.
		/// </summary>
		public EntityType EntityType
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return EntityType.Invalid;
				}

				// Read the same field as GET_ENTITY_TYPE does
				var entityType = (EntityTypeInternal)SHVDN.NativeMemory.ReadByte(address + 0x28);

				switch (entityType)
				{
					case EntityTypeInternal.Ped:
						return EntityType.Ped;
					case EntityTypeInternal.Vehicle:
						return EntityType.Vehicle;
					case EntityTypeInternal.Object:
						return EntityType.Prop;
				}

				return EntityType.Invalid;
			}
		}

		/// <summary>
		/// Gets or sets the population type of the current <see cref="Entity"/>.
		/// This property can also be used to add or remove <see cref="Entity"/> persistence.
		/// </summary>
		public EntityPopulationType PopulationType
		{
			get => (EntityPopulationType)Function.Call<int>(Hash.GET_ENTITY_POPULATION_TYPE, Handle);
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return;
				}

				SHVDN.NativeMemory.WriteByte(address + 0xDA, (byte)((int)value & 0xF));
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Entity"/> is dead or does not exist.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if this <see cref="Entity"/> is dead or does not exist; otherwise, <see langword="false" />.
		/// </value>
		/// <seealso cref="Exists"/>
		/// <seealso cref="Ped.IsInjured"/>
		public bool IsDead => Function.Call<bool>(Hash.IS_ENTITY_DEAD, Handle);
		/// <summary>
		/// Gets a value indicating whether this <see cref="Entity"/> exists and is alive.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if this <see cref="Entity"/> exists and is alive; otherwise, <see langword="false" />.
		/// </value>
		public bool IsAlive => !IsDead;

		#region Styling

		/// <summary>
		/// Gets the model of the current <see cref="Entity"/>.
		/// </summary>
		public Model Model => new Model(Function.Call<int>(Hash.GET_ENTITY_MODEL, Handle));

		/// <summary>
		/// Gets or sets how opaque this <see cref="Entity"/> is.
		/// </summary>
		/// <value>
		/// 0 for completely see through, 255 for fully opaque
		/// </value>
		public int Opacity
		{
			get => Function.Call<int>(Hash.GET_ENTITY_ALPHA, Handle);
			set => Function.Call(Hash.SET_ENTITY_ALPHA, Handle, value, false);
		}

		/// <summary>
		/// Resets the <seealso cref="Opacity"/>.
		/// </summary>
		public void ResetOpacity()
		{
			Function.Call(Hash.RESET_ENTITY_ALPHA, Handle);
		}

		#endregion

		#region Configuration

		/// <summary>
		/// Gets or sets the level of detail distance of this <see cref="Entity"/>.
		/// </summary>
		public int LodDistance
		{
			get => Function.Call<int>(Hash.GET_ENTITY_LOD_DIST, Handle);
			set => Function.Call(Hash.SET_ENTITY_LOD_DIST, Handle, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Entity"/> is persistent.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this <see cref="Entity"/> is persistent; otherwise, <see langword="false" />.
		/// </value>
		/// <remarks>
		/// If this <see cref="Entity"/> is <see cref="Ped"/>, setting to <see langword="true" /> can clear ambient tasks and setting to <see langword="false" /> will clear all tasks immediately.
		/// Use <see cref="Ped.SetIsPersistentNoClearTask(bool)"/> instead if you need to keep assigned tasks.
		/// </remarks>
		public bool IsPersistent
		{
			get => Function.Call<bool>(Hash.IS_ENTITY_A_MISSION_ENTITY, Handle);
			set
			{
				if (value)
				{
					Function.Call(Hash.SET_ENTITY_AS_MISSION_ENTITY, Handle, true, true);
				}
				else
				{
					MarkAsNoLongerNeeded();
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Entity"/> is frozen.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this <see cref="Entity"/> is position frozen; otherwise, <see langword="false" />.
		/// </value>
		public bool IsPositionFrozen
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return false;
				}

				return SHVDN.NativeMemory.IsBitSet(address + 0x2E, 1);
			}
			set => Function.Call(Hash.FREEZE_ENTITY_POSITION, Handle, value);
		}

		/// <summary>
		/// Gets a collection of the <see cref="EntityBone"/>s in this <see cref="Entity"/>.
		/// </summary>
		public virtual EntityBoneCollection Bones => _bones ?? (_bones = new EntityBoneCollection(this));

		#endregion

		#region Health

		/// <summary>
		/// Gets or sets the health of this <see cref="Entity"/> as an <see cref="int"/>.
		/// <para>Use <see cref="HealthFloat"/> instead if you need to get or set the value precisely, since a health value of a <see cref="Entity"/> are stored as a <see cref="float"/>.</para>
		/// </summary>
		/// <value>
		/// The health as an <see cref="int"/>.
		/// </value>
		/// <seealso cref="HealthFloat"/>
		public int Health
		{
			get => Function.Call<int>(Hash.GET_ENTITY_HEALTH, Handle);
			set => Function.Call(Hash.SET_ENTITY_HEALTH, Handle, value);
		}
		/// <summary>
		/// Gets or sets the maximum health of this <see cref="Entity"/> as an <see cref="int"/>.
		/// <para>Use <see cref="MaxHealthFloat"/> instead if you need to get or set the value precisely, since a max health value of a <see cref="Entity"/> are stored as a <see cref="float"/>.</para>
		/// </summary>
		/// <value>
		/// The maximum health as a <see cref="int"/>.
		/// </value>
		public virtual int MaxHealth
		{
			get => Function.Call<int>(Hash.GET_ENTITY_MAX_HEALTH, Handle);
			set => Function.Call(Hash.SET_ENTITY_MAX_HEALTH, Handle, value);
		}

		/// <summary>
		/// Gets or sets the health of this <see cref="Entity"/> as a <see cref="float"/>.
		/// </summary>
		/// <value>
		/// The health as a <see cref="float"/>.
		/// </value>
		public float HealthFloat
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return 0.0f;
				}

				return SHVDN.NativeMemory.ReadFloat(address + 640);
			}
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return;
				}

				SHVDN.NativeMemory.WriteFloat(address + 640, value);
			}
		}
		/// <summary>
		/// Gets or sets the maximum health of this <see cref="Entity"/> as a <see cref="float"/>.
		/// </summary>
		/// <value>
		/// The maximum health as a <see cref="float"/>.
		/// </value>
		public float MaxHealthFloat
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.EntityMaxHealthOffset == 0)
				{
					return 0.0f;
				}

				return SHVDN.NativeMemory.ReadFloat(address + SHVDN.NativeMemory.EntityMaxHealthOffset);
			}
			set
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero || SHVDN.NativeMemory.EntityMaxHealthOffset == 0)
				{
					return;
				}

				SHVDN.NativeMemory.WriteFloat(address + SHVDN.NativeMemory.EntityMaxHealthOffset, value);
			}
		}

		#endregion

		#region Positioning

		/// <summary>
		/// Gets this <see cref="Entity"/>s matrix which stores position and rotation information.
		/// </summary>
		public Matrix Matrix
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return new Matrix();
				}

				return new Matrix(SHVDN.NativeMemory.ReadMatrix(address + 96));
			}
		}

		/// <summary>
		/// Gets or sets the position of this <see cref="Entity"/>.
		/// If the <see cref="Entity"/> is <see cref="Ped"/> and the <see cref="Ped"/> is in a <see cref="Vehicle"/>, the <see cref="Vehicle"/>'s position will be returned or changed.
		/// </summary>
		/// <value>
		/// The position in world space.
		/// </value>
		public virtual Vector3 Position
		{
			get => Function.Call<Vector3>(Hash.GET_ENTITY_COORDS, Handle, 0);
			set => Function.Call(Hash.SET_ENTITY_COORDS, Handle, value.X, value.Y, value.Z, 0, 0, 0, 1);
		}

		/// <summary>
		/// Sets the position of this <see cref="Entity"/> without any offset.
		/// </summary>
		/// <value>
		/// The position in world space.
		/// </value>
		public Vector3 PositionNoOffset
		{
			set => Function.Call(Hash.SET_ENTITY_COORDS_NO_OFFSET, Handle, value.X, value.Y, value.Z, 1, 1, 1);
		}

		/// <summary>
		/// Gets or sets the rotation of this <see cref="Entity"/>.
		/// </summary>
		/// <value>
		/// The yaw, pitch, roll rotation values.
		/// </value>
		public virtual Vector3 Rotation
		{
			get => Function.Call<Vector3>(Hash.GET_ENTITY_ROTATION, Handle, 2);
			set => Function.Call(Hash.SET_ENTITY_ROTATION, Handle, value.X, value.Y, value.Z, 2, 1);
		}

		/// <summary>
		/// Gets or sets the heading of this <see cref="Entity"/>.
		/// </summary>
		/// <value>
		/// The heading in degrees.
		/// </value>
		public float Heading
		{
			get => Function.Call<float>(Hash.GET_ENTITY_HEADING, Handle);
			set => Function.Call<float>(Hash.SET_ENTITY_HEADING, Handle, value);
		}

		/// <summary>
		/// Gets a value indicating how submersed this <see cref="Entity"/> is, 1.0f means the whole entity is submerged.
		/// </summary>
		public float SubmersionLevel => Function.Call<float>(Hash.GET_ENTITY_SUBMERGED_LEVEL, Handle);

		/// <summary>
		/// Gets how high above ground this <see cref="Entity"/> is.
		/