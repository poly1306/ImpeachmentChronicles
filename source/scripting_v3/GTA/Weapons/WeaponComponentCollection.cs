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
			_components = GetComponentsFromHash(wea