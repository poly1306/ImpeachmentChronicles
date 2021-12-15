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
		/// <remarks>Tag synchronizer flags are for syncing the anim against ped movement (walking / running / etc).</remarks>
		TagSyncOut = 32768,
		/// <summary>
		/// Sync all the time (Only usefull to synchronize a partial anim e.g. an upper body).
		/// </summary>
		/// <remarks>Tag synchronizer flags are for syncing the anim against ped movement (walking / running / etc).</remarks>
		TagSyncContinuous = 65536,
		/// <summary>
		/// Force the anim task to start even if the ped is falling / ragdolling / etc.
		/// Can fix issues with peds not playing their anims immediately after a warp / etc.
		/// </summary>
		ForceStart = 131072,
		/// <summary>
		/// Use the kinematic physics mode on the entity for the duration of the anim (it should push other entities out of the way, and not be pushed around by players / etc).
		/// </summary>
		UseKinematicPhysics = 262144,
		/// <summary>
		/// Updates the peds capsule position every frame based on the animation.
		/// Use in conjunction with <see cref="UseKinematicPhysics"/> to create characters that cannot be pushed off course by other entities / geometry / etc whilst playing the anim.
		/// </summary>
		UseMoverExtraction = 524288,
		/// <summary>
		/// Indicates that the ped's weapon should be hidden while this animation is playing.
		/// </summary>
		HideWeapon = 1048576,
		/// <summary>
		/// When the anim ends, kill the ped and use the currently playing anim as the dead pose.
		/// </summary>
		EndsInDeadPose = 2097152,
		/// <summary>
		/// If the peds ragdoll bounds make contact with something physical (that isn't flat ground) activate the ragdoll and fall over.
		/// </summary>
		RagdollOnCollision = 4194304,
		/// <summary>
		/// Currently used only on secondary anim tasks. Secondary anim tasks will end automatically when the ped dies. Setting this flag stops that from happening.
		/// </summary>
		DontExitOnDeath = 8388608,
		/// <summary>
		/// Allow aborting from damage events (including non-ragdoll damage events) even when blocking other ai events using <see cref="NotInterruptable"/>.
		/// </summary>
		AbortOnWeaponDamage = 16777216,
		/// <summary>
		/// Prevent adjusting the capsule on the enter state (useful if script is doing a sequence of scripted anims and they are known to more or less stand still