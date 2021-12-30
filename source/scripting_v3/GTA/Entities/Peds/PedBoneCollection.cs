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
	public class PedBoneCollection : EntityBoneCollection, IEnumerable<PedBone>
	{
		public new class Enumerator : IEnumerator<PedBone>
		{
			#region Fields
			readonly PedBoneCollection collection;
			int currentIndex = -1; // Skip the CORE bone index(-1)
			#endregion

			public Enumerator(PedBoneCollection collection)
			{
				this.collection = collection;
			}

			public PedBone Current => collection[currentIndex];

			object IEnumerator.Current => collection[currentIndex];

			public void Reset()
			{
				currentIndex = -1;
			}

			public bool MoveNext()
			{
				return ++currentIndex < collection.Count;
			}

			void IDisposable.Dispose()
			{
			}
		}

		internal PedBoneCollection(Ped owner) : base(owner)
		{
		}

		/// <summary>
		/// Gets the <see cref="PedBone"/> with the specified <paramref name="boneId"/>.
		/// </summary>
		/// <param name="boneId">The bone Id.</param>
		public PedBone this[Bone boneId]
		{
			get => new PedBone((Ped)_owner, boneId);
		}

		/// <summary>
		/// Gets the <see cref="PedBone"/> at the specified bone index.
		/// </summary>
		/// <param name="boneIndex">The bone index.</param>
		public new PedBone this[int boneIndex]
		{
			get => new PedBone((Ped)_owner, boneIndex);
		}

		/// <summary>
		/// <para>
		/// Gets the <see cref="PedBone"/> with the specified bone name. Use this overload only if you know a correct bone tag.
		/// If the corresponding bone is not found, the <see cref="EntityBone.Index"/> of the returned instance will return <c>-1</c>.
		/// </para>
		/// <para>
		/// This method will try to find the corresponding bone by the hash calcutated with <c>(ElfHashUppercased(string) % 0xFE8F + 0x170)</c>,
		/// where <c>ElfHashUppercased(string)</c> will convert