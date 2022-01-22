//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;

namespace GTA
{
	/// <summary>
	/// The apply force type.
	/// </summary>
	public enum ForceType
	{
		/// <summary>
		/// Add a continuous internal force to the entity.
		/// Force itself cannot detach any fragment parts of props like <see cref="ExternalForce"/> can.
		/// </summary>
		InternalForce,
		/// <summary>
		/// Add an instant internal impulse to the entity.
		/// Impulse itself cannot detach any fragment parts of props like <see cref="ExternalImpulse"/> can.
		/// </summary>
		InternalImpulse,
		/// <summary>
		/// Add a continuous external force to the entity.
		/// Unlike <s