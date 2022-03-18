//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Native;
using System;

namespace GTA
{
	public struct WeaponAsset : IEquatable<WeaponAsset>, INativeValue
	{
		public WeaponAsset(int hash) : this()
		{
			Hash = hash;
		}
		public WeaponAsset(uint hash) : this((int)hash)
		{
		}
		public WeaponAsset(WeaponHash hash) : this((int)hash)
		{
		}

		/// <summary>
		/// Gets the hash for this <see cref="WeaponAsset"/>.
		/// </summary>
		public int Hash
		{
			get; private set;
		}

		/// <summary>
		/// Gets the native representation of this <see cref="WeaponAsset"/>.
		/// </summary>
		public ulong NativeValue
		{
			get => (ulong)Hash;
			set => Hash = unchecked((int)value);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="WeaponAsset"/> is valid as a weapon or a ammo hash.
		/// </summary>
		public bool IsValid => Function.Call<bool>(Native.Hash.IS_WEAPON_VALID, Hash);

		/// <summary>
		/// Gets a valu