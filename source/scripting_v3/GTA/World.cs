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
		/// <summary>
		/// A fast way to get the total number of <see cref="Prop"/>s spawned in the world.
		/// </summary>
		public static int PropCount => SHVDN.NativeMemory.GetObjectCount();
		/// <summary>
		/// A fast way to get the total number of <see cref="Prop"/>s in the world associated with a <see cref="Pickup"/>.
		/// </summary>
		public static int PickupObjectCount => SHVDN.NativeMemory.GetPickupObjectCount();

		/// <summary>
		/// A fast way to get the total number of <see cref="Building"/>s spawned in the world.
		/// </summary>
		public static int BuildingCount => SHVDN.NativeMemory.GetBuildingCount();
		/// <summary>
		/// A fast way to get the total number of <see cref="AnimatedBuilding"/>s spawned in the world.
		/// </summary>
		public static int AnimatedBuildingCount => SHVDN.NativeMemory.GetAnimatedBuildingCount();
		/// <summary>
		/// A fast way to get the total number of <see cref="InteriorInstance"/>s spawned in the world.
		/// </summary>
		public static int InteriorInstanceCount => SHVDN.NativeMemory.GetInteriorInstCount();
		/// <summary>
		/// A fast way to get the total number of <see cref="InteriorProxy"/>s managed in the <see cref="InteriorProxy"/> pool.
		/// </summary>
		public static int InteriorProxyCount => SHVDN.NativeMemory.GetInteriorProxyCount();

		/// <summary>
		/// A fast way to get the total number of <see cref="Projectile"/>s spawned in the world.
		/// </summary>
		public static int ProjectileCount => SHVDN.NativeMemory.GetProjectileCount();

		/// <summary>
		/// Returns the total number of <see cref="Entity"/> colliders used.
		/// </summary>
		public static int EntityColliderCount => SHVDN.NativeMemory.GetEntityColliderCount();

		/// <summary>
		/// The total number of <see cref="Vehicle"/>s that can exist in the world.
		/// </summary>
		/// <remarks>The game will crash when the number of <see cref="Vehicle"/> is the same as this limit and the game tries to create a <see cref="Vehicle"/>.</remarks>
		public static int VehicleCapacity => SHVDN.NativeMemory.GetVehicleCapacity();
		/// <summary>
		/// The total number of <see cref="Ped"/>s that can exist in the world.
		/// </summary>
		/// <remarks>The game will crash when the number of <see cref="Ped"/> is the same as this limit and the game tries to create a <see cref="Ped"/>.</remarks>
		public static int PedCapacity => SHVDN.NativeMemory.GetPedCapacity();
		/// <summary>
		/// The total number of <see cref="Prop"/>s that can exist in the world.
		/// </summary>
		/// <remarks>The game will crash when the number of <see cref="Prop"/> is the same as this limit and the game tries to create a <see cref="Prop"/>.</remarks>
		public static int PropCapacity => SHVDN.NativeMemory.GetObjectCapacity();
		/// <summary>
		/// The total number of <see cref="Prop"/>s in the world associated with a <see cref="Pickup"/> that can exist in the world.
		/// </summary>
		public static int PickupObjectCapacity => SHVDN.NativeMemory.GetPickupObjectCapacity();
		/// <summary>
		/// The total number of <see cref="Projectile"/>s that can exist in the world.
		/// Always returns 50 currently since the limit is hard-coded in the exe.
		/// </summary>
		public static int ProjectileCapacity => SHVDN.NativeMemory.GetProjectileCapacity();
		/// <summary>
		/// The total number of <see cref="Building"/>s that can exist in the world.
		/// </summary>
		public static int BuildingCapacity => SHVDN.NativeMemory.GetBuildingCapacity();
		/// <summary>
		/// The total number of <see cref="AnimatedBuilding"/>s that can exist in the world.
		/// </summary>
		public static int AnimatedBuildingCapacity => SHVDN.NativeMemory.GetAnimatedBuildingCapacity();
		/// <summary>
		/// The total number of <see cref="InteriorInstance"/>s that can exist in the world.
		/// </summary>
		public static int InteriorInstanceCapacity => SHVDN.NativeMemory.GetInteriorInstCapacity();
		/// <summary>
		/// The total number of <see cref="InteriorProxy"/>s the game can manage at the same time in the <see cref="InteriorProxy"/> pool.
		/// </summary>
		public static int InteriorProxyCapacity => SHVDN.NativeMemory.GetInteriorProxyCapacity();
		/// <summary>
		/// <para>The total number of <see cref="Entity"/> colliders can be used. The return value can be different in different versions.</para>
		/// <para>When <see cref="EntityColliderCount"/> reaches this value, no more <see cref="Entity"/> will not be able to be physically moved
		/// and <see cref="Vehicle"/>s and <see cref="Prop"/>s will not be able to detach fragment parts properly.</para>
		/// </summary>
		public static int EntityColliderCapacity => SHVDN.NativeMemory.GetEntityColliderCapacity();

		/// <summary>
		/// Gets the closest <see cref="Ped"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to find the nearest <see cref="Ped"/>.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Ped"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Ped"/>s to get, leave blank for all <see cref="Ped"/> <see cref="Model"/>s.</param>
		/// <remarks>Returns <see langword="null" /> if no <see cref="Ped"/> was in the given region.</remarks>
		public static Ped GetClosestPed(Vector3 position, float radius, params Model[] models)
		{
			return GetClosest(position, GetNearbyPeds(position, radius, models));
		}

		/// <summary>
		/// Gets an <c>array</c>of all <see cref="Ped"/>s in the World.
		/// </summary>
		/// <param name="models">The <see cref="Model"/> of <see cref="Ped"/>s to get, leave blank for all <see cref="Ped"/> <see cref="Model"/>s.</param>
		public static Ped[] GetAllPeds(params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			return Array.ConvertAll(SHVDN.NativeMemory.GetPedHandles(hashes), handle => new Ped(handle));
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Ped"/>s near a given <see cref="Ped"/> in the world
		/// </summary>
		/// <param name="ped">The ped to check.</param>
		/// <param name="radius">The maximun distance from the <paramref name="ped"/> to detect <see cref="Ped"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Ped"/>s to get, leave blank for all <see cref="Ped"/> <see cref="Model"/>s.</param>
		/// <remarks>Doesnt include the <paramref name="ped"/> in the result</remarks>
		public static Ped[] GetNearbyPeds(Ped ped, float radius, params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			int[] handles = SHVDN.NativeMemory.GetPedHandles(ped.Position.ToArray(), radius, hashes);

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
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Ped"/>s in a given region in the World.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Ped"/> against.</param>
		/// <param name="radius">The maximun distance from the <paramref name="position"/> to detect <see cref="Ped"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Ped"/>s to get, leave blank for all <see cref="Ped"/> <see cref="Model"/>s.</param>
		public static Ped[] GetNearbyPeds(Vector3 position, float radius, params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			return Array.ConvertAll(SHVDN.NativeMemory.GetPedHandles(position.ToArray(), radius, hashes), handle => new Ped(handle));
		}

		/// <summary>
		/// Gets the closest <see cref="Vehicle"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to find the nearest <see cref="Vehicle"/>.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Vehicle"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Vehicle"/>s to get, leave blank for all <see cref="Vehicle"/> <see cref="Model"/>s.</param>
		/// <remarks>Returns <see langword="null" /> if no <see cref="Vehicle"/> was in the given region.</remarks>
		public static Vehicle GetClosestVehicle(Vector3 position, float radius, params Model[] models)
		{
			return GetClosest(position, GetNearbyVehicles(position, radius, models));
		}

		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Vehicle"/>s in the World.
		/// </summary>
		/// <param name="models">The <see cref="Model"/> of <see cref="Vehicle"/>s to get, leave blank for all <see cref="Vehicle"/> <see cref="Model"/>s.</param>
		public static Vehicle[] GetAllVehicles(params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			return Array.ConvertAll(SHVDN.NativeMemory.GetVehicleHandles(hashes), handle => new Vehicle(handle));
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Vehicle"/>s near a given <see cref="Ped"/> in the world
		/// </summary>
		/// <param name="ped">The ped to check.</param>
		/// <param name="radius">The maximun distance from the <paramref name="ped"/> to detect <see cref="Vehicle"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Vehicle"/>s to get, leave blank for all <see cref="Vehicle"/> <see cref="Model"/>s.</param>
		/// <remarks>Doesnt include the <see cref="Vehicle"/> the <paramref name="ped"/> is using in the result</remarks>
		public static Vehicle[] GetNearbyVehicles(Ped ped, float radius, params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			int[] handles = SHVDN.NativeMemory.GetVehicleHandles(ped.Position.ToArray(), radius, hashes);

			var result = new List<Vehicle>();
			Vehicle ignore = ped.CurrentVehicle;

			foreach (int handle in handles)
			{
				if (ignore != null && handle == ignore.Handle)
				{
					continue;
				}

				result.Add(new Vehicle(handle));
			}

			return result.ToArray();
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Vehicle"/>s in a given region in the World.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Vehicle"/> against.</param>
		/// <param name="radius">The maximun distance from the <paramref name="position"/> to detect <see cref="Vehicle"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Vehicle"/>s to get, leave blank for all <see cref="Vehicle"/> <see cref="Model"/>s.</param>
		public static Vehicle[] GetNearbyVehicles(Vector3 position, float radius, params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			return Array.ConvertAll(SHVDN.NativeMemory.GetVehicleHandles(position.ToArray(), radius, hashes), handle => new Vehicle(handle));
		}

		/// <summary>
		/// Gets the closest <see cref="Prop"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to find the nearest <see cref="Prop"/>.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Prop"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Prop"/>s to get, leave blank for all <see cref="Prop"/> <see cref="Model"/>s.</param>
		/// <remarks>Returns <see langword="null" /> if no <see cref="Prop"/> was in the given region.</remarks>
		public static Prop GetClosestProp(Vector3 position, float radius, params Model[] models)
		{
			return GetClosest(position, GetNearbyProps(position, radius, models));
		}

		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Prop"/>s in the World.
		/// </summary>
		/// <param name="models">The <see cref="Model"/> of <see cref="Prop"/>s to get, leave blank for all <see cref="Prop"/> <see cref="Model"/>s.</param>
		public static Prop[] GetAllProps(params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			return Array.ConvertAll(SHVDN.NativeMemory.GetPropHandles(hashes), handle => new Prop(handle));
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Prop"/>s in a given region in the World.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Prop"/> against.</param>
		/// <param name="radius">The maximun distance from the <paramref name="position"/> to detect <see cref="Prop"/>s.</param>
		/// <param name="models">The <see cref="Model"/> of <see cref="Prop"/>s to get, leave blank for all <see cref="Prop"/> <see cref="Model"/>s.</param>
		public static Prop[] GetNearbyProps(Vector3 position, float radius, params Model[] models)
		{
			int[] hashes = Array.ConvertAll(models, model => model.Hash);
			return Array.ConvertAll(SHVDN.NativeMemory.GetPropHandles(position.ToArray(), radius, hashes), handle => new Prop(handle));
		}

		/// <summary>
		/// Gets the closest <see cref="Prop"/> to a given position in the World associated with a <see cref="Pickup"/>.
		/// </summary>
		/// <param name="position">The position to find the nearest <see cref="Prop"/>.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Prop"/>s.</param>
		/// <remarks>Returns <see langword="null" /> if no <see cref="Prop"/> was in the given region.</remarks>
		public static Prop GetClosestPickupObject(Vector3 position, float radius)
		{
			return GetClosest(position, GetNearbyPickupObjects(position, radius));
		}

		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Prop"/>s in the World associated with a <see cref="Pickup"/>.
		/// </summary>
		public static Prop[] GetAllPickupObjects()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPickupObjectHandles(), handle => new Prop(handle));
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Prop"/>s in a given region in the World associated with a <see cref="Pickup"/>.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Entity"/> against.</param>
		/// <param name="radius">The maximun distance from the <paramref name="position"/> to detect <see cref="Prop"/>s.</param>
		public static Prop[] GetNearbyPickupObjects(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetPickupObjectHandles(position.ToArray(), radius), handle => new Prop(handle));
		}
		/// <summary>
		/// Gets the closest <see cref="Projectile"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to find the nearest <see cref="Projectile"/>.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Projectile"/>s.</param>
		/// <remarks>Returns <see langword="null" /> if no <see cref="Projectile"/> was in the given region.</remarks>
		public static Projectile GetClosestProjectile(Vector3 position, float radius)
		{
			return GetClosest(position, GetNearbyProjectiles(position, radius));
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Projectile"/>s in the World.
		/// </summary>
		public static Projectile[] GetAllProjectiles()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetProjectileHandles(), handle => new Projectile(handle));
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Projectile"/>s in a given region in the World.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Projectile"/> against.</param>
		/// <param name="radius">The maximum distance from the <paramref name="position"/> to detect <see cref="Projectile"/>s.</param>
		public static Projectile[] GetNearbyProjectiles(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetProjectileHandles(position.ToArray(), radius), handle => new Projectile(handle));
		}

		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Entity"/>s in the World.
		/// </summary>
		public static Entity[] GetAllEntities()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetEntityHandles(), Entity.FromHandle);
		}
		/// <summary>
		/// Gets an <c>array</c> of all <see cref="Entity"/>s in a given region in the World.
		/// </summary>
		/// <param name="position">The position to check the <see cref="Entity"/> against.</param>
		/// <param name="radius">The maximun distance from the <paramref name="position"/> to detect <see cref="Entity"/>s.</param>
		public static Entity[] GetNearbyEntities(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetEntityHandles(position.ToArray(), radius), Entity.FromHandle);
		}

		public static Building[] GetAllBuildings()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetBuildingHandles(), Building.FromHandle);
		}
		public static Building[] GetNearbyBuildings(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetBuildingHandles(position.ToArray(), radius), Building.FromHandle);
		}
		public static Building GetClosestBuilding(Vector3 position, float radius)
		{
			return GetClosest(position, GetNearbyBuildings(position, radius));
		}

		public static AnimatedBuilding[] GetAllAnimatedBuildings()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetAnimatedBuildingHandles(), AnimatedBuilding.FromHandle);
		}
		public static AnimatedBuilding[] GetNearbyAnimatedBuildings(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetAnimatedBuildingHandles(position.ToArray(), radius), AnimatedBuilding.FromHandle);
		}
		public static AnimatedBuilding GetClosestAnimatedBuilding(Vector3 position, float radius)
		{
			return GetClosest(position, GetNearbyAnimatedBuildings(position, radius));
		}

		public static InteriorInstance[] GetAllInteriorInstances()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetInteriorInstHandles(), InteriorInstance.FromHandle);
		}
		public static InteriorInstance[] GetNearbyInteriorInstances(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetInteriorInstHandles(position.ToArray(), radius), InteriorInstance.FromHandle);
		}
		public static InteriorInstance GetClosestInteriorInstance(Vector3 position, float radius)
		{
			return GetClosest(position, GetNearbyInteriorInstances(position, radius));
		}

		public static InteriorProxy[] GetAllInteriorProxies()
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetInteriorProxyHandles(), InteriorProxy.FromHandle);
		}
		public static InteriorProxy[] GetNearbyInteriorProxies(Vector3 position, float radius)
		{
			return Array.ConvertAll(SHVDN.NativeMemory.GetInteriorProxyHandles(position.ToArray(), radius), InteriorProxy.FromHandle);
		}
		public static InteriorProxy GetClosestInteriorProxy(Vector3 position, float radius)
		{
			return GetClosest(position, GetNearbyInteriorProxies(position, radius));
		}

		/// <summary>
		/// Gets the closest <see cref="ISpatial"/> to a given position in the World.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="position">The position to check against.</param>
		/// <param name="spatials">The spatials to check.</param>
		/// <returns>The closest <see cref="ISpatial"/> to the <paramref name="position"/></returns>
		public static T GetClosest<T>(Vector3 position, params T[] spatials) where T : ISpatial
		{
			ISpatial closest = null;
			float closestDistance = 3e38f;

			foreach (var spatial in spatials)
			{
				var distance = position.DistanceToSquared(spatial.Position);
				if (distance <= closestDistance)
				{
					closest = spatial;
					closestDistance = distance;
				}
			}
			return (T)closest;
		}
		/// <summary>
		/// Gets the closest <see cref="ISpatial"/> to a given position in the World ignoring height.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="position">The position to check against.</param>
		/// <param name="spatials">The spatials to check.</param>
		/// <returns>The closest <see cref="ISpatial"/> to the <paramref name="position"/></returns>
		public static T GetClosest<T>(Vector2 position, params T[] spatials) where T : ISpatial
		{
			ISpatial closest = null;
			float closestDistance = 3e38f;
			var position3D = new Vector3(position.X, position.Y, 0.0f);

			foreach (var spatial in spatials)
			{
				var distance = position3D.DistanceToSquared2D(spatial.Position);
				if (distance <= closestDistance)
				{
					closest = spatial;
					closestDistance = distance;
				}
			}
			return (T)closest;
		}
		/// <summary>
		/// Gets the closest <see cref="Building"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="buildings">The buildings to check.</param>
		/// <returns>The closest <see cref="Building"/> to the <paramref name="position"/></returns>
		public static Building GetClosest(Vector3 position, params Building[] buildings)
		{
			Building closest = null;
			float closestDistance = 3e38f;

			foreach (var building in buildings)
			{
				var distance = position.DistanceToSquared(building.Position);
				if (distance <= closestDistance)
				{
					closest = building;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="Building"/> to a given position in the World ignoring height.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="buildings">The buildings to check.</param>
		/// <returns>The closest <see cref="Building"/> to the <paramref name="position"/></returns>
		public static Building GetClosest(Vector2 position, params Building[] buildings)
		{
			Building closest = null;
			float closestDistance = 3e38f;
			var position3D = new Vector3(position.X, position.Y, 0.0f);

			foreach (var building in buildings)
			{
				var distance = position3D.DistanceToSquared2D(building.Position);
				if (distance <= closestDistance)
				{
					closest = building;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="AnimatedBuilding"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="animatedBuildings">The animated building to check.</param>
		/// <returns>The closest <see cref="AnimatedBuilding"/> to the <paramref name="position"/></returns>
		public static AnimatedBuilding GetClosest(Vector3 position, params AnimatedBuilding[] animatedBuildings)
		{
			AnimatedBuilding closest = null;
			float closestDistance = 3e38f;

			foreach (var animatedBuilding in animatedBuildings)
			{
				var distance = position.DistanceToSquared(animatedBuilding.Position);
				if (distance <= closestDistance)
				{
					closest = animatedBuilding;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="AnimatedBuilding"/> to a given position in the World ignoring height.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="animatedBuildings">The animated building to check.</param>
		/// <returns>The closest <see cref="AnimatedBuilding"/> to the <paramref name="position"/></returns>
		public static AnimatedBuilding GetClosest(Vector2 position, params AnimatedBuilding[] animatedBuildings)
		{
			AnimatedBuilding closest = null;
			float closestDistance = 3e38f;
			var position3D = new Vector3(position.X, position.Y, 0.0f);

			foreach (var animatedBuilding in animatedBuildings)
			{
				var distance = position3D.DistanceToSquared2D(animatedBuilding.Position);
				if (distance <= closestDistance)
				{
					closest = animatedBuilding;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="InteriorInstance"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="interiorInstances">The spatials to check.</param>
		/// <returns>The closest <see cref="InteriorInstance"/> to the <paramref name="position"/></returns>
		public static InteriorInstance GetClosest(Vector3 position, params InteriorInstance[] interiorInstances)
		{
			InteriorInstance closest = null;
			float closestDistance = 3e38f;

			foreach (var interiorInstance in interiorInstances)
			{
				var distance = position.DistanceToSquared(interiorInstance.Position);
				if (distance <= closestDistance)
				{
					closest = interiorInstance;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="InteriorInstance"/> to a given position in the World ignoring height.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="interiorInstances">The interior instances to check.</param>
		/// <returns>The closest <see cref="InteriorInstance"/> to the <paramref name="interiorInstances"/></returns>
		public static InteriorInstance GetClosest(Vector2 position, params InteriorInstance[] interiorInstances)
		{
			InteriorInstance closest = null;
			float closestDistance = 3e38f;
			var position3D = new Vector3(position.X, position.Y, 0.0f);

			foreach (var interiorInstance in interiorInstances)
			{
				var distance = position3D.DistanceToSquared2D(interiorInstance.Position);
				if (distance <= closestDistance)
				{
					closest = interiorInstance;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="InteriorProxy"/> to a given position in the World.
		/// </summary>
		/// <param name="position">The position to check against.</param>
		/// <param name="interiorProxies">The spatials to check.</param>
		/// <returns>The closest <see cref="InteriorProxy"/> to the <paramref name="position"/></returns>
		public static InteriorProxy GetClosest(Vector3 position, params InteriorProxy[] interiorProxies)
		{
			InteriorProxy closest = null;
			float closestDistance = 3e38f;

			foreach (var interiorProxy in interiorProxies)
			{
				var distance = position.DistanceToSquared(interiorProxy.Position);
				if (distance <= closestDistance)
				{
					closest = interiorProxy;
					closestDistance = distance;
				}
			}
			return closest;
		}
		/// <summary>
		/// Gets the closest <see cref="InteriorProxy"