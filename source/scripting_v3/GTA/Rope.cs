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
		/// Connects two <see cref="Entity"/>s with this <see cref="Rope"/> at the specified positions.
		/// </summary>
		/// <param name="entity1">The first entity to attach.</param>
		/// <param name="entity2">The second entity to attach.</param>
		/// <param name="position1">The position in world coordinates to attach the first entity to.</param>
		/// <param name="position2">The position in world coordinates to attach the second entity to.</param>
		/// <param name="length">The rope length.</param>
		public void Connect(Entity entity1, Vector3 position1, Entity entity2, Vector3 position2, float length)
		{
			Function.Call(Hash.ATTACH_ENTITIES_TO_ROPE, Handle, entity1.Handle, entity2.Handle, position1.X, position1.Y, position1.Z, position2.X, position2.Y, position2.Z, length, 0, 0, 0, 0);
		}

		/// <summary>
		/// Pin a vertex of this <see cref="Rope"/> to a <paramref name="position"/>.
		/// </summary>
		/// <param name="vertex">The index of the vertex.</param>
		/// <param name="position">The position in world coordinates to pin to.</param>
		public void PinVertex(int vertex, Vector3 position)
		{
			Function.Call(Hash.PIN_ROPE_VERTEX, Handle, vertex, position.X, position.Y, position.Z);
		}
		/// <summary>
		/// Unpin a vertex of this <see cref="Rope"/>.
		/// </summary>
		/// <param name="vertex">The index of the vertex.</param>
		public void UnpinVertex(int vertex)
		{
			Function.Call(Hash.UNPIN_ROPE_VERTEX, Handle, vertex);
		}

		/// <summary>
		/// Gets the world coordinates of a single vertex of this <see cref="Rope"/>.
		/// </summary>
		/// <param name="vertex">The index of the vertex.</param>
		/// <returns>The position of the vertex in world coordinates.</returns>
		public Vector3 GetVertexCoord(int vertex)
		{
			return Function.Call<Vector3>(Hash.GET_ROPE_VERTEX_COORD, Handle, vertex);
		}

		/// <summary>
		/// Destroys this <see cref="Rope"/>.
		/// </summary>
		public override void Delete()
		{
			int handle = Handle;
			unsafe
			{
				Function.Call(Hash.DELETE_ROPE, &handle);
			}
			Handle = handle;
		}

		/// <summary>
		/// Determines if this <see cref="Rope"/> exists.
		/// </summary>
		/// <returns><see langword="true" /> if this <see cref="Rope"/> exi