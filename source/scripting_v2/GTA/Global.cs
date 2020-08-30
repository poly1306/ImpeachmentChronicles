//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Text;
using System.Runtime.InteropServices;
using GTA.Math;

namespace GTA
{
	public unsafe struct Global
	{
		readonly IntPtr address;

		internal Global(int index)
		{
			address = SHVDN.NativeMemory.GetGlobalPtr(index);
		}

		public unsafe ulong* MemoryAddress => (ulong*)address.ToPointer();

		public unsafe void SetInt(int value)
		{
			SHVDN.NativeMemory.WriteInt32(address, value);
		}
		public unsafe void SetFloat(float value)
		{
			SHVDN.NativeMemory.WriteFloat(address, value);
		}
		public unsafe void SetString(string value)
		{
			int size = Encoding.UTF8.GetByteCount(value);
			Marshal.Copy(Encoding.UTF8.GetBytes(value), 0, address, size