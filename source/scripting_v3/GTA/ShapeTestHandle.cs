//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Math;
using GTA.Native;
using System;

namespace GTA
{
	/// <summary>
	/// Represents a shape test handle.
	/// You need to call <see cref="O:GetResult"/> or <see cref="O:GetResultIncludingMaterial"/> every frame until one of the methods returns <see cref="ShapeTestStatus.Ready"/>.
	/// </summary>
	public struct ShapeTestHandle : IEquatable<ShapeTestHandle>, INativeValue
	{
		internal ShapeTestHandle(int handle) : this()
		{
			Handle = handle;
		}

		/// <summary>
		/// Gets the shape test handle.
		/// </summary>
		public int Handle
		{
			get; private set;
		}

		/// <summary>
		/// Gets the native representation of this <see cref="Model"/>.
		/// </summar