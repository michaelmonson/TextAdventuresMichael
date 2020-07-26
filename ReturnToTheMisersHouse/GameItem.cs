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
            SAFE = 5,
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
            new GameItem (0,  "MAT",        "old door mat",      ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.VISIBLE] = "It is a vintage entrance mat, quite heavy, and beautifully made.  the dye has faded, but it appears to feature the face of a Gorgon, in a Roman or Greek style motif." }, true, true, false, true, 8, 5),
            new GameItem (0,  "KEY_BRASS",  "brass door key",    ObjectState.HIDDEN,  new Dictionary<ObjectState, string> { }, true, true, false, false, 1, 1 ),
            new GameItem (0,  "DOOR_FRONT", "heavy wooden door", ObjectState.LOCKED,  new Dictionary<ObjectState, string> { [ObjectState.LOCKED] = "The door is locked.  It is far too heavy to force open.",  [ObjectState.VISIBLE] = "The door lies open... it bids you move forward!"}, false, false, false, false, 200, 100 ),
            new GameItem (1,  "DOOR_FRONT", "heavy wooden door", ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.LOCKED] = "The door is locked once more." }, false, false, false, false, 200, 100 ),   //The door is locked again on the Foyer side.
            new GameItem (2,  "STAIRCASE",  "majestic staircase",ObjectState.VISIBLE, new Dictionary<ObjectState,string>{ [ObjectState.VISIBLE] = "a majestic staircase leading up" }, false, false, false, false, 5000, 1000),
            new GameItem (4,  "SNAKE",      "vicious snake",     ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.SAFE] = "the dangerous snake has been tamed."}, false, false, false, false, 0, 0),
            new GameItem (5,  "CABINET",    "rolling cabinet",   ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.VISIBLE] = "cabinet on rollers against one wall"}, true, false, true, false, 120, 50),
            new GameItem (6,  "RUG",        "oriental rug",      ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, false, false, false, 20, 30),
            new GameItem (6,  "TRAPDOOR",   "trapdoor marked 'danger'",     ObjectState.HIDDEN, new Dictionary<ObjectState, string> { }, false, false, true, false, 25, 20),
            new GameItem (8,  "PORTAL",     "portal in the north wall",     ObjectState.HIDDEN, new Dictionary<ObjectState, string> { }, false, false, true, false, 2000, 80),
            new GameItem (11, "BATTERED_BOOK", "battered book",  ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false, 8, 7),
            new GameItem (13, "SWORD",      "sword of Elvish workmanship",  ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false, 6, 7),
            new GameItem (19, "SIGN",       "quaint wooden sign", ObjectState.VISIBLE, new Dictionary<ObjectState, string> { [ObjectState.VISIBLE] = "the sign reads, 'drop coins for luck!''" }, false, false, false, false, 20, 16),
            new GameItem (21, "ORGAN",      "organ in the corner", ObjectState.CLOSED, new Dictionary<ObjectState, string> { [ObjectState.VISIBLE] = "The organ stands open, and a beautiful light eminates from its stops and functions", [ObjectState.CLOSED] = "An organ stands in the corner, its lid closed."}, false, false, true, false, 800, 400),
            new GameItem (21, "RIPCORD",    "parachute ripcord", ObjectState.HIDDEN, new Dictionary<ObjectState, string> { }, true, true, false, false, 5, 5),
            new GameItem (21, "RUBY_SLIPPERS", "pair of *RUBY SLIPPERS*",   ObjectState.HIDDEN, new Dictionary<ObjectState, string> { }, true, true, false, false, 8, 6),
            new GameItem (23, "CROSS",      "rusty cross",       ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, false, false, false, 40, 30),
            new GameItem (26, "BUCKET",     "metal bucket",      ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false,3, 5),
            new GameItem (27, "STAIRCASE",  "majestic staircase",ObjectState.VISIBLE,new Dictionary<ObjectState,string>{ [ObjectState.VISIBLE] = "a majestic staircase leading down" }, false, false, false, false, 5000, 1000),
            new GameItem (28, "PENNY",      "tarnished penny",   ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false, 1, 1),
            new GameItem (31, "PAPER",      "piece of paper",    ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, true, 1, 1),
            new GameItem (34, "PARACHUTE",  "brightly coloured parachute",  ObjectState.DAMAGED, new Dictionary<ObjectState, string> { [ObjectState.VISIBLE] = "repaired parachute", [ObjectState.DAMAGED] = "parachute with no ripcord" }, true, true, false, true, 8, 30),
            new GameItem (39, "PAINTING",   "*RARE PAINTING*",   ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, true, 10, 12),
            new GameItem (45, "GOLDEN_LEAF","*GOLDEN LEAF*",     ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false,2, 1),
            new GameItem (46, "MONEY_BAG",  "*BULGING MONEYBAG*",ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false, 8, 4),
            new GameItem (48, "DIAMOND_RING", "*DIAMOND RING*",  ObjectState.VISIBLE, new Dictionary<ObjectState, string> { }, true, true, false, false, 1, 1),
            //new GameItem ()
            
            //63066 DATA* bulging moneybag *,46,>$<,-2,*diamond ring *,48           //What is the '$" ?  Does it relate to the existing treasures?
            
        };

    }

}
