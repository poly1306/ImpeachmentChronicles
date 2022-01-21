//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Collections;
using System.Collections.Generic;

namespace GTA
{
	public sealed class VehicleWheelCollection : IEnumerable<VehicleWheel>, IEnumerable
	{
		#region Fields
		// Vehicles have up to 10 wheels
		const int MAX_WHEEL_COUNT = 10;
		readonly VehicleWheel[] _vehicleWheels = new VehicleWheel[MAX_WHEEL_COUNT];
		VehicleWheel[] _vehicleWheelsForIndex6And7;
		VehicleWheel _nullWheel;
		#endregion

		internal VehicleWheelCollection(Vehicle owner)
		{
			Vehicle = owner;
		}

		[Obsolete("VehicleWheel indexer overload with int index does not support any of the wheels wheel_lm2, wheel_rm2, wheel_lm3, or wheel_lm3, but provided for legacy scripts compatibility in v3 API. Use VehicleWheel indexer overload with VehicleWheelBoneId enum instead.")]
		public VehicleWheel this[int index]
		{
			get
			{
				// Return null wheel instance to avoid scripts that targets to between 3.0 to 3.1 not working due to a exception
				// The vehicle wheel id array for natives defines only 8 elements, and any other values can result in undefined behavior or even memory access violation
				if (index < 0 || index > 7)
				{
					return _nullWheel ?? (_nullWheel = new VehicleWheel(Vehicle, -1));
				}

				if (index < 6)
				{
					VehicleWheelBoneId boneId = VehicleWheel.vehicleWheelBoneIndexTableForNatives[index];
					int boneIndexZeroBased = (int)boneId - 11;
					return _vehicleWheels[boneIndexZeroBased] ?? (_vehicleWheels[boneIndexZeroBased] = new VehicleWheel(Vehicle, index));
				}
				// Use a special array in case some scripts access to index 6 or 7 wheel and read Index property
				else
				