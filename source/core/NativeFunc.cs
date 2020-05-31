//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SHVDN
{
	/// <summary>
	/// Class responsible for executing script functions.
	/// </summary>
	public static unsafe class NativeFunc
	{
		#region ScriptHookV Imports
		/// <summary>
		/// Initializes the stack for a new script function call.
		/// </summary>
		/// <param name="hash">The function hash to call.</param>
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ScriptHookV.dll", ExactSpelling = true, EntryPoint = "?nativeInit@@YAX_K@Z")]
		static extern void NativeInit(ulong hash);

		/// <summary>
		/// Pushes a function argument on the script function stack.
		/// </summary>
		/// <param name="val">The argument value.</param>
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ScriptHookV.dll", ExactSpelling = true, EntryPoint = "?nativePush64@@YAX_K@Z")]
		static extern void NativePush64(ulong val);

		/// <summary>
		/// Executes the script function call.
		/// </summary>
		/// <returns>A pointer to the return value of the call.</returns>
		[SuppressUnmanagedCodeSecurity]
		[DllImport("ScriptHookV.dll", ExactSpelling = true, EntryPoint = "?nativeCall@@YAPEA_KXZ")]
		static unsafe extern ulong* NativeCall();
		#endregion

		/// <summary>
		/// Internal script task which holds all data necessary for a script function call.
		/// </summary>
		class NativeTask : IScriptTask
		{
			internal ulong Hash;
			internal ulong[] Arguments;
			internal unsafe ulong* Result;

			public void Run()
			{
				Result = InvokeInternal(Hash, Arguments);
			}
		}

		/// <summary>
		/// Internal script task which holds all data necessary for a script function call.
		/// </summary>
		class NativeTaskPtrArgs : IScriptTask
		{
			internal ulong Hash;
			internal ulong* ArgumentPtr;
			internal int ArgumentCount;
			internal unsafe ulong* Result;

			public void Run()
			{
				Result = InvokeInternal(Hash, ArgumentPtr, ArgumentCount);
			}
		}

		/// <summary>
		/// Pushes a single string component on the text stack.
		/// </summary>
		/// <param name="str">The string to push.</param>
		static void PushString(string str)
		{
			var domain = SHVDN.ScriptDomain.CurrentDomain;
			if (domain == null)
			{
				throw new InvalidOperationException("Illegal scripting call outside script domain.");
			}

			IntPtr strUtf8 = domain.PinString(str);

			var strArg = (ulong)strUtf8.ToInt64();
			domain.ExecuteTask(new NativeTaskPtrArgs
			{
				Hash = 0x6C188BE134E074AA /* ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME */,
				ArgumentPtr = &strArg,
				ArgumentCount = 1
			});
		}

		/// <summary>
		/// Splits up a string into manageable components and adds them as text components to the current text command.
		/// This requires that a text command that accepts multiple text components is active (e.g. "CELL_EMAIL_BCON").
		/// </summary>
		/// <param name="str">The string to split up.</param>
		public static void PushLongString(string str)
		{
			PushLongString(str, PushString);
		}
		/// <summary>
		/// Splits up a string into manageable components and performs an <paramref name="action"/> on them.
		/// </summary>
		/// <param name="str">The string to split up.</param>
		/// <param name="action">The action to perform on the component.</param>
		public static void PushLongString(string str, Action<string> action)
		{
			const int maxLengthUtf8 = 99;

			if (str == null || Encoding.UTF8.GetByteCount(str) <= maxLengthUtf8)
			{
				action(str);
				return;
			}

			int startPos = 0;
			int currentPos = 0;
			int currentUtf8StrLength = 0;

			while (currentPos < str.Length)
			{
				int codePointSize = 0;

				// Calculate the UTF-8 code point size of the current character
				var chr = str[currentPos];
				if (chr < 0x80)
				{
					codePointSize = 1;
				}
				else if (chr < 0x800)
				{
					codePointSize = 2;
				}
				else if (chr < 0x10000)
				{
					codePointSize = 3;
				}
				else
				{
					#region Surrogate check
					const int LowSurrogateStart = 0xD800;
					const int HighSurrogateStart = 0xD800;

					var temp1 = (int)chr - HighSurrogateStart;
					if (temp1 >= 0 && temp1 <= 0x7ff)
					{
						// Found a high surrogate
						if (currentPos < str.Length - 1)
						{
							var temp2 = str[currentPos + 1] - LowSur