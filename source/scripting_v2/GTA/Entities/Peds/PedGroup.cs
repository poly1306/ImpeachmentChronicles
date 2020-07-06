//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using GTA.Native;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GTA
{
	public class PedGroup : IEquatable<PedGroup>, IEnumerable<Ped>, IHandleable, IDisposable
	{
		public class enumerator : IEnumerator<Ped>
		{
			#region Fields
			int index;
			readonly PedGroup group;
			#endregion

			public enumerator(PedGroup group)
			{
				index = -2;
				this.group = group;
			}

			public virtual Ped Current
			{
				get; private set;
			}

			public object Current2 => Current;

			object IEnumerator.Current => Current;

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			protected virtual void Dispose(bool disposing)
			{
			}

			public virtual void Reset()
			{
			}

			public virtual bool MoveNext()
			{
				if (index++ < (group.MemberCount - 1))
				{
					Current = index < 0 ? group.Leader : group.GetMember(index);

					if (Entity.Exists(Current))
					{
						return true;
					}

					return MoveNext();
				}

				return false;
			}
		}

		public PedGroup() : this(Function.Call<int>(Hash.CREATE_GROUP, 0))
		{
		}
		public PedGroup(int handle)
		{
			Handle = handle;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Function.Call(Hash.REMOVE_GROUP, Handle);
			}
		}

		public int Handle
		{
			get;
		}

		public int MemberCount
		{
			get
			{
				int count, val1;
				unsafe
				{
					Function.Call(Hash.GET_GROUP_SIZE, Handle, &val1, &count);
				}
				return count;
			}
		}

		public float SeparationRange
		{
			set => Function.Call(Hash.SET_GROUP_SEPARATION_RANGE, Handle, value);
		}

		public FormationType Formation