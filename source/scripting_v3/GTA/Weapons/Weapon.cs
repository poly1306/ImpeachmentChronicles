//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Native;
using System.Linq;

namespace GTA
{
	public sealed class Weapon
	{
		#region Fields
		readonly Ped owner;
		WeaponComponentCollection components;
		#endregion

		internal Weapon()
		{
			Hash = WeaponHash.Unarmed;
		}
		internal Weapon(Ped owner, WeaponHash hash)
		{
			this.owner = owner;
			Hash = hash;
		}

		public WeaponHash Hash
		{
			get;
		}

		public string DisplayName => GetDisplayNameFromHash(Hash);

		public string LocalizedName => Game.GetLocalizedString((int)SHVDN.NativeMemory.GetHumanNameHashOfWeaponInfo((uint)Hash));

		public bool IsPresent => Hash == WeaponHash.Unarmed || Function.Call<bool>(Native.Hash.HAS_PED_GOT_WEAPON, owner.Handle, Hash);

		public Model Model => new Model(Function.Call<int>(Native.Hash.GET_WEAPONTYPE_MODEL, Hash));

		public WeaponTint Tint
		{
			get => Function.Call<WeaponTint>(Native.Hash.GET_PED_WEAPON_TINT_INDEX, owner.Handle, Hash);
			set => Function.Call(Native.Hash.SET_PED_WEAPON_TINT_INDEX, owner.Handle, Hash, value);
		}

		public WeaponGroup Group => Function.Call<WeaponGroup>(Native.Hash.GET_WEAPONTYPE_GROUP, Hash);

		public int Ammo
		{
			get
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return 1;
				}

				if (!IsPresent)
				{
					return 0;
				}

				return Function.Call<int>(Native.Hash.GET_AMMO_IN_PED_WEAPON, owner.Handle, Hash);
			}
			set
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return;
				}

				if (IsPresent)
				{
					Function.Call(Native.Hash.SET_PED_AMMO, owner.Handle, Hash, value);
				}
				else
				{
					Function.Call(Native.Hash.GIVE_WEAPON_TO_PED, owner.Handle, Hash, value, false, true);
				}
			}
		}
		public int AmmoInClip
		{
			get
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return 1;
				}

				if (!IsPresent)
				{
					return 0;
				}

				int ammoInClip;
				unsafe
				{
					Function.Call(Native.Hash.GET_AMMO_IN_CLIP, owner.Handle, Hash, &ammoInClip);
				}
				return ammoInClip;
			}
			set
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return;
				}

				if (IsPresent)
				{
					Function.Call(Native.Hash.SET_AMMO_IN_CLIP, owner.Handle, Hash, value);
				}
				else
				{
					Function.Call(Native.Hash.GIVE_WEAPON_TO_PED, owner.Handle, Hash, value, true, false);
				}
			}
		}

		public int MaxAmmo
		{
			get
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return 1;
				}

				int maxAmmo;
				unsafe
				{
					Function.Call(Native.Hash.GET_MAX_AMMO, owner.Handle, Hash, &maxAmmo);
				}
				return maxAmmo;
			}
		}
		public int MaxAmmoInClip
		{
			get
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return 1;
				}

				if (!IsPresent)
				{
					return 0;
				}

				return Function.Call<int>(Native.Hash.GET_MAX_AMMO_IN_CLIP, owner.Handle, Hash, true);
			}
		}

		public int DefaultClipSize => Function.Call<int>(Native.Hash.GET_WEAPON_CLIP_SIZE, Hash);

		public bool InfiniteAmmo
		{
			set
			{
				if (Hash == WeaponHash.Unarmed)
				{
					return;
				}

				Function.Call(Native.Hash.SET_PED_INFINITE_AMMO, owner.Handle, value, Hash);
			}
		}
		public bool InfiniteAmmoClip
		{
			set => Function.Call(Native.Hash.SET_PED_INFINITE_AMMO_CLIP, owner.Handle, value);
		}

		public bool CanUseOnParachute => Function.Call<bool>(Native.Hash.CAN_USE_WEAPON_ON_PARACHUTE, Hash);

		public WeaponComponentCollection Components => components ?? (components = new WeaponComponentCollection(owner, this));

		public static implicit operator WeaponHash(Weapon weapon)
		{
			return weapon.Hash;
		}

		public static string GetDisplayNameFromHash(WeaponHash hash)
		{
			// Will be found in this switch statement if the hash is one of the weapon hashes for singleplayer
			switch (hash)
			{
				case WeaponHash.Unarmed:
					return "WT_UNARMED";
				case WeaponHash.Knife:
					return "WT_KNIFE";
				case WeaponHash.Nightstick:
					return "WT_NGTSTK";
				case WeaponHash.Hammer:
					return "WT_HAMMER";
				case WeaponHash.Bat:
					return "WT_BAT";
				case WeaponHash.Crowbar:
					return "WT_CROWBAR";
				case WeaponHash.GolfClub:
					return "WT_GOLFCLUB";
				case WeaponHash.Pistol:
					return "WT_PIST";
				case WeaponHash.CombatPistol:
					return "WT_PIST_CBT";
				case WeaponHash.Pistol50:
					return "WT_PIST_50";
				case WeaponHash.APPistol:
					return "WT_PIST_AP";
				case WeaponHash.StunGun:
					return "WT_STUN";
				case WeaponHash.MicroSMG:
					return "WT_SMG_MCR";
				case Weapon