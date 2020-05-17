﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    /*
     * A special subset of game objects that represent what the player is carrying with them.
     * Note that when an object is being carried by the player, it's room location will be set to '-1'
     * TODO:  Although this may change in the future, since the object state can be inventory.
     * I think I need to deprecate "INVENTORY" as one of the object status options.  
     * Perhaps it is fine to have it stored there too, but it seems redundant.  Oh well, it helps with visibility
     * to be able to NOT display objects in the room that are carried by the player.  Probably keep it as is.
     */ 
    class Inventory
    {
       //FIXME:  Deprecate this!  But learn what you find first!
        public List<GameItem> InvItems { get; set; }

        //what other properties do we need?


        public Inventory()
        {
            //default constructor

            //There has GOT to be a better way to do this!  But it needs to be created at game start!
            GameItem.gameItems.Add(new GameItem(RoomLocation.LocInventory, "MANGO_FOOD", "a juicy mango", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "this delectable fruit has the power to restore strength to the weak!" }, true, true, 1, 1));
            GameItem.gameItems.Add(new GameItem(RoomLocation.LocInventory, "FLASHLIGHT", "small LED flashlight", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "a small, but bright, LED flashlight" }, true, true, 3, 2));
            GameItem.gameItems.Add(new GameItem(RoomLocation.LocInventory, "WATER_BOTTLE", "water bottle", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "a cheap, plastic water bottle" }, true, true, 2, 2));
        }


        //FIXME:  The mat was NOT appearing when adding it to the inventory, and I finally realized
        //   that it was because I had not added it to this extra list.  In reality, we don't want to
        //   maintain a SEPARATE list for inventory... it's state and location value should be sufficient.
        //   So I need to kill this list, and then simply add them to the master game item array.
        //   For that matter, I don't want to hard-code it to a specific array element, 
        //   so it also NEEDS to become a list me thinks! :-)

        //Auto-initialize Inventory (only gets called on creation, so I should be able to add
        //      inventory items to the master list of GameItems, stored in the GameItem.cs class.
        //      The players inventory won't be initialized until the player enters the command to display inventory.

        public static  List<GameItem> playerInv = new List<GameItem>
        {
        /* Ex: new GameItem(0, "MAT", "old door mat", (int)ObjectState.VISIBLE, 
         *         new Dictionary<int, string> { [(int)ObjectState.VISIBLE]="Visible Description" },
         *         true, true, 8, 5);
         */

            //There has GOT to be a better way to do this!
            //GameItem.gameItems.Add( new GameItem( RoomLocation.LocInventory, "MANGO_FOOD", "a juicy mango", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "this delectable fruit has the power to restore strength to the weak!" }, true, true, 1, 1));
            //GameItem.gameItems.Add( new GameItem(RoomLocation.LocInventory, "FLASHLIGHT", "small LED flashlight", (int) GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "a small, but bright, LED flashlight" }, true, true, 3, 2));
            //GameItem.gameItems.Add( new GameItem(RoomLocation.LocInventory, "WATER_BOTTLE", "water bottle", (int) GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "a cheap, plastic water bottle" }, true, true, 2, 2));

        //GameItem gameItem3 = new GameItem();
        
        //gameItem3.
            //new GameItem(RoomLocation.LocInventory, "MANGO_FOOD",   "a juicy mango",        (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE]="this delectable fruit has the power to restore strength to the weak!"}, true, true, 1, 1),
            //new GameItem(RoomLocation.LocInventory, "FLASHLIGHT",   "small LED flashlight", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE]="a small, but bright, LED flashlight"}, true, true, 3, 2),
            //new GameItem(RoomLocation.LocInventory, "WATER_BOTTLE", "water bottle",         (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE]="a cheap, plastic water bottle"}, true, true, 2, 2)
                     //Later on, add logic to track how full the water bottle is, which decreases as you drink from it.  You can refill it though.
        };


        /*
         * Display a formatted list of items in the player's inventory, using the detailed name.
         */
        public static void DisplayInventory()
        {
            Console.WriteLine("\n YOU ARE CARRYING:");
            int inventoryItemCount = 0;
            foreach (var item in GameItem.gameItems)
            {
                if (item.LocationIndex.Equals(RoomLocation.LocInventory))
                {
                    Console.WriteLine($"    > {item.Name}");
                    inventoryItemCount++;
                }                
            }
            if (inventoryItemCount == 0)
            {
                Console.WriteLine("    > Nothing");
            }
            //GameItem.gameItems.AddRange
            {
                //There has GOT to be a better way to do this!
                //GameItem.gameItems.Add( new GameItem( RoomLocation.LocInventory, "MANGO_FOOD", "a juicy mango", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "this delectable fruit has the power to restore strength to the weak!" }, true, true, 1, 1));
                //GameItem.gameItems.Add( new GameItem( RoomLocation.LocInventory, "FLASHLIGHT", "small LED flashlight", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "a small, but bright, LED flashlight" }, true, true, 3, 2));
                //GameItem.gameItems.Add( new GameItem( RoomLocation.LocInventory, "WATER_BOTTLE", "water bottle", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE] = "a cheap, plastic water bottle" }, true, true, 2, 2));
            }


        }


        public static bool ContainsItem(string itemSearch)
        {
            bool itemFound = false;
            if (itemSearch.Length >= 3)
            {
                foreach (var invItem in playerInv)
                {
                    itemFound = invItem.ItemId.Contains(itemSearch);
                    if (itemFound) { break; }
                }
            }
            return itemFound;
        }


    }



}
