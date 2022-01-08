﻿//
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
		public static PedMoveBlendRatio Walk => new PedMoveBlendRatio(1.0f);
		/// <summary>
		/// Returns the same struct as <c>new PedMoveBlendRatio(2.0f)</c>.
		/// </summary>
		public static PedMoveBlendRatio Run => new PedMoveBlendRatio(2.0f);
		/// <summary>
		/// Returns the same struct as <c>new PedMoveBlendRatio(3.0f)</c>.
		/// </summary>
		public static PedMoveBlendRatio Sprint => new PedMoveBlendRatio(3.0f);

		public static implicit operator PedMoveBlendRatio(float value) => new PedMoveBlendRatio(value);
		public static explicit operator float(PedMoveBlendRatio value) => value.Value;
		public static implicit operator InputArgument(PedMoveBlendRatio value)
		{
			return new InputArgument(value.Value);
		}

		/// <summary>
		/// Tests for equality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><see langword="true" /> if <paramref name="left"/> has the same value as <paramref name="right"/>; otherwise, <see langword="false" />.</returns>
		public static bool operator ==(PedMoveBlendRatio left, PedMoveBlendRatio right) => Equals(left, right);
		/// <summary>
		/// Tests for inequality between two objects.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><see langword="true" /> if <paramref name="left"/> has a different value than <paramref name="right"/>; otherwise, <see langword="false" />.</returns>
		public static bool operator !=(PedMoveBlendRatio left, PedMoveBlendRatio right) => !Equals(left, right);

		public bool Equals(PedMoveBlendRatio moveBlendRatio)
		{
			return Value == moveBlendRatio.Value;
		}
		public override bool Equals(object obj)
		{
			if (obj is PedMoveBlendRatio asset)
			{
				return Equals(asset);
			}

			return false;
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode() => Value.GetHashCode();

		/// <summary>
		/// Converts the value of the object to its equivalent string representation.
		/// </summary>
		/// <returns>The string representation of the value of this instance.</returns>
		public override string ToString() => Value.ToString();
	}
}
