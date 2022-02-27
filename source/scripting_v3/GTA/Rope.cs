//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;

namespace GTA
{
	public sealed class Rope : PoolObject
	{
		public Rope(int handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets the number of vertices of this <see cref="Rope"/>.
		/// </summary>
		public int VertexCount => Function.Call<int>(Hash.GET_ROPE_VERTEX_COUNT, Handle);

		/// <summary>
		/// Gets or sets the length of this <see cref="Rope"/>.
		/// </summary>
		public float Length
		{
			get => Function.Call<float>(Hash.ROPE_GET_DISTANCE_BETWEEN_ENDS, Handle);
			set => Function.Call(Hash.ROPE_FORCE_LENGTH, Handle, value);
		}

		/// <summary>
		/// Activates physics interactions for this <see cref="Rope"/>.
		/// </summary>
		public void ActivatePhysics()
		{
			Function.Call(Hash.ACTIVATE_PHYSICS, Handle);
		}

		/// <summary>
		/// Attaches a single <see cref="Entity"/> to this <see cref="Rope"/>.
		/// </summary>
		/// <param name="entity">The entity to attach.</param>
		public void Attach(Entity entity)
		{
			Attach(entity, Vector3.Zero);
		}
		/// <summary>
		/// Attaches a single <see cref="Entity"/> to this <see cref="Rope"/> at the specified <paramref name="position"/>.
		/// </summary>
		/// <param name="entity">The entity to attach.</param>
		/// <param name="position">The position in world coordinates to attach to.</param>
		public void Attach(Entity entity, Vector3 position)
		{
			Function.Call(Hash.ATTACH_ROPE_TO_ENTITY, Handle, entity.Handle, position.X, position.Y, position.Z, 0);
		}
		/// <summary>
		/// Detaches a single <see cref="Entity"/> from this <see cref="Rope"/>.
		/// </summary>
		/// <param name="entity">The entity to detach.</param>
		public void Detach(Entity entity)
		{
			Function.Call(Hash.DETACH_ROPE_FROM_ENTITY, Handle, entity.Handle);
		}

		/// <summary>
		/// Connects two <see cref="Entity"/>s with this <see cref="Rope"/>.
		/// </summary>
		/// <param name="entity1">The first entity to attach.</param>
		/// <param name="entity2">The second entity to attach.</param>
		/// <param name="length">The rope length.</param>
		public void Connect(Entity entity1, Entity entity2, float length)
		{
			Connect(entity1, Vector3.Zero, entity2, Vector3.Zero, length);
		}
		/// <summary>
		/// Connects two <see cref="Entity"/>s with this <see cref="Rope"/>