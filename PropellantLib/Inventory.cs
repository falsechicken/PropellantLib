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
		public static bool ClearItems(UnturnedPlayer _player)
		{
			bool bSuccess = false;

			try
			{
				_player.Player.equipment.dequip();

				for (byte p = 0; p < PlayerInventory.PAGES; p++)
				{
					byte itemc = _player.Player.inventory.getItemCount(p);

					if (itemc > 0)
					{
						for (byte p1 = 0; p1 < itemc; p1++)
						{

							_player.Player.inventory.removeItem(p, 0);
						
						}
					}
				}

				_player.Player.SteamChannel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						(byte)0,
						(byte)0,
						new byte[0]
					});

				_player.Player.SteamChannel.send("tellSlot", ESteamCall.ALL, ESteamPacket.UPDATE_RELIABLE_BUFFER, new object[]
					{
						(byte)1,
						(byte)0,
						new byte[0]
					});

				bSuccess = true;
			
			}

			catch (Exception e)
			{

				Console.Write(e);
			
			}

			return bSuccess;
		}

		/**
		 * Remove all clothing equiped on a player.
		 **/
		public static bool ClearClothes(UnturnedPlayer _player)
		{
			bool bSuccess = false;

			try
			{
				_player.Player.Clothing.askWearBackpack(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				_player.Player.Clothing.askWearGlasses(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				_player.Player.Clothing.askWearHat(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				_player.Player.Clothing.askWearMask(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				_player.Player.Clothing.askWearPants(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				_player.Player.Clothing.askWearShirt(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				_player.Player.Clothing.askWearVest(0, 0, new byte[0]);
				for (byte p2 = 0; p2 < _player.Player.Inventory.getItemCount(2); p2++)
				{
					_player.Player.Inventory.removeItem(2, 0);
				}

				bSuccess = true;
			}
			catch (Exception e)
			{

				Console.Write(e);

			}

			return bSuccess;
		}

		/**
		 * Check if a player's inventory contains the specified item.
		 **/
		public static bool DoesPlayerHaveItem(ushort _itemID, UnturnedPlayer _player)
		{
			foreach (Items item in _player.Inventory.Items)
			{
				if (item.getItem(0).item.id == _itemID) return true;
			}

			return false;
		}

		/**
		 * Get the number of items of a specified type in a player's
		 * inventory.
		 **/
		public static ushort GetItemCount(ushort _itemID, UnturnedPlayer _player)
		{
			foreach (Items item in _player.Inventory.Items)
			{
				if (item.getItem(0).item.id == _itemID)
				{
					return item.getItem(0).item.Amount;
				}
			}

			return 0;
		}
			
		//TODO: Fill in.
		/**
		 * Drop all items in a player's inventory on the ground.
		 **/
		public static bool DropItems(UnturnedPlayer _player)
		{
			return false;	
		}

		//TODO: Fill in.
		/**
		 * Drop all the clothing equiped by a player on the ground.
		 **/
		public static bool DropClothes(UnturnedPlayer _player)
		{
			return false;
		}
	}
}