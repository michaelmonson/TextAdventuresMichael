using System;
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
        public List<GameItem> InvItems { get; set; }

        //what other properties do we need?


        public Inventory()
        {
            //default constructor
        }


        //Auto-initialize Inventory
        public static List<GameItem> playerInv = new List<GameItem>
        {
            /* Ex: new GameItem(0, "MAT", "old door mat", (int)ObjectState.VISIBLE, 
             *         new Dictionary<int, string> { [(int)ObjectState.VISIBLE]="Visible Description" },
             *         true, true, 8, 5);
             */
            new GameItem(-1, "MANGO_FOOD",   "mango",      "a juicy mango", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { }, true, true, 1, 1),
            new GameItem(-1, "FLASHLIGHT",   "flashlight", "small LED flashlight", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE]="a small, but bright, LED flashlight"}, true, true, 3, 2),
            new GameItem(-1, "WATER_BOTTLE", "bottle",     "water bottle", (int)GameItem.ObjectState.INVENTORY, new Dictionary<int, string> { [(int)GameItem.ObjectState.VISIBLE]="a cheap, plastic water bottle"}, true, true, 2, 2),
                                    //Later on, add logic to track how full the water bottle is, which decreases as you drink from it.  You can refill it though.
        };


        /*
         * Display a formatted list of items in the player's inventory, using the detailed name.
         */
        public static void DisplayInventory()
        {
            Console.WriteLine("\n YOU ARE CARRYING:");
            foreach (var invItem in playerInv)
            {
                Console.WriteLine($"    > {invItem.NameDetailed}");
            }
        }


        public static bool ContainsItem(string itemSearch)
        {
            bool itemFound = false;
            foreach (var invItem in playerInv)
            {
                itemFound = invItem.Name.Contains(itemSearch.ToLower());
                break;
            }
            return itemFound;
        }


    }



}
