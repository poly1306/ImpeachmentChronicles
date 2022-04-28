
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
	/// <summary>
	/// Defines a 4x4 matrix.
	/// </summary>
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct Matrix : IEquatable<Matrix>
	{
		/// <summary>
		/// Gets or sets the element of the matrix that exists in the first row and first column.
		/// </summary>
		public float M11;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the first row and second column.
		/// </summary>
		public float M12;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the first row and third column.
		/// </summary>
		public float M13;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the first row and fourth column.
		/// </summary>
		public float M14;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the second row and first column.
		/// </summary>
		public float M21;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the second row and second column.
		/// </summary>
		public float M22;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the second row and third column.
		/// </summary>
		public float M23;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the second row and fourth column.
		/// </summary>
		public float M24;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the third row and first column.
		/// </summary>
		public float M31;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the third row and second column.
		/// </summary>
		public float M32;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the third row and third column.
		/// </summary>
		public float M33;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the third row and fourth column.
		/// </summary>
		public float M34;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the fourth row and first column.
		/// </summary>
		public float M41;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the fourth row and second column.
		/// </summary>
		public float M42;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the fourth row and third column.
		/// </summary>
		public float M43;

		/// <summary>
		/// Gets or sets the element of the matrix that exists in the fourth row and fourth column.
		/// </summary>
		public float M44;

		/// <summary>
		/// Initializes a new instance of the <see cref="Matrix"/> structure.
		/// </summary>
		/// <param name="values">The values to assign to the components of the matrix. This must be an array with sixteen elements.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <see langword="null" />.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="values"/> contains more or less than sixteen elements.</exception>
		public Matrix(float[] values)
		{
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}

			if (values.Length != 16)
			{
				throw new ArgumentOutOfRangeException("values", "There must be sixteen and only sixteen input values for Matrix.");
			}

			M11 = values[0];
			M12 = values[1];
			M13 = values[2];
			M14 = values[3];

			M21 = values[4];
			M22 = values[5];
			M23 = values[6];
			M24 = values[7];

			M31 = values[8];
			M32 = values[9];
			M33 = values[10];
			M34 = values[11];

			M41 = values[12];
			M42 = values[13];
			M43 = values[14];
			M44 = values[15];
		}

		/// <summary>
		/// A <see cref="Matrix"/> with all of its components set to zero.
		/// </summary>
		public static Matrix Zero => new Matrix();

		/// <summary>
		/// The identity <see cref="Matrix"/>.
		/// </summary>
		public static Matrix Identity => new Matrix() { M11 = 1.0f, M22 = 1.0f, M33 = 1.0f, M44 = 1.0f };

		/// <summary>
		/// Gets or sets the component at the specified index.
		/// </summary>
		/// <value>The value of the matrix component, depending on the index.</value>
		/// <param name="index">The zero-based index of the component to access.</param>
		/// <returns>The value of the component at the specified index.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the <paramref name="index"/> is out of the range [0, 15].</exception>
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return M11;
					case 1:
						return M12;
					case 2:
						return M13;
					case 3:
						return M14;
					case 4:
						return M21;
					case 5:
						return M22;
					case 6:
						return M23;
					case 7:
						return M24;
					case 8:
						return M31;
					case 9:
						return M32;
					case 10:
						return M33;
					case 11:
						return M34;
					case 12:
						return M41;
					case 13:
						return M42;
					case 14:
						return M43;
					case 15:
						return M44;
				}

				throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
			}

			set
			{
				switch (index)
				{
					case 0:
						M11 = value;
						break;
					case 1:
						M12 = value;
						break;
					case 2:
						M13 = value;
						break;
					case 3:
						M14 = value;
						break;
					case 4:
						M21 = value;
						break;
					case 5:
						M22 = value;
						break;
					case 6:
						M23 = value;
						break;
					case 7:
						M24 = value;
						break;
					case 8:
						M31 = value;
						break;
					case 9:
						M32 = value;
						break;
					case 10:
						M33 = value;
						break;
					case 11:
						M34 = value;
						break;
					case 12:
						M41 = value;
						break;
					case 13:
						M42 = value;
						break;
					case 14:
						M43 = value;
						break;
					case 15:
						M44 = value;
						break;
					default:
						throw new ArgumentOutOfRangeException("index", "Indices for Matrix run from 0 to 15, inclusive.");
				}
			}
		}

		/// <summary>
		/// Gets or sets the component at the specified index.
		/// </summary>
		/// <value>The value of the matrix component, depending on the index.</value>
		/// <param name="row">The row of the matrix to access.</param>
		/// <param name="column">The column of the matrix to access.</param>
		/// <returns>The value of the component at the specified index.</returns>
		/// <exception cref="System.ArgumentOutOfRangeException">Thrown when the <paramref name="row"/> or <paramref name="column"/>is out of the range [0, 3].</exception>
		public float this[int row, int column]
		{
			get
			{
				if (row < 0 || row > 3)
				{
					throw new ArgumentOutOfRangeException("row", "Rows and columns for matrices run from 0 to 3, inclusive.");
				}

				if (column < 0 || column > 3)
				{
					throw new ArgumentOutOfRangeException("column", "Rows and columns for matrices run from 0 to 3, inclusive.");
				}

				return this[(row * 4) + column];
			}

			set
			{
				if (row < 0 || row > 3)
				{
					throw new ArgumentOutOfRangeException("row", "Rows and columns for matrices run from 0 to 3, inclusive.");
				}

				if (column < 0 || column > 3)
				{
					throw new ArgumentOutOfRangeException("column", "Rows and columns for matrices run from 0 to 3, inclusive.");
				}

				this[(row * 4) + column] = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is an identity matrix.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if this instance is an identity matrix; otherwise, <see langword="false" />.
		/// </value>
		public bool IsIdentity => Equals(Identity);

		/// <summary>
		/// Gets a value indicating whether this instance has an inverse matrix.
		/// </summary>
		public bool HasInverse => Determinant() != 0.0f;

		/// <summary>
		/// Calculates the determinant of the matrix.
		/// </summary>
		/// <returns>The determinant of the matrix.</returns>
		public float Determinant()
		{
			float temp1 = (M33 * M44) - (M34 * M43);
			float temp2 = (M32 * M44) - (M34 * M42);
			float temp3 = (M32 * M43) - (M33 * M42);
			float temp4 = (M31 * M44) - (M34 * M41);
			float temp5 = (M31 * M43) - (M33 * M41);
			float temp6 = (M31 * M42) - (M32 * M41);

			return ((((M11 * (((M22 * temp1) - (M23 * temp2)) + (M24 * temp3))) - (M12 * (((M21 * temp1) -
				(M23 * temp4)) + (M24 * temp5)))) + (M13 * (((M21 * temp2) - (M22 * temp4)) + (M24 * temp6)))) -
				(M14 * (((M21 * temp3) - (M22 * temp5)) + (M23 * temp6))));
		}

		float Det3x3(float M11, float M12, float M13, float M21, float M22, float M23, float M31, float M32, float M33)
		{
			return M11 * (M22 * M33 - M23 * M32) - M12 * (M21 * M33 - M23 * M31) + M13 * (M21 * M32 - M22 * M31);
		}

		/// <summary>
		/// Inverts the matrix.
		/// </summary>
		public void Invert()
		{
			float Det = Determinant();

			if (Det == 0.0f)
			{
				return;
			}

			float invDet = 1.0f / Det;
			float tM11 = Det3x3(M22, M23, M24, M32, M33, M34, M42, M43, M44) * invDet;
			float tM21 = -Det3x3(M21, M23, M24, M31, M33, M34, M41, M43, M44) * invDet;
			float tM31 = Det3x3(M21, M22, M24, M31, M32, M34, M41, M42, M44) * invDet;
			float tM41 = -Det3x3(M21, M22, M23, M31, M32, M33, M41, M42, M43) * invDet;

			float tM12 = -Det3x3(M12, M13, M14, M32, M33, M34, M42, M43, M44) * invDet;
			float tM22 = Det3x3(M11, M13, M14, M31, M33, M34, M41, M43, M44) * invDet;
			float tM32 = -Det3x3(M11, M12, M14, M31, M32, M34, M41, M42, M44) * invDet;
			float tM42 = Det3x3(M11, M12, M13, M31, M32, M33, M41, M42, M43) * invDet;

			float tM13 = Det3x3(M12, M13, M14, M22, M23, M24, M42, M43, M44) * invDet;
			float tM23 = -Det3x3(M11, M13, M14, M21, M23, M24, M41, M43, M44) * invDet;
			float tM33 = Det3x3(M11, M12, M14, M21, M22, M24, M41, M42, M44) * invDet;
			float tM43 = -Det3x3(M11, M12, M13, M21, M22, M23, M41, M42, M43) * invDet;

			float tM14 = -Det3x3(M12, M13, M14, M22, M23, M24, M32, M33, M34) * invDet;
			float tM24 = Det3x3(M11, M13, M14, M21, M23, M24, M31, M33, M34) * invDet;
			float tM34 = -Det3x3(M11, M12, M14, M21, M22, M24, M31, M32, M34) * invDet;
			float tM44 = Det3x3(M11, M12, M13, M21, M22, M23, M31, M32, M33) * invDet;

			M11 = tM11;
			M12 = tM12;
			M13 = tM13;
			M14 = tM14;

			M21 = tM21;
			M22 = tM22;
			M23 = tM23;
			M24 = tM24;

			M31 = tM31;
			M32 = tM32;
			M33 = tM33;
			M34 = tM34;

			M41 = tM41;
			M42 = tM42;
			M43 = tM43;
			M44 = tM44;
		}

		//This might need to be fixed
		//This can get faster with something like System.Numerics.Vectors
		/// <summary>
		/// Apply the transformation matrix to a point in world space
		/// </summary>
		/// <param name="point">The original vertex location</param>
		/// <returns>The vertex location transformed by the given <see cref="Matrix"/></returns>
		public Vector3 TransformPoint(Vector3 point)
		{
			unsafe
			{
				float[,] vectorFloat = new float[4, 4];
				float* VTempX = stackalloc float[4];
				float* VTempY = stackalloc float[4];
				float* VTempZ = stackalloc float[4];

				fixed (float* vectorFloatPtr = &vectorFloat[0, 0])
				{
					// Splat x,y and z
					for (int i = 0; i < 4; i++)
					{
						VTempX[i] = point.X;
						VTempY[i] = point.Y;
						VTempZ[i] = point.Z;
					}

					// Multiply by the matrix
					for (int i = 0; i < 4; i++)
					{
						VTempX[i] *= this[0, i];
						VTempY[i] *= this[1, i];
						VTempZ[i] *= this[2, i];
					}

					// Add them all together
					for (int i = 0; i < 4; i++)
					{
						VTempX[i] = VTempX[i] + VTempY[i] + VTempZ[i] + this[3, i];
					}

					return new Vector3(VTempX[0], VTempX[1], VTempX[2]);
				}
			}
		}


		/// <summary>
		/// Calculates the position of a point before this transformation matrix gets applied
		/// </summary>
		/// <param name="point">The transformed vertex location</param>
		/// <returns>The original vertex location before being transformed by the given <see cref="Matrix"/></returns>
		public Vector3 InverseTransformPoint(Vector3 point)
		{
			return Invert(this).TransformPoint(point);
		}

		/// <summary>
		/// Determines the sum of two matrices.
		/// </summary>
		/// <param name="left">The first matrix to add.</param>
		/// <param name="right">The second matrix to add.</param>
		/// <returns>The sum of the two matrices.</returns>
		public static Matrix Add(Matrix left, Matrix right)
		{
			Matrix result;
			result.M11 = left.M11 + right.M11;
			result.M12 = left.M12 + right.M12;
			result.M13 = left.M13 + right.M13;
			result.M14 = left.M14 + right.M14;
			result.M21 = left.M21 + right.M21;
			result.M22 = left.M22 + right.M22;
			result.M23 = left.M23 + right.M23;
			result.M24 = left.M24 + right.M24;
			result.M31 = left.M31 + right.M31;
			result.M32 = left.M32 + right.M32;
			result.M33 = left.M33 + right.M33;
			result.M34 = left.M34 + right.M34;
			result.M41 = left.M41 + right.M41;
			result.M42 = left.M42 + right.M42;
			result.M43 = left.M43 + right.M43;
			result.M44 = left.M44 + right.M44;
			return result;
		}

		/// <summary>
		/// Determines the difference between two matrices.
		/// </summary>
		/// <param name="left">The first matrix to subtract.</param>
		/// <param name="right">The second matrix to subtract.</param>
		/// <returns>The difference between the two matrices.</returns>
		public static Matrix Subtract(Matrix left, Matrix right)
		{
			Matrix result;
			result.M11 = left.M11 - right.M11;
			result.M12 = left.M12 - right.M12;
			result.M13 = left.M13 - right.M13;
			result.M14 = left.M14 - right.M14;
			result.M21 = left.M21 - right.M21;
			result.M22 = left.M22 - right.M22;
			result.M23 = left.M23 - right.M23;
			result.M24 = left.M24 - right.M24;
			result.M31 = left.M31 - right.M31;
			result.M32 = left.M32 - right.M32;
			result.M33 = left.M33 - right.M33;
			result.M34 = left.M34 - right.M34;
			result.M41 = left.M41 - right.M41;
			result.M42 = left.M42 - right.M42;
			result.M43 = left.M43 - right.M43;
			result.M44 = left.M44 - right.M44;
			return result;
		}

		/// <summary>
		/// Determines the product of two matrices.
		/// </summary>
		/// <param name="left">The first matrix to multiply.</param>
		/// <param name="right">The second matrix to multiply.</param>
		/// <returns>The product of the two matrices.</returns>
		public static Matrix Multiply(Matrix left, Matrix right)
		{
			Matrix result;
			result.M11 = (left.M11 * right.M11) + (left.M12 * right.M21) + (left.M13 * right.M31) + (left.M14 * right.M41);
			result.M12 = (left.M11 * right.M12) + (left.M12 * right.M22) + (left.M13 * right.M32) + (left.M14 * right.M42);
			result.M13 = (left.M11 * right.M13) + (left.M12 * right.M23) + (left.M13 * right.M33) + (left.M14 * right.M43);
			result.M14 = (left.M11 * right.M14) + (left.M12 * right.M24) + (left.M13 * right.M34) + (left.M14 * right.M44);
			result.M21 = (left.M21 * right.M11) + (left.M22 * right.M21) + (left.M23 * right.M31) + (left.M24 * right.M41);
			result.M22 = (left.M21 * right.M12) + (left.M22 * right.M22) + (left.M23 * right.M32) + (left.M24 * right.M42);
			result.M23 = (left.M21 * right.M13) + (left.M22 * right.M23) + (left.M23 * right.M33) + (left.M24 * right.M43);
			result.M24 = (left.M21 * right.M14) + (left.M22 * right.M24) + (left.M23 * right.M34) + (left.M24 * right.M44);
			result.M31 = (left.M31 * right.M11) + (left.M32 * right.M21) + (left.M33 * right.M31) + (left.M34 * right.M41);
			result.M32 = (left.M31 * right.M12) + (left.M32 * right.M22) + (left.M33 * right.M32) + (left.M34 * right.M42);
			result.M33 = (left.M31 * right.M13) + (left.M32 * right.M23) + (left.M33 * right.M33) + (left.M34 * right.M43);
			result.M34 = (left.M31 * right.M14) + (left.M32 * right.M24) + (left.M33 * right.M34) + (left.M34 * right.M44);
			result.M41 = (left.M41 * right.M11) + (left.M42 * right.M21) + (left.M43 * right.M31) + (left.M44 * right.M41);
			result.M42 = (left.M41 * right.M12) + (left.M42 * right.M22) + (left.M43 * right.M32) + (left.M44 * right.M42);
			result.M43 = (left.M41 * right.M13) + (left.M42 * right.M23) + (left.M43 * right.M33) + (left.M44 * right.M43);
			result.M44 = (left.M41 * right.M14) + (left.M42 * right.M24) + (left.M43 * right.M34) + (left.M44 * right.M44);
			return result;
		}

		/// <summary>
		/// Scales a matrix by the given value.
		/// </summary>
		/// <param name="left">The matrix to scale.</param>
		/// <param name="right">The amount by which to scale.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix Multiply(Matrix left, float right)
		{
			Matrix result;
			result.M11 = left.M11 * right;
			result.M12 = left.M12 * right;
			result.M13 = left.M13 * right;
			result.M14 = left.M14 * right;
			result.M21 = left.M21 * right;