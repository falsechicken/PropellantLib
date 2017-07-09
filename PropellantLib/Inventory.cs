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
	/**
	 * Provides utility functions dealing with player inventories.
	 **/
	public static class Inventory
	{

		/**
		 * Remove all items from a player's inventory.
		 **/
		public static bool Clear(UnturnedPlayer _player)
		{

			// Loops over each of the player's inventory pages
			for (byte page = 0; page < 8; page++)
			{
				// gets the item count for the current inventory page its looking in
				var items = _player.Inventory.getItemCount(page);
				// loop over all items in current inventory page
				for (byte index = 0; index < items; index++)
				{
					// remove the item
					_player.Inventory.removeItem(page, 0);
				}
			}

			// "Remove "models" of items from player "body""
			_player.Player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER,
				(byte) 0, (byte) 0, new byte[0]);
			_player.Player.channel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER,
				(byte) 1, (byte) 0, new byte[0]);

			// Unequip & remove from inventory
			_player.Player.clothing.askWearBackpack(0, 0, new byte[0], true);
			removeUnequipped (_player);

			_player.Player.clothing.askWearGlasses(0, 0, new byte[0], true);
			removeUnequipped (_player);

			_player.Player.clothing.askWearHat(0, 0, new byte[0], true);
			removeUnequipped (_player);

			_player.Player.clothing.askWearPants(0, 0, new byte[0], true);
			removeUnequipped (_player);

			_player.Player.clothing.askWearMask(0, 0, new byte[0], true);
			removeUnequipped (_player);

			_player.Player.clothing.askWearShirt(0, 0, new byte[0], true);
			removeUnequipped (_player);

			_player.Player.clothing.askWearVest(0, 0, new byte[0], true);
			removeUnequipped (_player);

			return true;
		}
			
		/**
		 * Check if a player's inventory contains the specified item.
		 **/
		public static bool DoesPlayerHaveItem(ushort _itemID, UnturnedPlayer _player)
		{
			for (byte page = 0; page < 8; page++)
			{
				var items = _player.Inventory.getItemCount(page);

				for (byte index = 0; index < items; index++)
				{
					if (_player.Inventory.getItem (page, index).item.id == _itemID)
						return true;
				}
			}

			return false;
		}

		/**
		 * Get the number of items of a specified type in a player's
		 * inventory.
		 **/
		public static ushort GetItemCount(ushort _itemID, UnturnedPlayer _player)
		{
			byte count = 0;

			for (byte page = 0; page < 8; page++)
			{
				var items = _player.Inventory.getItemCount(page);

				for (byte index = 0; index < items; index++)
				{
					if (_player.Inventory.getItem (page, index).item.id == _itemID)
						count++;
				}
			}

			return count;
		}

		/**
		 * Drop all items in a player's inventory on the ground.
		 **/
		public static bool DropItems(UnturnedPlayer _player)
		{
			return false;
		}
			

		private static void removeUnequipped(UnturnedPlayer _player)
		{
			for (byte i = 0; i < _player.Inventory.getItemCount (2); i++) {
				_player.Inventory.removeItem (2, 0);
			}
		}
	}
}