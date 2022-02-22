//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;

namespace GTA
{
	public readonly struct RaycastResult
	{
		public RaycastResult(int handle) : this()
		{
			NativeVector3 hitPositionArg;
			bool hitSomethingArg;
			int materialHashArg;
			int entityHandleArg;
			NativeVector3 surfaceNormalArg;
			unsafe
			{
				Result = Function.Call<int>(Hash.GET_SHAPE_TEST