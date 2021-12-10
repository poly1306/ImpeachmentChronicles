//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;

namespace GTA
{
	[Flags]
	public enum AnimationFlags
	{
		None = 0,
		/// <summary>
		/// Repeat the animation.
		/// </summary>
		Loop = 1,
		/// <summary>
		/// Hold on the last frame of the animation.
		/// </summary>
		StayInEndFrame = 2,
		/// <summary>
		/// When the animation finishes pop the peds physical reprsentation position to match the visual representations position.
		/// Note that the animator of the animation must not unwind the animation and must have an independent mover node.
		/// </summary>
		RepositionWhenFinished = 4,
		/// <summary>
		/// The task cannot be interupted by extenal events.
		/// </summary>
		NotInterruptable = 8,
		/// <summary>
		/// Only plays the upper body part of the animation.
		/// Dampens any motion caused by the lower body animation.Note that the animation should include the root node.
		/// </summary>
		UpperBodyOnly = 16,
		/// <summary>
		/// The task will run in the secondary task slot. This means it can be used aswell as a movement task (for instance).
		/// </summary>
		Secondary = 32,
		/// <summary>
		/// When the animation finishes pop the peds physical reprsentation direction to match the visual representations direction.
		/