//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GTA
{
	public sealed class Vehicle : Entity
	{
		public Vehicle(int handle) : base(handle)
		{
		}

		public void Repair()
		{
			Function.Call(Hash.SET_VEHICLE_FIXED, Handle);
			RemoveDestroyedFlag(Handle);

			void RemoveDestroyedFlag(int vehicleHandle)
			{
				var address = SHVDN.NativeMemory.GetEntityAddress(vehicleHandle);
				if (address == IntPtr.Zero)
				{
					return;
				}

				int targetValue = SHVDN.NativeMemory.ReadByte(address + 0xD8);

				if ((targetValue & 7) == 3)
					targetValue &= 0xF8;

				SHVDN.NativeMemory.WriteByte(address + 0xD8, (byte)targetValue);
			}
		}

		public void Explode()
		{
			Function.Call(Hash.EXPLODE_VEHICLE, Handle, true, false);
		}

		#region Styling

		public bool IsConvertible => Function.Call<bool>(Hash.IS_VEHICLE_A_CONVERTIBLE, Handle, 0);

		public float DirtLevel
		{
			get => Function.Call<float>(Hash.GET_VEHICLE_DIRT_LEVEL, Handle);
			set => Function.Call(Hash.SET_VEHICLE_DIRT_LEVEL, Handle, value);
		}

		public void InstallModKit()
		{
			Function.Call(Hash.SET_VEHICLE_MOD_KIT, Handle, 0);
		}

		public int GetMod(VehicleMod modType)
		{
			return Function.Call<int>(Hash.GET_VEHICLE_MOD, Handle, (int)(modType));
		}
		public void SetMod(VehicleMod modType, int modIndex, bool variations)
		{
			Function.Call(Hash.SET_VEHICLE_MOD, Handle, (int)(modType), modIndex, variations);
		}
		public int GetModCount(VehicleMod modType)
		{
			return Function.Call<int>(Hash.GET_NUM_VEHICLE_MODS, Handle, (int)(modType));
		}
		public void ToggleMod(VehicleToggleMod toggleMod, bool toggle)
		{
			Function.Call(Hash.TOGGLE_VEHICLE_MOD, Handle, (int)(toggleMod), toggle);
		}
		public bool IsToggleModOn(VehicleToggleMod toggleMod)
		{
			return Function.Call<bool>(Hash.IS_TOGGLE_MOD_ON, Handle, (int)(toggleMod));
		}
		public string GetModTypeName(VehicleMod modType)
		{
			return Function.Call<string>(Hash.GET_MOD_SLOT_NAME, Handle, (int)(modType));
		}
		public string GetToggleModTypeName(VehicleToggleMod toggleModType)
		{
			return Function.Call<string>(Hash.GET_MOD_SLOT_NAME, Handle, (int)(toggleModType));
		}
		public string GetModName(VehicleMod modType, int modValue)
		{
			return Function.Call<string>(Hash.GET_MOD_TEXT_LABEL, Handle, (int)(modType), modValue);
		}

		public void Wash()
		{
			DirtLevel = 0.0f;
		}

		public bool IsExtraOn(int extra)
		{
			return Function.Call<bool>(Hash.IS_VEHICLE_EXTRA_TURNED_ON, Handle, extra);
		}

		public bool ExtraExists(int extra)
		{
			return Function.Call<bool>(Hash.DOES_EXTRA_EXIST, Handle, extra);
		}

		public void ToggleExtra(int extra, bool toggle)
		{
			Function.Call(Hash.SET_VEHICLE_EXTRA, Handle, extra, !toggle);
		}

		public string NumberPlate
		{
			get => Function.Call<string>(Hash.GET_VEHICLE_NUMBER_PLATE_TEXT, Handle);
			set => Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT, Handle, value);
		}

		public NumberPlateMounting NumberPlateMounting => (NumberPlateMounting)Function.Call<int>(Hash.GET_VEHICLE_PLATE_TYPE, Handle);

		public NumberPlateType NumberPlateType
		{
			get => (NumberPlateType)Function.Call<int>(Hash.GET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, Handle);
			set => Function.Call(Hash.SET_VEHICLE_NUMBER_PLATE_TEXT_INDEX, Handle, (int)value);
		}

		public VehicleColor PrimaryColor
		{
			get
			{
				int color1, color2;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_COLOURS, Handle, &color1, &color2);
				}

				return (VehicleColor)color1;
			}
			set
			{
				int color1, color2;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_COLOURS, Handle, &color1, &color2);
				}
				Function.Call(Hash.SET_VEHICLE_COLOURS, Handle, (int)value, color2);
			}
		}

		public VehicleColor SecondaryColor
		{
			get
			{
				int color1, color2;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_COLOURS, Handle, &color1, &color2);
				}

				return (VehicleColor)color2;
			}
			set
			{
				int color1, color2;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_COLOURS, Handle, &color1, &color2);
				}
				Function.Call(Hash.SET_VEHICLE_COLOURS, Handle, color1, (int)value);
			}
		}

		public VehicleColor RimColor
		{
			get
			{
				int pearlescentColor, rimColor;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_EXTRA_COLOURS, Handle, &pearlescentColor, &rimColor);
				}
				return (VehicleColor)rimColor;
			}
			set => Function.Call(Hash.SET_VEHICLE_EXTRA_COLOURS, Handle, (int)PearlescentColor, (int)value);
		}

		public VehicleColor PearlescentColor
		{
			get
			{
				int pearlescentColor, rimColor;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_EXTRA_COLOURS, Handle, &pearlescentColor, &rimColor);
				}
				return (VehicleColor)pearlescentColor;
			}
			set => Function.Call(Hash.SET_VEHICLE_EXTRA_COLOURS, Handle, (int)value, (int)RimColor);
		}

		public VehicleColor DashboardColor
		{
			get
			{
				int dashboardColor;
				unsafe
				{
					Function.Call((Hash)0xB7635E80A5C31BFF, Handle, &dashboardColor);
				}
				return (VehicleColor)dashboardColor;
			}
			set => Function.Call((Hash)0x6089CDF6A57F326C, Handle, (int)value);
		}

		public VehicleColor TrimColor
		{
			get
			{
				int trimColor;
				unsafe
				{
					Function.Call((Hash)0x7D1464D472D32136, Handle, &trimColor);
				}
				return (VehicleColor)trimColor;
			}
			set => Function.Call((Hash)0xF40DD601A65F7F19, Handle, (int)value);
		}

		public int ColorCombination
		{
			get => Function.Call<int>(Hash.GET_VEHICLE_COLOUR_COMBINATION, Handle);
			set => Function.Call(Hash.SET_VEHICLE_COLOUR_COMBINATION, Handle, value);
		}

		public int ColorCombinationCount => Function.Call<int>(Hash.GET_NUMBER_OF_VEHICLE_COLOURS, Handle);

		public VehicleWheelType WheelType
		{
			get => (VehicleWheelType)Function.Call<int>(Hash.GET_VEHICLE_WHEEL_TYPE, Handle);
			set => Function.Call(Hash.SET_VEHICLE_WHEEL_TYPE, Handle, (int)value);
		}

		public VehicleWindowTint WindowTint
		{
			get => (VehicleWindowTint)Function.Call<int>(Hash.GET_VEHICLE_WINDOW_TINT, Handle);
			set => Function.Call(Hash.SET_VEHICLE_WINDOW_TINT, Handle, (int)value);
		}

		public bool IsPrimaryColorCustom => Function.Call<bool>(Hash.GET_IS_VEHICLE_PRIMARY_COLOUR_CUSTOM, Handle);
		public bool IsSecondaryColorCustom => Function.Call<bool>(Hash.GET_IS_VEHICLE_SECONDARY_COLOUR_CUSTOM, Handle);

		public void ClearCustomPrimaryColor()
		{
			Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_PRIMARY_COLOUR, Handle);
		}
		public void ClearCustomSecondaryColor()
		{
			Function.Call(Hash.CLEAR_VEHICLE_CUSTOM_SECONDARY_COLOUR, Handle);
		}

		public Color CustomPrimaryColor
		{
			get
			{
				int r, g, b;
				unsafe
				{
					Function.Call(Hash.GET_VEHICLE_CUSTOM_PRIMARY_COLOUR, Handle, &r, &g, &b);
				}
				return Color.FromArgb(r, g, b);
			}
			set => Function.Call(Hash.SET_VEHICLE_CUSTOM_PRIMARY_COLOUR, Handle, value.R, value.G, val