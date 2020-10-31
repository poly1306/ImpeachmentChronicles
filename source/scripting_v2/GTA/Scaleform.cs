//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;
using System.Drawing;

namespace GTA
{
	public sealed class Scaleform : IDisposable
	{
		string scaleformID;

		[Obsolete("Scaleform(int handle) is obselet