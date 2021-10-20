//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Collections.Generic;
using GTA.Math;
using GTA.Native;

namespace GTA.NaturalMotion
{
	/// <summary>
	/// A base class for manually building a NaturalMotion Euphoria message.
	/// </summary>
	public class Message
	{
		#region Fields
		private readonly string _message;
		private Dictionary<string, (int value, Type type)> _boolIntFloatArguments;
		private Dictionary<string, object> _stringVector3ArrayArguments;
		private static readonly Dictionary<string, (int value, Type type)> _stopArgument = new Dictionary<string, (int value, Type type)>() { { "start", (0, typeof(bool)) } };
		#endregion

		/// <summary>
		/// Creates a class to manually build a NaturalMotion Euphoria message that can be sent to any <see cref="Ped"/>.
		/// </summary>
		/// <param name="message">The name of the message.</param>
		public Message(string message)
		{
			_message = message;
		}

		/// <summary>
		/// Stops this behavior on the given <see cref="Ped"/>.
		/// </summary>
		/// <param name="target">The <see cref="Ped"/> to stop the behavior on.</param>
		public void Abort(Ped target)
		{
			SHVDN.NativeMemory.SendNmMessage(target.Handle, _message, _stopArgument, null);
		}

		/// <summary>
		/// Starts this behavior on the given <see cref="Ped"/> and loop it until manually aborted.
		/// </summary>
		/// <param name="target">The <see cref="Ped"/> to start the behavior on.</param>
		public void SendTo(Ped target)
		{
			if (!target.IsRagdoll)
			{
				if (!target.CanRagdoll)
				{
					target.CanRagdoll = true;
				}
			}

			unsafe
			{
				if (!SHVDN.NativeMemory.IsTaskNMScriptControlOrEventSwitch2NMActive(new IntPtr(target.MemoryAddress)))
				{
					// Does not call when a CTaskNMControl task is active or the CEvent (which usually causes some task) related to CTaskNMControl occured for calling SET_PED_TO_RAGDOLL for legacy script compatibility.
					// Otherwise, the ragdoll duration will be overridden.
					Function.Call(Hash.SET_PED_TO_RAGDOLL, target.Handle, 10000, -1, 1, 1, 1, 0);
				}
			}

			SetArgument("start", true);
			SHVDN.NativeMemory.SendNmMessage(target.Handle, _message, _boolIntFloatArguments, _stringVector3ArrayArguments);
		}
		/// <summary>
		///	Starts this behavior on the given <see cref="Ped"/> for a specified duration.
		/// </summary>
		/// <param name="target">The <see cref="Ped"/> to start the behavior to.</param>
		/// <param name="duration">How long to apply the behavior for (-1 for looped).</param>
		public void SendTo(Ped target, int duration)
		{
			if (!target.CanRagdoll)
			{
				target.CanRagdoll = true;
			}

			// Always call to specify the new duration
			Function.Call(Hash.SET_PED_TO_RAGDOLL, target.Handle, 10000, duration, 1, 1, 1, 0);
			SendTo(target);
		}

		/// <summary>
		/// Sets an argument to a <see cref="bool"/> value.
		/// </summary>
		/// <param name="message">The argument name.</param>
		/// <param name="value">The value to set the argument to.</param>
		public void SetArgument(string message, bool value)
		{
			CreateBoolIntFloatArgDictIfNotCreated();

			int valueConverted = value ? 1 : 0;
			_boolIntFloatArguments[message] = (valueConverted, typeof(bool));
		}
		/// <summary>
		/// Sets an argument to a <see cref="int"/> value.
		/// </summary>
		/// <param name="message">The argument name.</param>
		/// <param name="value">The value to set the argument to.</param>
		public void SetArgument(string message, int value)
		{
			CreateBoolIntFloatArgDictIfNotCreated();

			_boolIntFloatArguments[message] = (value, typeof(int));
		}
		/// <summary>
		/// Sets an argument to a <see cref="float"/> value.
		/// </summary>
		/// <param name="message">The argument name.</param>
		/// <param name="value">The value to set the argument to.</param>
		public void SetArgument(string message, float value)
		{
			CreateBoolIntFloatArgDictIfNotCreated();

			unsafe
			{
				int valueConverted = *(int*)&value;
				_boolIntFloatArguments[message] = (valueConverted, typeof(float));
			}
		}
		/// <summary>
		/// Sets an argument to a <see cref="string"/> value.
		/// </summary>
		/// <param name="message">The argument name.</para