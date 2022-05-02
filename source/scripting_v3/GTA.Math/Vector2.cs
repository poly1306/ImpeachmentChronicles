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
		/// <param name="position">The second vector to calculate the distance to.</param>
		/// <returns>The distance to the other vector.</returns>
		public float DistanceTo(Vector2 position)
		{
			return (position - this).Length();
		}

		/// <summary>
		/// Calculates the squared distance between two vectors.
		/// </summary>
		/// <param name="position">The second vector to calculate the squared distance to.</param>
		/// <returns>The squared distance to the other vector.</returns>
		public float DistanceToSquared(Vector2 position)
		{
			return DistanceSquared(position, this);
		}

		/// <summary>
		/// Calculates the distance between two vectors.
		/// </summary>
		/// <param name="position1">The first vector to calculate the distance to the second vector.</param>
		/// <param name="position2">The second vector to calculate the distance to the first vector.</param>
		/// <returns>The distance between the two vectors.</returns>
		public static float Distance(Vector2 position1, Vector2 position2)
		{
			return (position1 - position2).Length();
		}

		/// <summary>
		/// Calculates the squared distance between two vectors.
		/// </summary>
		/// <param name="position1">The first vector to calculate the squared distance to the second vector.</param>
		/// <param name="position2">The second vector to calculate the squared distance to the first vector.</param>
		/// <returns>The squared distance between the two vectors.</returns>
		public static float DistanceSquared(Vector2 position1, Vector2 position2)
		{
			return (position1 - position2).LengthSquared();
		}

		/// <summary>
		/// Returns the angle in degrees between from and to.
		/// The angle returned is always the acute angle between the two vectors.
		/// </summary>
		public static float Angle(Vector2 from, Vector2 to)
		{
			return System.Math.Abs(SignedAngle(from, to));
		}

		/// <summary>
		/// Returns the signed angle in degrees between from and to.
		/// </summary>
		public static float SignedAngle(Vector2 from, Vector2 to)
		{
			return (float)((System.Math.Atan2(to.Y, to.X) - System.Math.Atan2(from.Y, from.X)) * (180.0 / System.Math.PI));
		}

		/// <summary>
		/// Converts a vector to a heading.
		/// </summary>
		public float ToHeading()
		{
			return (float)((System.Math.Atan2(X, -Y) + System.Math.PI) * (180.0 / System.Math.PI));
		}

		/// <summary>
		/// Returns a new normalized vector with random X and Y components.
		/// </summary>
		public static Vector2 RandomXY()
		{
			Vector2 v;
			double radian = Random.Instance.NextDouble() * 2 * System.Math.PI;
			v.X = (float)(System.Math.Cos(radian));
			v.Y = (float)(System.Math.Sin(radian));
			v.Normalize();
			return v;
		}

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The sum of the two vectors.</returns>
		public static Vector2 Add(Vector2 left, Vector2 right) => new Vector2(left.X + right.X, left.Y + right.Y);

		/// <summary>
		/// Subtracts two vectors.
		/// </summary>
		/// <param name="left">The first vector to subtract.</param>
		/// <param name="right">The second vector to subtract.</param>
		/// <returns>The difference of the two vectors.</returns>
		public static Vector2 Subtract(Vector2 left, Vector2 right) => new Vector2(left.X - right.X, left.Y - right.Y);

		/// <summary>
		/// Scales a vector by the given value.
		/// </summary>
		/// <param name="value">The vector to scale.</param>
		/// <param name="scale">The amount by which to scale the vector.</param>
		/// <returns>The scaled vector.</returns>
		public static Vector2 Multiply(Vector2 value, float scale) => new Vector2(value.X * scale, value.Y * scale);

		/// <summary>
		/// Multiplies a vector with another by performing component-wise multiplication.
		/// </summary>
		/// <param name="left">The first vector to multiply.</param>
		/// <param name="right">The second vector to multiply.</param>
		/// <returns>The multiplied vector.</returns>
		public static Vector2 Multiply(Vector2 left, Vector2 right) => new Vector2(left.X * right.X, left.Y * right.Y);

		/// <summary>
		/// Scales a vector by the given value.
		/// </summary>
		/// <param name="value">The vector to scale.</param>
		/// <param name="scale">The amount by which to scale the vector.</param>
		/// <returns>The scaled vector.</returns>
		public static Vector2 Divide(Vector2 value, float scale) => new Vector2(value.X / scale, value.Y / scale);

		/// <summary>
		/// Reverses the direction of a given vector.
		/// </summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>A vector facing in the opposite direction.</returns>
		public static Vector2 Negate(Vector2 value) => new Vector2(-value.X, -value.Y);

		/// <summary>
		/// Restricts a value to be within a specified range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The clamped value.</returns>
		public static Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
		{
			float x = value.X;
			x = (x > max.X) ? max.X : x;
			x = (x < min.X) ? min.X : x;

			float y = value.Y;
			y = (y > max.Y) ? max.Y : y;
			y = (y < min.Y) ? min.Y : y;

			return new Vector2(x, y);
		}

		/// <summary>
		/// Performs a linear interpolation between two vectors.
		/// </summary>
		/// <param name="start">Start vector.</param>
		/// <param name="end">End vector.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="end"/>.</param>
		/// <returns>The linear interpolation of the two vectors.</returns>
		/// <remarks>
		/// This method performs the linear interpolation based on the following formula.
		/// <code>start + (end - start) * amount</code>
		/// Passing <paramref name="amount"/> a value of 0 will cause <paramref name="start"/> to be returned; a value of 1 will cause <paramref name="end"/> to be returned.
		/// </remarks>
		public static Vector2 Lerp(Vector2 start, Vector2 end, float amount)
		{
			Vector2 vector;

			vector.X = start.X + ((end.X - start.X) * amount);
			vector.Y = start.Y + ((end.Y - start.Y) * amount);

			return vector;
		}

		/// <summary>
		/// Converts the vector into a unit vector.
		/// </summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		public static Vector2 Normalize(Vector2 vector)
		{
			vector.Normalize();
			ret