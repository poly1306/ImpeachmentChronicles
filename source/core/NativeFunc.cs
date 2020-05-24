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
			var domain = 