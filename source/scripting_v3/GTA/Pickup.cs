//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;

namespace GTA
{
	public sealed class Pickup : PoolObject
	{
		public Pickup(int handle) : base(handle)
		{
		}

		/// <summary>
		/// The position of this <see cref="Pickup"/>.
		/// </summary>
		public Vector3 Position => Function.Call<Vector3>(Hash.GET_PICKUP_COORDS, Handle);

		/// <summary>
		/// Gets if this <see cref="Pickup"/> has been collected.
		/// </summary>
		public bool IsCollected => Function.Call<bool>(Hash.HAS_PICKUP_BEEN_COLLECTED, Handle);

		/// <summary>
		/// Determ