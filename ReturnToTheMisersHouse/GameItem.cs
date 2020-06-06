using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class GameItem
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // TODO: Create some matrix / dictionaries for reference when trying to do specific things such as eating items.
        //       So, have a "FOOD" matrix, where there is a description of what happens when trying to eat something.
        //       In addition, classify objects with an enumeration on general purposes of the item.
        //           > Example:  FOOD, TOOL, TREASURE, etc.
        //       Creating things like this allow for more intelligatn interactions, as well as reducing the amount of 
        //       spurious logic and checks.  Having lists of items that are classified in matrixes with specific responses 
        //       for different scenarios, as well as generic responses, cleverly chosen, when items do not match,
        //       allow for generic responses as well.  
        //
        // TODO: Create a Dictionary (aka Java HashMap) where items can be fixed!  Both ones the player breaks 
        //       (to an extent) as well as items that need fixing in the game.  But a traditional Dictionary 
        //       might not quite do it.  Need to be able to associate TWO items together (the broken item and the tool)
        //       as well as a description that is rendered to the user when they are successful in fixing an item.
        //
        // TODO: Track how much (quantity, percentage) an item has been used.  For instance, a water bottle, 
        //       when the player drinks from it.  Or a spool of string or a coil of rope when used to fix things.
        //       That adds an awful lot of complexity, since items are no longer "black & white", and can track 
        //       "gray areas" of partial usage.  It may not be worth implementing at all. ;-)
        //
        // TODO: I still need to implement tracking of weight and size to limit how much the player can carry.
        //       And I need to track things like fatigue and rest, and possibly even the passage of time!
        //       THAT'S what I was trying to remember!  I'll add a TODO to the MisersHouseMain class.
        //
        // IN other words, there is significant complexity and depth I still need to add to the foundation 
        // of the game before I think about item classifications or partial usage of an item!  Cool, but not yet!
        //
        // The flexibility of a modern programming language allows for so much!  In the past, a program could only reach
        // a certain point before the maintainability of that program became overwhelming and nigh impossible.
        // Object-oriented programming languages allow data and associations can be represented by objects within the world.
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public int LocationIndex { get; set; }  //Location that the object first appears
        public string ItemId { get; set; }      //Unique alphanumeric CONSTANT that is an ID for the item
        public string Name { get; set; }        //Item Name
        public ObjectState State { get; set; }  //Object state.  Correspondes to enum values.
        public Dictionary<ObjectState, string> StateDescription { get; set; } //An array of descriptions for different states/conditions 
        public bool Movable { get; set; }       //Item can be moved, but not taken
        public bool Luggable { get; set; }      //Item can be taken
        public bool Lockable { get; set; }      //Item can be locked
        public bool Breakable { get; set; }     //Item can be broken or damaged under normal circumstances; denotes whether the item is impervious to damage.
        public int Weight { get; set; }         //Max: 50 pounds. Impacts how much the player can carry.
        public int Size { get; set; }           //Max: 20 size units (arbitrary). Large, cumbersome loads are difficult.

        public GameItem()
        {
        }

        public GameItem(int locationIndex, string itemId, string name,
                    ObjectState state, Dictionary<ObjectState, string> stateDescription,
                    bool movable, bool luggable, bool lockable, bool breakable, int weight, int size)
        {
            LocationIndex = locationIndex;
            ItemId = itemId;
            Name = name;
            State = state;
            StateDescription = stateDescription;
            Movable = movable;
            Luggable = luggable;
            Lockable = lockable;
            Breakable = breakable;
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

        /*
         * Find a specific item.  Note that this takes an item name as a keyword, as well as a
         * list of items to search.  That way the calling method can send through either a subset
         * of items to search (such as those items in their room), or the entire set of game items.
         */ 
        public static GameItem FindItem(string itemNameSearch, List<GameItem> itemList)
        {
            GameItem itemMatch = null;
            if (itemNameSearch.Length >= 3)
            {
                foreach (var currentItem in itemList)
                {
                    itemMatch = currentItem.ItemId.Contains(itemNameSearch) ? currentItem : null;
                    if (itemMatch != null) { break; }
                }
            }
            return itemMatch;
        }


        //GameItem[] gameItems; //Getting rid of the array, and making it a static List.  More flexible!
        //public static List<GameItem> gameItems = new List<GameItem>();

        public static List<GameItem> gameItems = new List<GameItem>
        {
            new GameItem (0, "MAT",        "old door mat",      ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.VISIBLE] = "It is a vintage entrance mat, quite heavy, and beautifully made.  the dye has faded, but it appears to feature the face of a Gorgon, in a Roman or Greek style motif." }, true, true, false, true, 8, 5),
            new GameItem (0, "KEY_BRASS",  "brass door key",    ObjectState.HIDDEN,  new Dictionary<ObjectState, string> { }, true, true, false, false, 1, 1 ),
            new GameItem (0, "DOOR_FRONT", "heavy wooden door", ObjectState.LOCKED,  new Dictionary<ObjectState, string> { [ObjectState.LOCKED] = "The door is locked.  It is far too heavy to force open.",  [ObjectState.VISIBLE] = "The door lies open... it bids you move forward!"}, false, false, false, false, 200, 100 ),
            new GameItem (1, "DOOR_FRONT", "heavy wooden door", ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.LOCKED] = "The door is locked once more."}, false, false, false, false, 200, 100 )   //The door is locked again on the Foyer side.
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
