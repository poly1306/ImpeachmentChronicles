//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;

namespace GTA
{
	public enum VehicleLockStatus
	{
		None,
		Unlocked,
		/// <summary>
		/// The <see cref="Vehicle"/> cannot be entered regardless of whether the door is open or closed, or missing entirely.
		/// Warping into the <see cref="Vehicle"/> is the only way to make <see cref="Ped"/>s get in on a seat.
		/// </summary>
		CannotEnter,
		/// <summary>
		/// Players cannot enter the <see cref="Vehicle"/> regardless of whether the door is open or closed, or missing entirely.
		///Warping into the <see cref="Vehicle"/> is the only way to make <see cref="Ped"/>s get in on a seat.
		/// </summary>
		PlayerCannotEnter,
		/// <summary>
		/// <para>Doesn't allow players to exit the <see cref="Vehicle"/> with the exit vehicle key or button.</para>
		/// <para>The <see cref="Vehicle"/> is locked and must be broken into even if already broken into (the same as <see cref="CanBeBrokenIntoPersist"/>).</para>
		/// </summary>
		PlayerCannotLeaveCanBeBrokenIntoPersist,
		/// <summary>
		/// For players, the <see cref="Vehicle"/> cannot open any door if it has 