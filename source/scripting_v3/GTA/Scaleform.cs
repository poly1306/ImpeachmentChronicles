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
	/// <summary>
	/// A class which handles rendering of Scaleform elements.
	/// </summary>
	public sealed class Scaleform : IDisposable, INativeValue
	{
		public Scaleform(string scaleformID)
		{
			Handle = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, scaleformID);
		}

		public void Dispose()
		{
			if (IsLoaded)
			{
				unsafe
				{
					int handle = Handle;
					Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &handle);
				}
			}

			GC.SuppressFinalize(this);
		}

		public int Handle
		{
			get;
			private set;
		}

		public ulong NativeValue
		{
			get
			{
				return (ulong)Handle;
			}
			set
			{
				Handle = unchecked((int)value);
			}
		}

		public bool IsValid => Handle != 0;
		public bool IsLoaded => Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Handle);

		void CallFunctionHead(string function, params object[] arguments)
		{
			Function.Call(Hash.BEGIN_SCALEFORM_MOVIE_METHOD, Handle, function);

			foreach (var argument in arguments)
			{
				if (argument is int argInt)
				{
					Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_INT, argInt);
				}
				else if (argument is string argString)
				{
					Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, SHVDN.NativeMemory.String);
					Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, argString);
					Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);
				}
				else if (argument is char argChar)
				{
					Function.Call(Hash.BEGIN_TEXT_COMMAND_SCALEFORM_STRING, SHVDN.NativeMemory.String);
					Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, argChar.ToString());
					Function.Call(Hash.END_TEXT_COMMAND_SCALEFORM_STRING);
				}
				else if (argument is float argFloat)
				{
					Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_FLOAT, argFloat);
				}
				else if (argument is double argDouble)
				{
					Function.Call(Hash.SCALEFORM_MOVIE_METHOD_ADD_PARAM_FLOAT, (float)arg