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
		/// Note that the animator of the animation must not unwind the animation and must have an independent mover node.
		/// </summary>
		ReorientWhenFinished = 64,
		/// <summary>
		/// Ends the animation early if the ped attemps to move e.g. if the player tries to move using the controller.
		/// Can also be used to blend out automatically when an AI ped starts moving by combining it with the <see cref="Secondary"/> flag.
		/// </summary>
		AbortOnPedMovement = 128,
		/// <summary>
		/// Play back the animation additively. Note that this will only produce sensible results on specifically authored additive animations.
		/// </summary>
		Additive = 256,
		/// <summary>
		/// Do not react to collision detection whilst this anim is playing.
		/// </summary>
		TurnOffCollision = 512,
		/// <summary>
		/// Do not apply any physics forces whilst the anim is playing.
		/// Automatically turns off collision, extracts any initial offset provided in the clip and uses per frame mover extraction.
		/// </summary>
		OverridePhysics = 1024,
		/// <summary>
		/// Do not apply gravity while the anim is playing.
		/// </summary>
		IgnoreGravity = 2048,
		/// <summary>
		/// Extract an initial offset from the playback position authored by the animator.
		/// Use this flag when playing back anims on different peds which have been authored to sync with each other.
		/// </summary>
		ExtractInitialOffset = 4096,
		/// <summary>
		/// Exit the animation task if it is interrupted by another task (ie Natural Motion).
		/// Without this flag bing set looped animations will restart ofter the NM task
		/// </summary>
		ExitAfterInterrupted = 8192,
		/// <summary>
		/// Sync the anim whilst blending in (use for seamless transitions from walk / run into a full body anim).
		/// </summary>
		/// <remarks>Tag synchronizer flags are for syncing the anim against ped movement (walking / running / etc).</remarks>
		TagSyncIn = 16384,
		/// <summary>
		/// Sync the anim whilst blending out (use for seamless transitions from a full body anim into walking / running behaviour).
		/// </summary>
		/// <remarks>Tag synchronizer flags are for syncing the anim against ped movement (walking / r