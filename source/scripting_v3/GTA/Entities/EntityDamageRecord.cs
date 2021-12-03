//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

namespace GTA
{
	public readonly struct EntityDamageRecord
	{
		internal EntityDamageRecord(Entity victim, Entity attacker, WeaponHash weaponHash, int gameTime)
		{
			Victim = victim;
			Attacker = attacker;
			WeaponHash = weaponHash;
			GameTime = gameTime;
		}

		/// <summary>
		/// Gets the victim <see cref="Entity" />.
		/// </summary>
		public Entity Victim
		{
			get;
		}

		/// <summary>
		/// Gets the attacker <see cref="Entity" />. Can be <c>null</c>.
		/// </summary>
		public Entity Attacker
		{
			get;
		}

		/// <summary>
		/// Gets the game time when the <see cref="Victim" /> took damage.
		/// </summary>
		public int GameTime
		{
			get;
		}

		/// <summary>
		/// Gets the weapon hash what the <see cref="Victim" /> took damage with.
		/// </summary>
		public WeaponHash WeaponHash
		{
			get;
		}

		public void Deconstruct(out Entity attacker, out WeaponHash weaponHash, out int gameTime)
		{
			attacker = Attacker;
			weaponHash = WeaponHash;
			gameTime = GameTime;
		}

		public void Deconstruct(out Entity victim, out Entity attacker, out WeaponHash weaponHash, out int gameTime)
		{
			victim = Victim;
			attacker = Attacker;
			weaponHash = WeaponHash;
			gameTime = GameTime;
		}

		/// <summary>
		/// Determines if <paramref name="entityD