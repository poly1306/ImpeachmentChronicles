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
			