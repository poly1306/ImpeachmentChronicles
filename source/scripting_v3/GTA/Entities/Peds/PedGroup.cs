
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
	public sealed class PedGroup : PoolObject, IEnumerable<Ped>, IDisposable
	{
		public sealed class Enumerator : IEnumerator<Ped>
		{
			#region Fields
			readonly PedGroup collection;
			Ped current;
			int currentIndex = -2;
			#endregion

			public Enumerator(PedGroup group)
			{
				collection = group;
			}

			public Ped Current => current;

			object IEnumerator.Current => current;

			public void Reset()
			{
			}

			public bool MoveNext()
			{
				if (currentIndex++ < (collection.MemberCount - 1))
				{
					current = currentIndex < 0 ? collection.Leader : collection.GetMember(currentIndex);

					if (current != null)
					{
						return true;
					}

					return MoveNext();
				}

				return false;
			}

			void IDisposable.Dispose()
			{
			}
		}

		public PedGroup() : base(Function.Call<int>(Hash.CREATE_GROUP, 0))
		{
		}
		public PedGroup(int handle) : base(handle)
		{
		}
