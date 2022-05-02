//
// Copyright (C) 2007-2010 SlimDX Group
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
// OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace GTA.Math
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Vector2 : IEquatable<Vector2>
	{
		/// <summary>
		/// Gets or sets the X component of the vector.
		/// </summary>
		/// <value>The X component of the vector.</value>
		public float X;

		/// <summary>
		/// Gets or sets the Y component of the vector.
		/// </summary>
		/// <value>The Y component of the vector.</value>
		public float Y;

		/// <summary>
		/// Initializes a new instance of the <see cref="Vector2"/> class.
		/// </summary>
		/// <param name="x">Initial value for the X component of the vector.</param>
		/// <param name="y">Initial value for the Y component of the vector.</param>
		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Returns this vector with a magnitude of 1.
		/// </summary>
		public Vector2 Normalized => Normalize(new Vector2(X, Y));

		/// <summary>
		/// Returns a null vector. (0,0)
		/// </summary>
		public static Vector2 Zero => new Vector2(0.0f, 0.0f);

		/// <summary>
		/// The X unit <see cref="Vector2"/> (1, 0).
		/// </summary>
		public static Vector2 UnitX => new Vector2(1.0f, 0.0f);

		/// <summary>
		/// The Y unit <see cref="Vector2"/> (0, 1).
		/// </summary>
		public static Vector2 UnitY => new Vector2(0.0f, 1.0f);

		/// <summary>
		/// Returns the up vector. (0,1)
		/// </summary>
		public static Vector2 Up => new Vector2(0.0f, 1.0f);

		/// <summary>
		/// Returns the down vector. (0,-1)
		/// </summary>
		public static Vector2 Down => new Vector2(0.0f, -1.0f);

		/// <summary>
		/// Returns the right vector. (1,0)
		/// </summary>
		public static Vector2 Right => new Vector2(1.0f, 0.0f);

		/// <summary>
		/// Returns the left vector. (-1,0)
		/// </summary>
		public static Vector2 Left => new Vector2(-1.0f, 0.0f);

		/// <summary>
		/// Gets or sets the component at the specified index.
		/// </summary>
		/// <value>The value of the X or Y component, depending on the index.</value>
		/// <param name="index">The index of the component to access. Use 0 for the X component and 1 for the Y component.</param>
		/// <returns>The value of the component at the specified index.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of the range [0, 1].</exception>
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
				}

				throw new ArgumentOutOfRangeException("index", "Indices for Vector2 run from 0 to 1, inclusive.");
			}

			set
			{
				switch (index)
				{
					case 0:
						X = value;
						break;
					case 1:
						Y = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("index", "Indices for Vector2 run from 0 to 1, inclusive.");
				}
			}
		}

		/// <summary>
		/// Calculates the length of the vector.
		/// </summary>
		/// <returns>The length of the vector.</returns>
		public float Length()
		{
			return (float)System.Math.Sqrt((X * X) + (Y * Y));
		}

		/// <summary>
		/// Calculates the squared length of the vector.
		/// </summary>
		/// <returns>The squared length of the vector.</returns>
		public float LengthSquared()
		{
			return (X * X) + (Y * Y);
		}

		/// <summary>
		/// Converts the vector into a unit vector.
		/// </summary>
		public void Normalize()
		{
			float length = Length();
			if (length == 0)
			{
				return;
			}

			float num = 1 / length;
			X *= num;
			Y *= num;
		}

		/// <summary>
		/// Calculates the distance between two vectors.
		/// </summary>
		/// <para