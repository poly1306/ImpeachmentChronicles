//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace GTA
{
	public sealed class WeaponComponentCollection
	{
		#region Fields
		readonly Ped _owner;
		readonly Weapon _weapon;
		readonly Dictionary<WeaponComponentHash, WeaponComponent> _weaponComponents = new Dictionary<WeaponComponentHash, WeaponComponent>();
		readonly WeaponComponentHash[] _components;
		readonly static WeaponComponent _invalidComponent = new WeaponComponent(null, null, WeaponComponentHash.Invalid);
		#endregion

		internal WeaponComponentCollection(Ped owner, Weapon weapon)
		{
			_owner = owner;
			_weapon = weapon;
			_components = GetComponentsFromHash(weapon.Hash);
		}

		public WeaponComponent this[int index]
		{
			get
			{
				WeaponComponent component = null;
				if (index >= 0 && index < Count)
				{
					WeaponComponentHash componentHash = _components[index];

					if (!_weaponComponents.TryGetValue(componentHash, out component))
					{
						component = new WeaponComponent(_owner, _weapon, componentHash);
						_weaponComponents.Add(componentHash, component);
					}
					return component;
				}
				else
				{
					return _invalidComponent;
				}
			}
		}

		public WeaponComponent this[WeaponComponentHash componentHash]
		{
			get
			{
				if (_components.Contains(componentHash))
				{
					WeaponComponent component = null;
					if (!_weaponComponents.TryGetValue(componentHash, out component))
					{
						component = new WeaponComponent(_owner, _weapon, componentHash);
						_weaponComponents.Add(componentHash, component);
					}

					return component;
				}
				else
				{
					return _invalidComponent;
				}
			}
		}

		/// <summary>
		/// Gets the number of compatible components.
		/// </summary>
		public int Count => _components.Length;

		public IEnumerator<WeaponComponent> GetEnumerator()
		{
			WeaponComponent[] AllComponents = new WeaponComponent[Count];
			for (int i = 0; i < Count; i++)
			{
				AllComponents[i] = this[_components[i]];
			}
			return (AllComponents as IEnumerable<WeaponComponent>).GetEnumerator();
		}

		/// <summary>
		/// Gets the clip component at the index.
		/// </summary>
		/// <param name="index">The index of the clip component subset of all the weapon component array.</param>
		/// <returns>
		/// A <see cref="WeaponComponent"/> instance if the <see cref="WeaponComponent"/> at the <paramref name="index"/> of the clip component subset is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		public WeaponComponent GetClipComponent(int index)
		{
			foreach (var component in this)
			{
				if (component.AttachmentPoint == WeaponAttachmentPoint.Clip ||
					component.AttachmentPoint == WeaponAttachmentPoint.Clip2)
				{
					if (index-- == 0)
					{
						return component;
					}
				}
			}
			return _invalidComponent;
		}

		/// <summary>
		/// Gets the number of compatible clip components.
		/// </summary>
		public int ClipVariationsCount
		{
			get
			{
				int count = 0;
				foreach (var component in this)
				{
					if (component.AttachmentPoint == WeaponAttachmentPoint.Clip ||
					component.AttachmentPoint == WeaponAttachmentPoint.Clip2)
					{
						count++;
					}
				}
				return count;
			}
		}

		/// <summary>
		/// Gets the scope component at the index.
		/// </summary>
		/// <param name="index">The index of the scope component subset of all the weapon component array.</param>
		/// <returns>
		/// A <see cref="WeaponComponent"/> instance if the <see cref="WeaponComponent"/> at the <paramref name="index"/> of the scope component subset is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		public WeaponComponent GetScopeComponent(int index)
		{
			foreach (var component in this)
			{
				if (component.AttachmentPoint == WeaponAttachmentPoint.Scope ||
					component.AttachmentPoint == WeaponAttachmentPoint.Scope2)
				{
					if (index-- == 0)
					{
						return component;
					}
				}
			}
			return _invalidComponent;
		}

		/// <summary>
		/// Gets the number of compatible scope components.
		/// </summary>
		public int ScopeVariationsCount
		{
			get
			{
				int count = 0;
				foreach (var component in this)
				{
					if (component.AttachmentPoint == WeaponAttachmentPoint.Scope ||
					component.AttachmentPoint == WeaponAttachmentPoint.Scope2)
					{
						count++;
					}
				}
				return count;
			}
		}

		/// <summary>
		/// Gets the barrel component at the index.
		/// </summary>
		/// <param name="index">The index of the barrel component subset of all the weapon component array.</param>
		/// <returns>
		/// A <see cref="WeaponComponent"/> instance if the <see cref="WeaponComponent"/> at the <paramref name="index"/> of the barrel component subset is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		public WeaponComponent GetBarrelComponent(int index)
		{
			foreach (var component in this)
			{
				if (component.AttachmentPoint == WeaponAttachmentPoint.Barrel)
				{
					if (index-- == 0)
					{
						return component;
					}
				}
			}
			return _invalidComponent;
		}

		/// <summary>
		/// Gets the number of compatible barrel components.
		/// </summary>
		public int BarrelVariationsCount
		{
			get
			{
				int count = 0;
				foreach (var component in this)
				{
					if (component.AttachmentPoint == WeaponAttachmentPoint.Barrel)
					{
						count++;
					}
				}
				return count;
			}
		}

		/// <summary>
		/// Gets the suppressor or muzzle brake component at the index.
		/// </summary>
		/// <param name="index">The index of the subset of the suppressor and muzzle brake components of all the weapon component array.</param>
		/// <returns>
		/// A <see cref="WeaponComponent"/> instance if the <see cref="WeaponComponent"/> at the <paramref name="index"/> of the subset of the suppressor and muzzle brake components is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		public WeaponComponent GetSuppressorOrMuzzleBrakeComponent(int index)
		{
			foreach (var component in this)
			{
				if (component.AttachmentPoint == WeaponAttachmentPoint.Supp ||
					component.AttachmentPoint == WeaponAttachmentPoint.Supp2)
				{
					if (index-- == 0)
					{
						return component;
					}
				}
			}
			return _invalidComponent;
		}

		/// <summary>
		/// Gets the number of compatible suppressor and muzzle brake components.
		/// </summary>
		public int SuppressorAndMuzzleBrakeVariationsCount
		{
			get
			{
				int count = 0;
				foreach (var component in this)
				{
					if (component.AttachmentPoint == WeaponAttachmentPoint.Supp ||
						component.AttachmentPoint == WeaponAttachmentPoint.Supp2)
					{
						count++;
					}
				}
				return count;
			}
		}

		/// <summary>
		/// Gets the component for <see cref="WeaponAttachmentPoint.GunRoot"/> at the index.
		/// </summary>
		/// <param name="index">The index of the components for <see cref="WeaponAttachmentPoint.GunRoot"/> subset of all the weapon component array.</param>
		/// <returns>
		/// A <see cref="WeaponComponent"/> instance if the <see cref="WeaponComponent"/> at the <paramref name="index"/> of the components for <see cref="WeaponAttachmentPoint.GunRoot"/> is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		public WeaponComponent GetGunRootComponent(int index)
		{
			foreach (var component in this)
			{
				if (component.AttachmentPoint == WeaponAttachmentPoint.GunRoot)
				{
					if (index-- == 0)
					{
						return component;
					}
				}
			}
			return _invalidComponent;
		}

		/// <summary>
		/// Gets the number of compatible components for <see cref="WeaponAttachmentPoint.GunRoot"/>.
		/// </summary>
		public int GunRootVariationsCount
		{
			get
			{
				int count = 0;
				foreach (var component in this)
				{
					if (component.AttachmentPoint == WeaponAttachmentPoint.GunRoot)
					{
						count++;
					}
				}
				return count;
			}
		}

		/// <summary>
		/// Gets the suppressor component.
		/// </summary>
		/// <returns>
		/// The <see cref="WeaponComponent"/> instance if the suppressor component is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		// This should get the suppressor instance, but we could add additional validations by checking if the weapon component info class is CWeaponComponentSuppressorInfo
		public WeaponComponent GetSuppressorComponent()
		{
			foreach (var component in this)
			{
				if (component.AttachmentPoint == WeaponAttachmentPoint.Supp ||
					component.AttachmentPoint == WeaponAttachmentPoint.Supp2)
				{
					return component;
				}
			}
			return _invalidComponent;
		}

		/// <summary>
		/// Gets the flashlight component.
		/// </summary>
		/// <returns>
		/// The <see cref="WeaponComponent"/> instance if the flashlight component is found;
		/// otherwise, the <see cref="WeaponComponent"/> instance representing the invalid component.
		/// </returns>
		public WeaponComponent GetFlashLightComponent()
		{
			foreach (var component in this)
			{
				// COMPONENT_AT_RAILCOVER_01 is the only component that attaches the flashlight position other than actual