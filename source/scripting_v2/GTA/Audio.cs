//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;

namespace GTA
{
	public static class Audio
	{
		public static int PlaySoundAt(Entity entity, string soundFile)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, id, soundFile, entity.Handle, 0, 0, 0);
			return id;
		}
		public static int PlaySoundAt(Entity entity, string soundFile, string soundSet)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, 