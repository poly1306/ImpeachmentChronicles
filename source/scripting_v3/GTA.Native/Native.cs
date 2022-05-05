
//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;


namespace GTA.Native
{
	public interface INativeValue
	{
		ulong NativeValue
		{
			get; set;
		}
	}

	internal static class NativeHelper<T>
	{
		static class CastCache<TFrom>
		{
			internal static readonly Func<TFrom, T> Cast;

			static CastCache()
			{
				var paramExp = Expression.Parameter(typeof(TFrom));
				var convertExp = Expression.Convert(paramExp, typeof(T));
				Cast = Expression.Lambda<Func<TFrom, T>>(convertExp, paramExp).Compile();
			}
		}

		static readonly Func<IntPtr, T> _ptrToStrFunc;

		static NativeHelper()
		{
			var ptrToStrMethod = new DynamicMethod("PtrToStructure<" + typeof(T) + ">", typeof(T),
				new Type[] { typeof(IntPtr) }, typeof(NativeHelper<T>), true);

			ILGenerator generator = ptrToStrMethod.GetILGenerator();
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Ldobj, typeof(T));
			generator.Emit(OpCodes.Ret);

			_ptrToStrFunc = (Func<IntPtr, T>)ptrToStrMethod.CreateDelegate(typeof(Func<IntPtr, T>));
		}

		internal static T Convert<TFrom>(TFrom from)
		{
			return CastCache<TFrom>.Cast(from);
		}

		internal static T PtrToStructure(IntPtr ptr)
		{
			return _ptrToStrFunc(ptr);
		}
	}
	internal static class InstanceCreator<T1, TInstance>
	{
		internal static Func<T1, TInstance> Create;

		static InstanceCreator()
		{
			var constructorInfo = typeof(TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
				new[] { typeof(T1) }, null);
			var arg1Exp = Expression.Parameter(typeof(T1));

			var newExp = Expression.New(constructorInfo, arg1Exp);
			var lambdaExp = Expression.Lambda<Func<T1, TInstance>>(newExp, arg1Exp);
			Create = lambdaExp.Compile();
		}
	}
	internal static class InstanceCreator<T1, T2, TInstance>
	{
		internal static Func<T1, T2, TInstance> Create;

		static InstanceCreator()
		{
			var constructorInfo = typeof(TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
				new[] { typeof(T1), typeof(T2) }, null);
			var arg1Exp = Expression.Parameter(typeof(T1));
			var arg2Exp = Expression.Parameter(typeof(T2));

			var newExp = Expression.New(constructorInfo, arg1Exp, arg2Exp);
			var lambdaExp = Expression.Lambda<Func<T1, T2, TInstance>>(newExp, arg1Exp, arg2Exp);
			Create = lambdaExp.Compile();
		}
	}
	internal static class InstanceCreator<T1, T2, T3, TInstance>
	{
		internal static Func<T1, T2, T3, TInstance> Create;

		static InstanceCreator()
		{
			var constructor = typeof(TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
				new[] { typeof(T1), typeof(T2), typeof(T3) }, null);
			var arg1 = Expression.Parameter(typeof(T1));
			var arg2 = Expression.Parameter(typeof(T2));
			var arg3 = Expression.Parameter(typeof(T3));

			var newExp = Expression.New(constructor, arg1, arg2, arg3);
			var lambdaExp = Expression.Lambda<Func<T1, T2, T3, TInstance>>(newExp, arg1, arg2, arg3);
			Create = lambdaExp.Compile();
		}
	}

	#region Functions
	/// <summary>
	/// An input argument passed to a script function.
	/// </summary>
	public class InputArgument
	{
		internal ulong _data;

		/// <summary>
		/// Initializes a new instance of the <see cref="InputArgument"/> class to a script function input argument.
		/// </summary>
		/// <param name="value">The pointer value.</param>
		public InputArgument(ulong value)
		{
			_data = value;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="InputArgument"/> class to a script function input argument.
		/// </summary>
		/// <param name="value">The value.</param>
		public InputArgument(IntPtr value)
		{
			unsafe
			{
				_data = (ulong)value.ToInt64();
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="InputArgument"/> class and converts a managed object to a script function input argument.
		/// </summary>
		/// <param name="value">The object to convert.</param>
		public InputArgument(object value)
		{
			unsafe
			{
				_data = Function.ObjectToNative(value);
			}
		}

		/// <summary>
		/// Converts the internal value of the argument to its equivalent string representation.
		/// </summary>
		public override string ToString()
		{
			return _data.ToString();
		}

		#region Implicit Conversion Operators
		// Value types
		public static implicit operator InputArgument(bool value)
		{
			// "new InputArgument(value ? 1 : 0)" calls InputArgument constructor using object parameter, not ulong one
			return value ? new InputArgument(1) : new InputArgument(0);
		}
		public static implicit operator InputArgument(byte value)
		{
			return new InputArgument(value);
		}
		public static implicit operator InputArgument(sbyte value)
		{
			return new InputArgument((ulong)value);
		}
		public static implicit operator InputArgument(short value)
		{
			return new InputArgument((ulong)value);
		}
		public static implicit operator InputArgument(ushort value)
		{
			return new InputArgument(value);
		}
		public static implicit operator InputArgument(int value)
		{
			return new InputArgument((ulong)value);
		}
		public static implicit operator InputArgument(uint value)
		{
			return new InputArgument(value);
		}
		public static implicit operator InputArgument(long value)
		{
			return new InputArgument((ulong)value);
		}
		public static implicit operator InputArgument(ulong value)
		{
			return new InputArgument(value);
		}
		public static implicit operator InputArgument(float value)
		{
			unsafe
			{
				ulong ulongValue = 0;
				*(float*)&ulongValue = value;
				return new InputArgument(ulongValue);
			}
		}
		public static implicit operator InputArgument(double value)
		{
			unsafe
			{
				//Native functions don't consider any arguments as double, so convert double values to float ones
				ulong ulongValue = 0;
				*(float*)&ulongValue = (float)value;
				return new InputArgument(ulongValue);
			}
		}
		public static implicit operator InputArgument(Enum value)
		{
			var enumDataType = Enum.GetUnderlyingType(value.GetType());
			ulong ulongValue = 0;

			if (enumDataType == typeof(int))
			{
				ulongValue = (ulong)Convert.ToInt32(value);
			}
			else if (enumDataType == typeof(uint))
			{
				ulongValue = Convert.ToUInt32(value);
			}
			else if (enumDataType == typeof(long))
			{
				ulongValue = (ulong)Convert.ToInt64(value);
			}
			else if (enumDataType == typeof(ulong))
			{
				ulongValue = Convert.ToUInt64(value);
			}
			else if (enumDataType == typeof(short))
			{
				ulongValue = (ulong)Convert.ToInt16(value);
			}
			else if (enumDataType == typeof(ushort))
			{
				ulongValue = Convert.ToUInt16(value);
			}
			else if (enumDataType == typeof(byte))
			{
				ulongValue = Convert.ToByte(value);
			}
			else if (enumDataType == typeof(sbyte))
			{
				ulongValue = (ulong)Convert.ToSByte(value);
			}

			return new InputArgument(ulongValue);
		}

		// String types
		public static implicit operator InputArgument(string value)
		{
			return new InputArgument(value);
		}
		public static unsafe implicit operator InputArgument(char* value)
		{
			return new InputArgument(new string(value));
		}

		// Pointer types
		public static implicit operator InputArgument(IntPtr value)
		{
			return new InputArgument(value);
		}
		public static unsafe implicit operator InputArgument(void* value)
		{
			return new InputArgument(new IntPtr(value));
		}

		public static implicit operator InputArgument(OutputArgument value)
		{
			return new InputArgument(value.storage);
		}
		#endregion
	}

	/// <summary>
	/// An output argument passed to a script function.
	/// </summary>
	public class OutputArgument : IDisposable
	{
		#region Fields
		bool disposed = false;
		internal IntPtr storage = IntPtr.Zero;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="OutputArgument"/> class for script functions that output data into pointers.
		/// </summary>
		public OutputArgument()
		{
			storage = Marshal.AllocCoTaskMem(24);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="OutputArgument"/> class with an initial value for script functions that require the pointer to data instead of the actual data.
		/// </summary>
		/// <param name="value">The value to set the data of this <see cref="OutputArgument"/> to.</param>
		public OutputArgument(object value) : this()
		{
			unsafe
			{
				*(ulong*)(storage) = Function.ObjectToNative(value);
			}
		}

		/// <summary>
		/// Frees the unmanaged resources associated with this <see cref="OutputArgument"/>.
		/// </summary>
		~OutputArgument()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			Marshal.FreeCoTaskMem(storage);
			disposed = true;
		}

		/// <summary>
		/// Gets the value of data stored in this <see cref="OutputArgument"/>.
		/// </summary>
		public T GetResult<T>()
		{
			unsafe
			{
				if (typeof(T).IsValueType || typeof(T).IsEnum)
				{
					return Function.ObjectFromNative<T>((ulong*)storage.ToPointer());
				}
				else
				{
					return (T)Function.ObjectFromNative(typeof(T), (ulong*)storage.ToPointer());
				}
			}
		}
	}

	/// <summary>
	/// A static class which handles script function execution.
	/// </summary>
	public static class Function
	{
		const int MAX_ARG_COUNT = 32;

		/// <summary>
		/// Calls the specified native script function and returns its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the native script function.</param>
		/// <param name="arguments">A list of input and output arguments to pass to the native script function.</param>
		/// <returns>The return value of the native.</returns>
		public static T Call<T>(Hash hash, params InputArgument[] arguments)
		{
			unsafe
			{
				int argCount = arguments.Length <= MAX_ARG_COUNT ? arguments.Length : MAX_ARG_COUNT;
				var argPtr = stackalloc ulong[argCount];

				for (int i = 0; i < argCount; ++i)
				{
					argPtr[i] = arguments[i]._data;
				}

				var res = SHVDN.NativeFunc.Invoke((ulong)hash, argPtr, argCount);
				return ReturnValueFromNativeIfNotNull<T>(res);
			}
		}

		#region Call with Return Value Overloads with Normal InputArgument Paramaters
		/// <summary>
		/// Calls the specified native script function and returns its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the script function.</param>
		/// <returns>The return value of the native.</returns>
		public static T Call<T>(Hash hash)
		{
			unsafe
			{
				var res = SHVDN.NativeFunc.Invoke((ulong)hash, null, 0);
				return ReturnValueFromNativeIfNotNull<T>(res);
			}
		}
		/// <summary>
		/// Calls the specified native script function and ignores its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the script function.</param>
		/// <param name="argument">The input or output argument to pass to the native script function.</param>
		/// <returns>The return value of the native.</returns>
		public static T Call<T>(Hash hash, InputArgument argument)
		{
			unsafe
			{
				const int argCount = 1;
				var argPtr = stackalloc ulong[argCount];

				argPtr[0] = argument._data;

				var res = SHVDN.NativeFunc.Invoke((ulong)hash, argPtr, argCount);
				return ReturnValueFromNativeIfNotNull<T>(res);
			}
		}
		/// <summary>
		/// Calls the specified native script function and ignores its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the script function.</param>
		/// <param name="argument0">The 1st input or output argument to pass to the native script function.</param>
		/// <param name="argument1">The 2nd input or output argument to pass to the native script function.</param>
		/// <returns>The return value of the native.</returns>
		public static T Call<T>(Hash hash, InputArgument argument0, InputArgument argument1)
		{
			unsafe
			{
				const int argCount = 2;
				var argPtr = stackalloc ulong[argCount];

				argPtr[0] = argument0._data;
				argPtr[1] = argument1._data;

				var res = SHVDN.NativeFunc.Invoke((ulong)hash, argPtr, argCount);
				return ReturnValueFromNativeIfNotNull<T>(res);
			}
		}
		/// <summary>
		/// Calls the specified native script function and ignores its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the script function.</param>
		/// <param name="argument0">The 1st input or output argument to pass to the native script function.</param>
		/// <param name="argument1">The 2nd input or output argument to pass to the native script function.</param>
		/// <param name="argument2">The 3rd input or output argument to pass to the native script function.</param>
		/// <returns>The return value of the native.</returns>
		public static T Call<T>(Hash hash, InputArgument argument0, InputArgument argument1, InputArgument argument2)
		{
			unsafe
			{
				const int argCount = 3;
				var argPtr = stackalloc ulong[argCount];

				argPtr[0] = argument0._data;
				argPtr[1] = argument1._data;
				argPtr[2] = argument2._data;

				var res = SHVDN.NativeFunc.Invoke((ulong)hash, argPtr, argCount);
				return ReturnValueFromNativeIfNotNull<T>(res);
			}
		}
		/// <summary>
		/// Calls the specified native script function and ignores its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the script function.</param>
		/// <param name="argument0">The 1st input or output argument to pass to the native script function.</param>
		/// <param name="argument1">The 2nd input or output argument to pass to the native script function.</param>
		/// <param name="argument2">The 3rd input or output argument to pass to the native script function.</param>
		/// <param name="argument3">The 4th input or output argument to pass to the native script function.</param>
		/// <returns>The return value of the native.</returns>
		public static T Call<T>(Hash hash, InputArgument argument0, InputArgument argument1, InputArgument argument2, InputArgument argument3)
		{
			unsafe
			{
				const int argCount = 4;
				var argPtr = stackalloc ulong[argCount];

				argPtr[0] = argument0._data;
				argPtr[1] = argument1._data;
				argPtr[2] = argument2._data;
				argPtr[3] = argument3._data;

				var res = SHVDN.NativeFunc.Invoke((ulong)hash, argPtr, argCount);
				return ReturnValueFromNativeIfNotNull<T>(res);
			}
		}
		/// <summary>
		/// Calls the specified native script function and ignores its return value.
		/// </summary>
		/// <param name="hash">The hashed name of the script function.</param>
		/// <param name="argument0">The 1st input or output argument to pass to the native script function.</param>
		/// <param name="argument1">The 2nd input or output argument to pass to the native script function.</param>
		/// <param name="argument2">The 3rd input or output argument to pass to the native script function.</param>
		/// <param name="argument3">The 4th input or output argument to pass to the native script function.</param>
		/// <param name="argument4">The 5th input or output argument to pass to the native script function.</param>