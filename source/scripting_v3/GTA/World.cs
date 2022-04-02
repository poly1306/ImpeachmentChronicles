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
using System.Linq;

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

		// removes gang and animal ped models just like CREATE_RANDOM_PED does
		static readonly Func<Model, bool> defaultPredicateForCreateRandomPed = (x => x.IsHumanPed && !x.IsGangPed);
		#endregion

		#region Time & Day

		/// <summary>
		/// Gets or sets a value indicating whether the in-game clock is paused.
		/// </summary>
		public static bool IsClockPaused
		{
			get => SHVDN.NativeMemory.IsClockPaused;
			set => Function.Call(Hash.PAUSE_CLOCK, value);
		}

		/// <summary>
		/// Pauses or resumes the in-game clock.
		/// </summary>
		/// <param name="value">Pauses the game clock if set to <see langword="true" />; otherwise, resumes the game clock.</param>
		[Obsolete("The World.PauseClock is obsolete, use World.IsClockPaused instead.")]
		public static void PauseClock(bool value)
		{
			IsClockPaused = value;
		}

		/// <summary>
		/// Gets or sets the current date and time in the GTA World.
		/// </summary>
		/// <value>
		/// The current date and time.
		/// </value>
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

		/// <summary>
		/// Gets or sets the current time of day in the GTA World.
		/// </summary>
		/// <value>
		/// The current time of day
		/// </value>
		public static TimeSpan CurrentTimeOfDay
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

		/// <summary>
		/// Gets or sets how many milliseconds in the real world one game minute takes.
		/// </summary>
		/// <value>
		/// The milliseconds one game minute takes in the real world.
		/// </value>
		public static int MillisecondsPerGameMinute
		{
			get => Function.Call<int>(Hash.GET_MILLISECONDS_PER_GAME_MINUTE);
			set => SHVDN.NativeMemory.MillisecondsPerGameMinute = value;
		}

		#endregion

		#region Weather & Effects

		/// <summary>
		/// Sets a value indicating whether lights in the <see cref="World"/> should be rendered.
		/// </summary>
		/// <value>
		///   <see langword="true" /> if blackout; otherwise, <see langword="false" />.
		/// </value>
		public static bool Blackout
		{
			set => Function.Call(Hash.SET_ARTIFICIAL_LIGHTS_STATE, value);
		}

		/// <summary>
		/// Gets or sets the weather.
		/// </summary>
		/// <value>
		/// The weather.
		/// </value>
		public static Weather Weather
		{
			get
			{
				for (int i = 0; i < weatherNames.Length; i++)
				{
					if (Function.Call<int>(Hash.GET_PREV_WEATHER_TYPE_HASH_NAME) == Game.GenerateHash(weatherNames[i]))
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
		/// <summary>
		/// Gets or sets the next weather.
		/// </summary>
		/// <value>
		/// The next weather.
		/// </value>
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
						Function.Call(Hash.GET_CURR_WEATHER_STATE, &currentWeatherHash, &nextWeatherHash, &weatherTransition);
					}
					Function.Call(Hash.SET_CURR_WEATHER_STATE, currentWeatherHash, Game.GenerateHash(weatherNames[(int)value]), 0.0f);
				}
			}
		}

		/// <summary>
		/// Transitions to weather.
		/// </summary>
		/// <param name="weather">The weather.</param>
		/// <param name="duration">The duration.</param>
		public static void TransitionToWeather(Weather weather, float duration)
		{
			if (Enum.IsDefined(typeof(Weather), weather) && weather != Weather.Unknown)
			{
				Function.Call(Hash.SET_WEATHER_TYPE_OVERTIME_PERSIST, weatherNames[(int)weather], duration);
			}
		}

		/// <summary>
		/// Sets the gravity level for all <see cref="World"/> objects.
		/// </summary>
		/// <value>
		/// The gravity level:
		/// 9.8f - Default gravity.
		/// 2.4f - Moon gravity.
		/// 0.1f - Very low gravity.
		/// 0.0f - No gravity.
		/// </value>
		public static float GravityLevel
		{
			get => SHVDN.NativeMemory.WorldGravity;
			set
			{
				// Write the value you want to the first item in the array where the native reads the gravity level choices from
				SHVDN.NativeMemory.WorldGravity = value;
				// Call set_gravity_level normally using 0 as gravity type
				// The native will then set the gravity level to what we just wrote
				Function.Call(Hash.SET_GRAVITY_LEVEL, 0);
				// Reset the array item back to 9.8 so as to restore behavior of the native
				SHVDN.NativeMemory.WorldGravity = 9.800000f;
			}
		}

		#endregion

		#region Wind
		public static float WindSpeed
		{
			get => Function.Call<float>(Hash.GET_WIND_SPEED);
			set
			{
				if (value < 0f)
				{
					value = 0;
				}

				if (value > 12f)
				{
					value = 12f;
				}

				Function.Call(Hash.SET_WIND_SPEED, value);
			}
		}

		public static Vector3 WindDirection => Function.Call<Vector3>(Hash.GET_WIND_DIRECTION);
		#endregion

		#region Blips

		/// <summary>
		/// Gets the waypoint blip.
		/// </summary>
		/// <returns>The <see cref="Vector3"/> coordinates of the Waypoint <see cref="Blip"/></returns>
		/// <remarks>
		/// Returns <see langword="null" /> if a waypoint <see cref="Blip"/> hasn't been set
		/// </remarks>
		public static Blip WaypointBlip
		{
			get
			{
				var handle = SHVDN.NativeMemory.GetWaypointBlip();

				if (handle != 0)
				{
					return new Blip(handle);
				}

				return null;
			}
		}

		/// <summary>
		/// Removes the waypoint.
		/// </summary>
		public static void RemoveWaypoint()
		{
			Function.Call(Hash.SET_WAYPOINT_OFF);
		}

		/// <summary>
		/// Gets or sets the waypoint position.
		/// </summary>
		/// <returns>The <see cref="Vector3"/> coordinates of the Waypoint <see cref="Blip"/></returns>
		/// <remarks>
		/// Returns an empty <see cref="Vector3"/> if a waypoint <see cref="Blip"/> hasn't been set
		/// If the game engine cant extract height information the Z component will be 0.0f
		/// </remarks>
		public static Vector3 WaypointPosition
		{
			get
			{
				Blip waypointBlip = WaypointBlip;
				if (waypointBlip == null)
				{
					return Vector3.Zero;
				}

				Vector3 position = waypointBlip.Position;
				position.Z = GetGroundHeight((Vector2)position);
				return position;
			}
			set
			{
				Function.Call(Hash.SET_NEW_WAYPOINT, value.X, value.Y);
			}
		}

		/// <summary>
		/// Gets an <c>array</c> of all the <see cref="Blip"/>s on the map with a given <see cref="BlipSprite"/>.
		/// </summary>
		/// <param name="blipTypes">The blip types to include, leave blank to get all <see cref="Blip"/>s.</param>
		public static Blip[] GetAllBlips(params BlipSprite[] blipTypes)
		{
			int[] blipTypesInt = Array.ConvertAll(blipTypes, blipType => (int)blipType);
			return Array.ConvertAll(SHVDN.NativeMemory.GetNonCriticalRadarBlipHandles(blipTypesInt), handle => new Blip(handle));
		}

		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Blip"/>s in a given region in the World.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Blip"/> against.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Blip"/>s.</param>
		/// <param name="blipTypes">The blip types to include, leave blank to get all <see cref="Blip"/>s.</param>
		public static Blip[] GetNearbyBlips(Vector3 position, float radius, params BlipSprite[] blipTypes)
		{
			int[] blipTypesInt = Array.ConvertAll(blipTypes, blipType => (int)blipType);
			return Array.ConvertAll(SHVDN.NativeMemory.GetNonCriticalRadarBlipHandles(position.ToArray(), radius, blipTypesInt), handle => new Blip(handle));
		}

		/// <summary>
		/// Creates a <see cref="Blip"/> at the given position on the map.
		/// </summary>
		/// <param name="position">The position of the blip on the map.</param>
		public static Blip CreateBlip(Vector3 position)
		{
			return new Blip(Function.Call<int>(Hash.ADD_BLIP_FOR_COORD, position.X, position.Y, position.Z));
		}
		/// <summary>
		/// Creates a <see cref="Blip"/> for a circular area at the given position on the map.
		/// </summary>
		/// <param name="position">The position of the blip on the map.</param>
		/// <param name="radius">The radius of the area on the map.</param>
		public static Blip CreateBlip(Vector3 position, float radius)
		{
			return new Blip(Function.Call<int>(Hash.ADD_BLIP_FOR_RADIUS, position.X, position.Y, position.Z, radius));
		}

		#endregion

		#region Entities

		/// <summary>
		/// A fast way to get the total number of <see cref="Vehicle"/>s spawned in the world.
		/// </summary>
		public static int VehicleCount => SHVDN.NativeMemory.GetVehicleCount();
		/// <summary>
		/// A fast way to get the total number of <see cref="Ped"/>s spawned in the world.
		/// </summary>
		public static int PedCount => SHVDN.NativeMemory.GetPedCount();
		/// <summary