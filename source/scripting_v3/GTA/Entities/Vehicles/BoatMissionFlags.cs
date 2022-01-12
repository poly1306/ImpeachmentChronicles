//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;

namespace GTA
{
	[Flags]
	public enum BoatMissionFlags
	{
		StopAtEnd = 1,
		StopAtShore = 2,
		AvoidShore = 4,
		PreferForward = 8,
		NeverStop = 16,
		NeverNavMesh = 32,
		NeverRoute = 64,
		ForceBeached = 128,
		UseWanderRoute = 256,
		UseFleeRou