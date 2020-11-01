//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Native;
using System;
using System.Runtime.InteropServices;

namespace GTA
{
	[StructLayout(LayoutKind.Explicit, Size = 0x138)]
	internal unsafe struct DlcWeaponData
	{
		[FieldOffset(0x00)] int validCheck;
		[FieldOffset(0x08)] int weaponHash;
		[FieldOffset(0x18)] int weaponCost;
		[FieldOffset(0x20)] int ammoCost;
		[FieldOffset(0x28)] int ammoType;
		[FieldOffset(0x30)] int defaultClipSize;
		[FieldOffset(0x38)] fixed byte name[0x40];
		[FieldOffset(0x78)] fixed byte desc[0x40];
		[FieldOffset(0xB8)] fixed byte simpleDesc[0x40]; // Usually refers to "the " + name
		[FieldOffset(0xF8)] fixed byte upperCaseName[0x40];

		public WeaponHash Hash => (WeaponHash)weaponHash;

		public string DisplayName
		{
			get
			{
				fixed (byte* ptr = name)
				{
					return SHVDN.NativeMemory.PtrToStringUTF8(new IntPtr(ptr));
				}
			}
		}
	}

	[StructLayout(LayoutKind.Explicit, Size = 0x110)]
	internal unsafe struct DlcWeaponComponentData
	{
		[FieldOffset(