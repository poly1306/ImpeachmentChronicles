//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Native;
using System;
using System.Drawing;

namespace GTA
{
	public readonly struct ParticleEffectAsset : IEquatable<ParticleEffectAsset>, IStreamingResource
	{
		/// <summary>
		/// Creates 