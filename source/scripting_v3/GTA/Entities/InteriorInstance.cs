//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using System;

namespace GTA
{
	public class InteriorInstance : IExistable
	{
		internal InteriorInstance(int handle)
		{
			Handle = handle;
		}

		/// <summary>
		/// Creates a new instance of an <see cref="InteriorInstance"/> from the given handle.
		/// </summary>
		/// <param name="handle">The interior instance handle.</param>
		/// <returns>
		/// Returns a <see cref="InteriorInstance"/> if this handle corresponds to a <see cref="InteriorInstance"/>.
		/// Returns <see langword="null" /> if no <see cref="InteriorInstance"/> exists this the specified <paramref name="handle"/>
		/// </returns>
		public static InteriorInstance FromHandle(int handle) => SHVDN.NativeMemory.InteriorInstHandleExists(handle) ? new InteriorInstance(handle) : null;

		/// <summary>
		/// The handle of this <see cref="Building"/>. This property is provided mainly for safer instance handling, but this is also used for equality comparison.
		/// </summary>
		public int Handle
		{
			get; private set;
		}

		/// <summary>
		/// Gets the memory address where the <see cref="InteriorInstance"/> is stored in memory.
		/// </summary>
		public IntPtr MemoryAddress => SHVDN.NativeMemory.GetInteriorInstAddress(Handle);

		/// <summary>
		/// Gets the model of this <see cref="InteriorInstance"/>.
		/// </summary>
		public Model Model
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return 0;
				}

				return new Model(SHVDN.NativeMemory.GetModelHashFromEntity(address));
			}
		}

		/// <summary>
		/// Gets this <see cref="InteriorInstance"/>s matrix which stores position and rotation information.
		/// </summary>
		public Matrix Matrix
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return Matrix.Zero;
				}

				return new Matrix(SHVDN.NativeMemory.ReadMatrix(address + 0x60));
			}
		}

		/// <summary>
		/// Gets the rotation of this <see cref="InteriorInstance"/>.
		/// </summary>
		/// <value>
		/// The yaw, pitch, roll rotation values in degree.
		/// </value>
		public Vector3 Rotation
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return Vector3.Zero;
				}

				unsafe
				{
					var tempRotationArray = stackalloc float[3];
					SHVDN.NativeMemory.GetRotationFromMatrix(tempRotationArray, address + 0x60);

					return new Vector3(tempRotationArray[0], tempRotationArray[1], tempRotationArray[2]);
				}
			}
		}

		/// <summary>
		/// Gets the quaternion of this <see cref="InteriorInstance"/>.
		/// </summary>
		public Quaternion Quaternion
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return Quaternion.Zero;
				}

				unsafe
				{
					var tempRotationArray = stackalloc float[4];
					SHVDN.NativeMemory.GetQuaternionFromMatrix(tempRotationArray, address + 0x60);

					return new Quaternion(tempRotationArray[0], tempRotationArray[1], tempRotationArray[2], tempRotationArray[3]);
				}
			}
		}

		/// <summary>
		/// Gets or sets the position of this <see cref="InteriorInstance"/>.
		/// </summary>
		/// <value>
		/// The position in world space.
		/// </value>
		public Vector3 Position
		{
			get
			{
				var address = MemoryAddress;
				if (address == IntPtr.Zero)
				{
					return Vector3.Zero;
				}

				return new Vector3(SHVDN.NativeMemory.ReadVector3(address + 0x90));
			}
		}

		/// <summary>
		/// Gets the <see cref="InteriorProxy"/> this <see cref="InteriorInstance"/> is loaded from.
		/// </summary>
		/// <remarks>returns <see langword="null" /> if this <see cref="InteriorInstance"/> does not exist or SHVDN could not fin