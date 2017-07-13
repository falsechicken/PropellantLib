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
using System.Collections.Generic;

using Rocket.Unturned.Player;

using SDG.Unturned;
using UnityEngine;

namespace FC.PropellantLib
{
	/**
	 * Provides utility functions dealing with player inventories.
	 **/
	public static class Inventory
	{
		/**
		 * Quick reference for inventory sections.
		 */
		public const byte
		SECTION_HANDS = 2,
		SECTION_BACKPACK = 3,
		SECTION_VEST = 4,
		SECTION_SHIRT = 5,
		SECTION_PANTS = 6;
	
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
		 * Forces an item into the players inventory. If there is no room it
		 * will be dropped on the ground.
		 */
		public static void ForceAddItem(Item _item, UnturnedPlayer _player)
		{
			_player.Inventory.forceAddItem (_item, true);
		}

		/**
		 * Forces a list of items into the players inventory. If there is no room it
		 * will be dropped on the ground.
		 */
		public static void ForceAddItems(List<Item> _items, UnturnedPlayer _player)
		{
			foreach (Item item in _items) {
				_player.Inventory.forceAddItem (item, true);
			}

		}

		/**
		 * Remove all items of a specified type from a players inventory.
		 */
		public static void RemoveItemsOfType(ushort _itemID, UnturnedPlayer _player)
		{
			List<Vector2> itemsToRemove = new List<Vector2> ();

			for (byte page = 0; page < 8; page++)
			{
				itemsToRemove.Clear ();

				var items = _player.Inventory.getItemCount(page);

				for (byte index = 0; index < items; index++)
				{
					if (_player.Inventory.getItem (page, index).item.id == _itemID) {
						itemsToRemove.Add ( new Vector2 (_player.Inventory.getItem (page, index).x, _player.Inventory.getItem (page, index).y));
					}
				}

				foreach (Vector2 loc in itemsToRemove) {
					_player.Inventory.removeItem (page,
						_player.Inventory.getIndex(page, (byte)loc.x, (byte)loc.y));
				}
			}
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
		 * Returns true if the inventory section provided contains the item.
		 */
		public static bool DoesInventorySectionContainItem(byte _section, ushort _itemID, UnturnedPlayer _player) {
			var items = _player.Inventory.getItemCount(_section);

			for (byte index = 0; index < items; index++)
			{
				if (_player.Inventory.getItem (_section, index).item.id == _itemID)
					return true;
			}

			return false;
		}

		/**
		 * Returns a nested dictionary containing all the items in the players inventory. The first dictionary
		 * key refers to the inventory section. The second is the items index position in the inventory section followed
		 * by the ItemJar containing the item.
		 */
		public static Dictionary<byte, Dictionary<byte, ItemJar>> GetInventory(UnturnedPlayer _player)
		{
			Dictionary<byte, Dictionary<byte, ItemJar>> allItems = new Dictionary<byte, Dictionary<byte, ItemJar>>();

			for (byte page = 0; page < 8; page++)
			{
				allItems [page] = new Dictionary<byte, ItemJar> ();

				var items = _player.Inventory.getItemCount(page);
			
				for (byte index = 0; index < items; index++)
				{
					allItems [page].Add (index, _player.Inventory.getItem (page, index));
				}
			}

			return allItems;
		}

		/**
		 * Returns a dictionary of bytes & ItemJars containing all of the items of the provided section and id. The
		 * byte is the itemjars position in the sections inventory list.
		 */
		public static Dictionary<byte,ItemJar> GetItemsFromSection(byte _section, ushort _itemID, UnturnedPlayer _player) {

			Dictionary<byte,ItemJar> itemsList = new Dictionary<byte,ItemJar> ();

			var items = _player.Inventory.getItemCount(_section);

			for (byte index = 0; index < items; index++)
			{
				if (_player.Inventory.getItem (_section, index).item.id == _itemID)
					itemsList[index] = _player.Inventory.getItem(_section, index);
			}

			return itemsList;
		}

		/**
		 * Drop all items in a player's inventory on the ground.
		 **/
		public static bool DropItems(UnturnedPlayer _player)
		{
			throw new NotImplementedException("Inventory.DropItems not implemented yet.");

			for (byte page = 0; page < 8; page++)
			{
				var items = _player.Inventory.getItemCount(page);

				for (byte index = 0; index < items; index++)
				{
					
				}
			}

			return true;
		}
			

		private static void removeUnequipped(UnturnedPlayer _player)
		{
			for (byte i = 0; i < _player.Inventory.getItemCount (2); i++) {
				_player.Inventory.removeItem (2, 0);
			}
		}
	}
}