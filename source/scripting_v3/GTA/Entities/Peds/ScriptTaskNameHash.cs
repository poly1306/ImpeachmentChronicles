//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

namespace GTA
{
	/// <summary>
	/// An enumeration of all possible scripted task name hashes.
	/// Used for <see cref="Ped.GetTaskStatus(ScriptTaskNameHash)"/>.
	/// </summary>
	public enum ScriptTaskNameHash : uint
	{
		Any = 0x55966344,
		Invalid = 0x811E343C,
		Pause = 0x03C990EC,
		StandStill = 0xC572E06A,
		Jump = 0x24415046,
		Cower = 0x1C43F4CF,