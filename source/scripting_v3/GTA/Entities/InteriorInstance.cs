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

				return new Model(SH