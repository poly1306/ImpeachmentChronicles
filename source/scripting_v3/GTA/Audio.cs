//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;

namespace GTA
{
	/// <summary>
	/// Methods to manipulate audio.
	/// </summary>
	public static class Audio
	{
		#region Music

		/// <summary>
		/// Plays music from the game's music files.
		/// </summary>
		/// <param name="musicFile">The music file to play.</param>
		public static void PlayMusic(string musicFile)
		{
			Function.Call(Hash.TRIGGER_MUSIC_EVENT, musicFile);
		}
		/// <summary>
		/// Cancels playing a music file.
		/// </summary>
		/// <param name="musicFile">The music file to stop.</param>
		public static void StopMusic(string musicFile)
		{
			Function.Call(Hash.CANCEL_MUSIC_EVENT, musicFile);
		}

		#endregion

		#region Sounds

		/// <summary>
		/// Plays a sound from the game's sound files at the specified <paramref name="entity"/>.
		/// </summary>
		/// <param name="entity">The entity to play the sound at.</param>
		/// <param name="soundFile">The sound file to play.</param>
		/// <returns>The identifier of the active sound effect instance.</returns>
		public static int PlaySoundAt(Entity entity, string soundFile)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, id, soundFile, entity.Handle, 0, 0, 0);
			return id;
		}
		/// <summary>
		/// Plays a sound from the game's sound files at the specified <paramref name="entity"/>.
		/// </summary>
		/// <param name="entity">The entity to play the sound at.</param>
		/// <param name="soundFile">The sound file to play.</param>
		/// <param name="soundSet">The name of the sound inside the file.</param>
		/// <returns>The identifier of the active sound effect instance.</returns>
		public static int PlaySoundAt(Entity entity, string soundFile, string soundSet)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, id, soundFile, entity.Handle, soundSet, 0, 0);
			return id;
		}
		/// <summary>
		/// Plays a sound from the game's sound files at the specified <paramref name="position"/>.
		/// </summary>
		/// <param name="position">The world coordinates to play the sound at.</param>
		/// <param name="soundFile">The sound file to play.</param>
		/// <returns>The identifier of the active sound effect instance.</returns>
		public static int PlaySoundAt(Vector3 position, string soundFile)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FROM_COORD, id, soundFile, position.X, position.Y, position.Z, 0, 0, 0, 0);
			return id;
		}
		/// <summary>
		/// Plays a sound from the game's sound files at the specified <paramref name="position"/>.
		/// </summary>
		/// <param name="position">The world coordinates to play the sound at.</param>
		/// <param name="soundFile">The sound file to play.</param>
		/// <param name="soundSet">The name of the sound inside the file.</param>
		/// <returns>The identifier of the active sound effect instance.</returns>
		public static int PlaySoundAt(Vector3 position, string soundFile, string soundSet)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FROM_COORD, id, soundFile, position.X, position.Y, position.Z, soundSet, 0, 0, 0);
			return id;
		}
		/// <summary>
		/// Plays a sound from the game's sound files without transformation.
		/// </summary>
		/// <param name="soundFile">The sound file to play.</param>
		/// <returns>The identifier of the active sound effect instance.</returns>
		public static int PlaySoundFrontend(string soundFile)
		{
			var id = Function.Call<int>(Hash.GET_SOUND_ID);
			Function.Call(Hash.PLAY_SOUND_FRONTEND, id, soundFile, 0, 0);
			return id;
		}
		/// <summary>
		/// Plays a sound from the game's sound files without transformation.
		/// </summary>
