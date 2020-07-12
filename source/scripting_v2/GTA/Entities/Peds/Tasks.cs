//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;

namespace GTA
{
	public class Tasks
	{
		Ped _ped;

		internal Tasks(Ped ped)
		{
			_ped = ped;
		}

		public void AchieveHeading(float heading)
		{
			AchieveHeading(heading, 0);
		}
		public void AchieveHeading(float heading, int timeout)
		{
			Function.Call(Hash.TASK_ACHIEVE_HEADING, _ped.Handle, heading, timeout);
		}
		public void AimAt(Entity target, int duration)
		{
			Function.Call(Hash.TASK_AIM_GUN_AT_ENTITY, _ped.Handle, target.Handle, duration, 0);
		}
		public void AimAt(Vector3 target, int duration)
		{
			Function.Call(Hash.TASK_AIM_GUN_AT_COORD, _ped.Handle, target.X, target.Y, target.Z, duration, 0, 0);
		}
		public void Arrest(Ped ped)
		{
			Function.Call(Hash.TASK_ARREST_PED, _ped.Handle, ped.Handle);
		}
		public void ChatTo(Ped ped)
		{
			Function.Call(Hash.TASK_CHAT_TO_PED, _ped.Handle, ped.Handle, 16, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
		}
		public void Climb()
		{
			Function.Call(Hash.TASK_CLIMB, _ped.Handle, true);
		}
		public void Cower(int duration)
		{
			Function.Call(Hash.TASK_COWER, _ped.Handle, duration);
		}
		public void CruiseWithVehicle(Vehicle vehicle, float speed)
		{
			CruiseWithVehicle(vehicle, speed, 0);
		}
		public void CruiseWithVehicle(Vehicle vehicle, float speed, int drivingstyle)
		{
			Function.Call(Hash.TASK_VEHICLE_DRIVE_WANDER, _ped.Handle, vehicle.Handle, speed, drivingstyle);
		}
		public void DriveTo(Vehicle vehicle, Vector3 position, float radius, float speed)
		{
			DriveTo(vehicle, position, radius, speed, 0);
		}
		public void DriveTo(Vehicle vehicle, Vector3 position, float radius, float speed, int drivingstyle)
		{
			Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD_LONGRANGE, _ped.Handle, vehicle.Handle, position.X, position.Y, position.Z, speed, drivingstyle, radius);
		}
		public void EnterVehicle()
		{
			EnterVehicle(new Vehicle(0), VehicleSeat.Any, -1, 0.0f, 0);
		}
		public void EnterVehicle(Vehicle vehicle, VehicleSeat seat)
		{
			EnterVehicle(vehicle, seat, -1, 0.0f, 0);
		}
		public void EnterVehicle(Vehicle vehicle, VehicleSeat seat, int timeout)
		{
			EnterVehicle(vehicle, seat, timeout, 0.0f, 0);
		}
		public void EnterVehicle(Vehicle vehicle, VehicleSeat seat, int timeout, float speed)
		{
			EnterVehicle(vehicle, seat, timeout, speed, 0);
		}
		public void EnterVehicle(Vehicle vehicle, VehicleSeat seat, int timeout, float speed, int flag)
		{
			Function.Call(Hash.TASK_ENTER_VEHICLE, _ped.Handle, vehicle.Handle, timeout, (int)(seat), speed, flag, 0);
		}
		public static void EveryoneLeaveVehicle(Vehicle vehicle)
		{
			Function.Call(Hash.TASK_EVERYONE_LEAVE_VEHICLE, vehicle.Handle);
		}
		public void FightAgainst(Ped target)
		{
			Function.Call(Hash.TASK_COMBAT_PED, _ped.Handle, target.Handle, 0, 16);
		}
		public void FightAgainst(Ped target, int duration)
		{
			Function.Call(Hash.TASK_COMBAT_PED_TIMED, _ped.Handle, target.Handle, duration, 0);
		}
		public void FightAgainstHatedTargets(float radius)
		{
			Function.Call(Hash.TASK_COMBAT_HATED_TARGETS_AROUND_PED, _ped.Handle, radius, 0);
		}
		public void FightAgainstHatedTargets(float radius, int duration)
		{
			Function.Call(Hash.TASK_COMBAT_HATED_TARGETS_AROUND_PED_TIMED, _ped.Handle, radius, duration, 0);
		}
		public void FleeFrom(Ped ped)
		{
			FleeFrom(ped, -1);
		}
		public void FleeFrom(Ped ped, int duration)
		{
			Function.Call(Hash.TASK_SMART_FLEE_PED, _ped.Handle, ped.Handle, 100.0f, duration, 0, 0);
		}
		public void FleeFrom(Vector3 position)
		{
			FleeFrom(position, -1);
		}
		public void FleeFrom(Vector3 position, int duration)
		{
			Function.Call(Hash.TASK_SMART_FLEE_COORD, _ped.Handle, position.X, position.Y, position.Z, 100.0f, duration, 0, 0);
		}
		public void FollowPointRoute(params Vector3[] points)
		{
			Function.Call(Hash.TASK_FLUSH_ROUTE);

			foreach (Vector3 point in points)
			{
				Function.Call(Hash.TASK_EXTEND_ROUTE, point.X, point.Y, point.Z);
			}

			Function.Call(Hash.TASK_FOLLOW_POINT_ROUTE, _ped.Handle, 1.0f, 0);
		}
		public void GoTo(Entity target)
		{
			GoTo(target, Vector3.Zero, -1);
		}
		public void GoTo(Entity target, Vector3 offset)
		{
			GoTo(target, offset, -1);
		}
		public void GoTo(Entity target, Vector3 offset, int timeout)
		{
		