using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class GameItem
    {

        public int LocationIndex { get; set; }  //Location that the object first appears
        public string ItemId { get; set; }      //Unique alphanumeric CONSTANT that is an ID for the item
        public string Name { get; set; }        //Item Name
        public int StateValue { get; set; }     //Object state.  Correspondes to enum values.
        public Dictionary<int, string> StateDescription { get; set; } //An array of descriptions for different states/conditions 
        public bool Movable { get; set; }       //Item can be moved, but not taken
        public bool Luggable { get; set; }      //Item can be taken
        public int Weight { get; set; }         //Max: 50 pounds. Impacts how much the player can carry.
        public int Size { get; set; }           //Max: 20 size units (arbitrary). Large, cumbersome loads are difficult.

        public GameItem()
        {
        }

        public GameItem(int locationIndex, string itemId, string name,
                    int stateValue, Dictionary<int, string> stateDescription,
                    bool movable, bool luggable, int weight, int size)
        {
            LocationIndex = locationIndex;
            ItemId = itemId;
            Name = name;
            StateValue = stateValue;
            StateDescription = stateDescription;
            Movable = movable;
            Luggable = luggable;
            Weight = weight;
            Size = size;
        }

        public enum ObjectState
        {
            LOCKED = 4,
            CLOSED = 3,
            DAMAGED = 2,
            VISIBLE = 1,    //Also in an 'open' state.  When visible, it can be seen by the playerin whichever room it is in.
            INVENTORY = 0,
            HIDDEN = -1,
            LOST = -2
        }

        
        /*
         * Return a list of items specific to the players current location.
         */ 
        public static List<GameItem> GetRoomItems(int roomLocation)
        {
            List<GameItem> itemList = new List<GameItem>();
            foreach (GameItem item in gameItems)
            {
                if (item.LocationIndex == roomLocation)
                {
                    itemList.Add(item);
                }
            }
            return itemList;
        }


        public static GameItem FindItem(string itemSearch, List<GameItem> itemList)
        {
            GameItem gameItem = null;
            if (itemSearch.Length >= 3)
            {
                foreach (var currentItem in itemList)
                {
                    gameItem = currentItem.ItemId.Contains(itemSearch) ? currentItem : null;
                    if (gameItem != null) { break; }
                }
            }
            return gameItem;
        }

        //GameItem[] gameItems; //Getting rid of the array, and making it a static List.  More flexible!
        //public static List<GameItem> gameItems = new List<GameItem>();

        public static List<GameItem> gameItems = new List<GameItem>
        {
            new GameItem (0, "MAT",        "old door mat",      (int)ObjectState.VISIBLE, new Dictionary<int, string> { [(int)ObjectState.VISIBLE] = "It is a vintage entrance mat, quite heavy, and beautifully made.  the dye has faded, but it appears to feature the face of a Gorgon, in a Roman or Greek style motif." }, true, true, 8, 5),
            new GameItem (0, "KEY_BRASS",  "brass door key",    (int)ObjectState.HIDDEN, new Dictionary<int, string> { }, true, true, 1, 1 ),
            new GameItem (0, "DOOR_FRONT", "heavy wooden door", (int)ObjectState.LOCKED, new Dictionary<int, string> { }, false, false, 200, 100 )

            //gameItems[0] = new GameItem(0, "MAT",        "old door mat", (int)ObjectState.VISIBLE, new Dictionary<int, string> { [(int)ObjectState.VISIBLE]="It is a vintage entrance mat, quite heavy, and beautifully made.  the dye has faded, but it appears to feature the face of a Gorgon, in a Roman or Greek style motif." }, true, true, 8, 5);
            //gameItems[1] = new GameItem(0, "KEY_BRASS",  "brass door key", (int)ObjectState.HIDDEN, new Dictionary<int, string>(), true, true, 1, 1);
            //gameItems[2] = new GameItem(0, "DOOR_FRONT", "heavy wooden door", (int)ObjectState.LOCKED, new Dictionary<int, string> { }, false, false, 200, 100);

            //63065 DATA plastic bucket,26,vicious snake,4,charmed snake,-2,*golden leaf *,45
            //63066 DATA* bulging moneybag *,46,>$<,-2,*diamond ring *,48
            //63067 DATA* rare painting *,39,sword,13,rusty cross,23,penny,28
            //63068 DATA piece of paper,31,parachute with no ripcord,34,oriental rug,6
            //63069 DATA trapdoor marked 'danger',-2
            //63070 DATA parachute ripcord,-2,portal in the north wall,-2
            //63071 DATA pair of* ruby slippers *,-2,
            //63072 DATA majestic staircase leading up,2
            //63073 DATA majestic staircase leading down,27,battered book,11
            //63074 DATA organ in the corner,21,open organ in the corner,-2
            //63075 DATA cabinet on rollers against one wall over,5,repaired parachute,-2
            //63076 DATA "sign saying 'drop coins for luck'",19


            //return gameItems2;
        };

    }

}
