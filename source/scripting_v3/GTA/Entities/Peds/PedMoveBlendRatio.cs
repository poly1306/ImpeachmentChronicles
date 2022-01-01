//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Native;
using System;

namespace GTA
{
	/// <summary>
	/// Represents a move blend ratio for peds.
	/// Between <c>0f</c> to <c>3f</c>, even subtle difference makes some difference at how peds will move.
	/// </summary>
	public readonly struct PedMoveBlendRatio : IEquatable<PedMoveBlendRatio>
	{
		public PedMoveBlendRatio(float value)
		{
			if (value < 0)
			{
				throw new ArgumentException("The value should be positive.", "value");
			}

			Value = value;
		}

		public float Value { get; }

		/// <summary>
		/// Returns the same struct as <c>new PedMoveBlendRatio(0.0f)</c>.
		/// </summary>
		public static PedMoveBlendRatio Still => new PedMoveBlendRatio(0.0f);
		/// <summary>
		/// Returns the same struct as <c>new PedMoveBlendRatio(1.0f)</c>.
		/// </summary>
		public static PedMoveBlendRatio Walk => ne