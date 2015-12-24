//
//  Player.cs
//
//  Author: False_Chicken 
//  Contact: jmdevsupport@gmail.com
//
//  Copyright (c) 2015 False_Chicken
//
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//

using System;

using Rocket.Unturned.Player;
using SDG.Unturned;

namespace FC.PropellantLib
{
	public static class Player
	{
		#region KILL PLAYER

		/**
		 * Kill the specified player.
		 **/
		public static void Kill(UnturnedPlayer _player)
		{
			_player.Damage(255, _player.Position, EDeathCause.SUICIDE, ELimb.SKULL, _player.CSteamID);
		}

		/**
		 * Kill the specified player.
		 **/
		public static void Kill(UnturnedPlayer _player, EDeathCause _deathCause)
		{
			_player.Damage(255, _player.Position, _deathCause, ELimb.SKULL, _player.CSteamID);
		}

		#endregion

	}
}

