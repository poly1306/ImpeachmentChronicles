//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace GTA
{
	public static class World
	{
		#region Fields
		static readonly string[] weatherNames = {
			"EXTRASUNNY",
			"CLEAR",
			"CLOUDS",
			"SMOG",
			"FOGGY",
			"OVERCAST",
			"RAIN",
			"THUNDER",
			"CLEARING",
			"NEUTRAL",
			"SNOW",
			"BLIZZARD",
			"SNOWLIGHT",
			"XMAS",
			"HALLOWEEN"
		};

		static readonly GregorianCalendar calendar = new GregorianCalendar();
		#endregion

		#region Time & Day

		public static DateTime CurrentDate
		{
			get
			{
				int year = Function.Call<int>(Hash.GET_CLOCK_YEAR);
				int month = Function.Call<int>(Hash.GET_CLOCK_MONTH) + 1;
				int day = System.Math.Min(Function.Call<int>(Hash.GET_CLOCK_DAY_OF_MONTH), calendar.GetDaysInMonth(year, month));
				int hour = Function.Call<int>(Hash.GET_CLOCK_HOURS);
				int minute = Function.Call<int>(Hash.GET_CLOCK_MINUTES);
				int second = Function.Call<int>(Hash.GET_CLOCK_SECONDS);

				return new DateTime(year, month, day, hour, minute, second);
			}
			set
			{
				Function.Call(Hash.SET_CLOCK_DATE, value.Day, value.Month - 1, value.Year);
				Function.Call(Hash.SET_CLOCK_TIME, value.Hour, value.Minute, value.Second);
			}
		}

		public static TimeSpan CurrentDayTime
		{
			get
			{
				int hours = Function.Call<int>(Hash.GET_CLOCK_HOURS);
				int minutes = Function.Call<int>(Hash.GET_CLOCK_MINUTES);
				int seconds = Function.Call<int>(Hash.GET_CLOCK_SECONDS);

				return new TimeSpan(hours, minutes, seconds);
			}
			set => Function.Call(Hash.SET_CLOCK_TIME, value.Hours, value.Minutes, value.Seconds);
		}

		#endregion

		#region Weather & Effects

		public static void SetBlackout(bool enable)
		{
			Function.Call(Hash._SET_BLACKOUT, enable);
		}

		public static Weather Weather
		{
			get
			{
				for (int i = 0; i < weatherNames.Length; i++)
				{
					if (Function.Call<int>(Hash._GET_CURRENT_WEATHER_TYPE) == Game.GenerateHash(weatherNames[i]))
					{
						return (Weather)i;
					}
				}

				return Weather.Unknown;
			}
			set
			{
				if (Enum.IsDefined(typeof(Weather), value) && value != Weather.Unknown)
				{
					Function.Call(Hash.SET_WEATHER_TYPE_NOW, weatherNames[(int)value]);
				}
			}
		}
		public static Weather NextWeather
		{
			get
			{
				for (int i = 0; i < weatherNames.Length; i++)
				{
					if (Function.Call<bool>(Hash.IS_NEXT_WEATHER_TYPE, weatherNames[i]))
					{
						return (Weather)i;
					}
				}

				return Weather.Unknown;
			}
			set
			{
				if (Enum.IsDefined(typeof(Weather), value) && value != Weather.Unknown)
				{
					int currentWeatherHash, nextWeatherHash;
					float weatherTransition;
					unsafe
					{
						Function.Call(Hash._GET_WEATHER_TYPE_TRANSITION, &currentWeatherHash, &nextWeatherHash, &weatherTransition);
					}
					Function.Call(Hash._SET_WEATHER_TYPE_TRANSITION, currentWeatherHash, Game.GenerateHash(weatherNames[(int)value]), 0.0f);
				}
			}
		}

		public static void TransitionToWeather(Weather value, float duration)
		{
			if (Enum.IsDefined(value.GetType(), value) && value != Weather.Unknown)
			{
				Function.Call(Hash._SET_WEATHER_TYPE_OVER_TIME, weatherNames[(int)value], duration);
			}
		}

		public static float WeatherTransition
		{
			get
			{
				int currentWeatherHash, nextWeatherHash;
				float weatherTransition;
				unsafe
				{
					Function.Call(Hash._GET_WEATHER_TYPE_TRANSITION, &currentWeatherHash, &nextWeatherHash, &weatherTransition);
				}
				return weatherTransition;
			}
			set => Function.Call(Hash._SET_WEATHER_TYPE_TRANSITION, 0, 0, value);
		}

		public static int GravityLevel
		{
			set => Function.Call(Hash.SET_GRAVITY_LEVEL, value);
		}

		#endregion

		#region Blips

		public static Vector3 GetWaypointPosition()
		{
			if (!Game.IsWaypointActive)
			{
				return Vector3.Zero;
			}

			bool blipFound = false;
			Vector3 position = Vector3.Zero;

			var waypointBlipHandle = SHVDN.NativeMemory.GetWaypointBlip();

			if (waypointBlipHandle != 0)
			{
				position = Function.Call<Vector3>(Hash.GET_BLIP_INFO_ID_COORD, waypointBlipHandle);
				blipFound = true;
			}

			if (blipFound)
			{
				bool groundFound = false;
				float height = 0.0f;

				for (int i = 800; i >= 0; i -= 50)
				{
					unsafe
					{
						if (Function.Call<bool>(Hash.GET_GROUND_Z_FOR_3D_COORD, position.X, position.Y, (float)i, &height))
						{
							groundFound = true;
							position.Z = height;
							break;
						}
					}

					Script.Wait(100);
				}

				if (!groundFound)
				{
					position.Z = 1000.0f;
				}
			}

			return position;
		}

		public static Blip CreateBlip(Vector3 position)
		{
			return Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, position.X, position.Y, position.Z);
		}
		public static Blip CreateBlip(Vector3 position, float radius)
		{
			return Function.Call<Blip>(Hash.ADD_BLIP_FOR_RADIUS, position.X, position.Y, position.Z, radius);
		}

		#endregion

		#region Entities

		public static Ped[] GetAllPeds()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPedHandles(), handle => new Ped(handle));
		}
		public static Ped[] GetAllPeds(Model model)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPedHandles(new[] { model.Hash }), handle => new Ped(handle));
		}
		public static Ped[] GetNearbyPeds(Ped ped, float radius)
		{
			int[] handles = SHVDN.NativeMemory.GetPedHandles(ped.Position.ToArray(), radius);

			var result = new List<Ped>();

			foreach (int handle in handles)
			{
				if (handle == ped.Handle)
				{
					continue;
				}

				result.Add(new Ped(handle));
			}

			return result.ToArray();
		}
		public static Ped[] GetNearbyPeds(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPedHandles(position.ToArray(), radius), handle => new Ped(handle));
		}
		public static Ped[] GetNearbyPeds(Vector3 position, float radius, Model model)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPedHandles(position.ToArray(), radius, new[] { model.Hash }), handle => new Ped(handle));
		}

		public static Vehicle[] GetAllVehicles()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetVehicleHandles(), handle => new Vehicle(handle));
		}
		public static Vehicle[] GetAllVehicles(Model model)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetVehicleHandles(new[] { model.Hash }), handle => new Vehicle(handle));
		}
		public static Vehicle[] GetNearbyVehicles(Ped ped, float radius)
		{
			int[] handles = SHVDN.NativeMemory.GetVehicleHandles(ped.Position.ToArray(), radius);

			var result = new List<Vehicle>();
			Vehicle ignore = ped.CurrentVehicle;
			int ignoreHandle = Vehicle.Exists(ignore) ? ignore.Handle : 0;

			foreach (int handle in handles)
			{
				if (handle == ignoreHandle)
				{
					continue;
				}

				result.Add(new Vehicle(handle));
			}

			return result.ToArray();
		}
		public static Vehicle[] GetNearbyVehicles(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetVehicleHandles(position.ToArray(), radius), handle => new Vehicle(handle));
		}
		public static Vehicle[] GetNearbyVehicles(Vector3 position, float radius, Model model)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetVehicleHandles(position.ToArray(), radius, new[] { model.Hash }), handle => new Vehicle(handle));
		}

		public static Prop[] GetAllProps()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPropHandles(), handle => new Prop(handle));
		}
		public static Prop[] GetAllProps(Model model)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPropHandles(new[] { model.Hash }), handle => new Prop(handle));
		}
		public static Prop[] GetNearbyProps(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPropHandles(position.ToArray(), radius), handle => new Prop(handle));
		}
		public static Prop[] GetNearbyProps(Vector3 position, float radius, Model model)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPropHandles(position.ToArray(), radius, new[] { model.Hash }), handle => new Prop(handle));
		}

		public static Blip[] GetActiveBlips()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetNonCriticalRadarBlipHandles(), handle => new Blip(handle));
		}

		public static Entity[] GetAllEntities()
		{
			return Array.ConvertAll<int, Entity>(SHVDN.NativeMemory.GetEntityHandles(), Entity.FromHandle);
		}
		public static Entity[] GetNearbyEntities(Vector3 position, float radius)
		{
			return Array.ConvertAll<int, Entity>(SHVDN.NativeMemory.GetEntityHandles(position