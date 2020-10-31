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
	public sealed class Scaleform : IDisposable
	{
		string scaleformID;

		[Obsolete("Scaleform(int handle) is obselete, Please Use Scaleform(string scaleformID) instead")]
		public Scaleform(int handle)
		{
			Handle = handle;
		}
		public Scaleform(string scaleformID)
		{
			this.scaleformID = scaleformID;

			Handle = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, scaleformID);
		}

		public void Dispose()
		{
			Unload();
			GC.SuppressFinalize(this);
		}

		public int Handle
		{
			get;
			private set;
		}

		public bool IsValid => Handle != 0;
		public bool IsLoaded => Function.Call<bool>(Hash.HAS_SCALEFORM_MOVIE_LOADED, Handle);

		[Obsolete("Scaleform.Load(string scaleformID) is obselete, Please Use Scaleform(string scaleformID) instead")]
		public bool Load(string scaleformID)
		{
			int handle = Function.Call<int>(Hash.REQUEST_SCALEFORM_MOVIE, scaleformID);
			if (handle == 0)
			{
				return false;
			}

			Handle = handle;
			this.scaleformID = scaleformID;

			return true;
		}
		public void Unload()
		{
			if (!IsLoaded)
			{
				return;
			}

			int handle = Handle;
			unsafe
			{
				Function.Call(Hash.SET_SCALEFORM_MOVIE_AS_NO_LONGER_NEEDED, &handle);
			}
			Handle = handle;
		}

		public void CallFunction(string function, params object[] arguments)
		{
			Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION, Handle, function);

			foreach (object argument in arguments)
			{
				if (argument is int argInt)
				{
					Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_INT, argInt);
				}
				else if (argument is string argString)
				{
					Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
					Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, argString);
					Function.Call(Hash._END_TEXT_COMPONENT);
				}
				else if (argument is char argChar)
				{
					Function.Call(Hash._BEGIN_TEXT_COMPONENT, "STRING");
					Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, argChar.ToString());
					Function.Call(Hash._END_TEXT_COMPONENT);
				}
				else if (argument is float argFloat)
				{
					Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, argFloat);
				}
				else if (argument is double argDouble)
				{
					Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_FLOAT, (float)argDouble);
				}
				else if (argument is bool argBool)
				{
					Function.Call(Hash._PUSH_SCALEFORM_MOVIE_FUNCTION_PARAMETER_BOOL, argBool)