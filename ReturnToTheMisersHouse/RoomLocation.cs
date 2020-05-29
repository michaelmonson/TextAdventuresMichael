using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

//-------------------------------------------------------------------------------------------------------
// TODO: Add in a RoomEvent[] array to store special events.  For instance, 
//       a door slamming shut or something that can change...  
//       Consider how to integrate it with the existing object state, and where overlap occurrs.
//       After all, many things are object specific if the player can interact with them.
//       But in other cases, they are room specific, and may be out of the player's control.
//       In the case of the door slamming shut, the description should only be displayed ONCE.
//       It can either be replaced with blank, or the message can be disabled.
//       If necessary, we can create it as a separate class, with it's own propeties.
//-------------------------------------------------------------------------------------------------------

namespace ReturnToTheMisersHouse
{
    class RoomLocation
    {
        public int LocationIndex { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; } //for allowing toggle between terse/verbose
        public string Description { get; set; }
        public int[] LocationMap = new int[4]; //TODO: Why is this set to '5' ?  Changing it to 4 (N,S,E,W)

        /* Special descriptions and events when certain items are brought into the room 
         * or specific events that occur under unique circumstances.  A door slamming shut, for instance,
         * or an organ that plays music when you have a special item with you.
         * Perhaps a fire burns down the library, and the "burned" description is now added.  i.e. "charred walls are all that remain"
         */
        public Dictionary<string, string> ItemRoomEvents { get; set; } //Key is the Item; value is the description.

        //Special Locations:
        public static int LocInventory = -1;

        //Standardize Formatting:
        public string dl = "\n\n"; //double line
        public string sl = "\n"; //single line

        public RoomLocation()
        {
        }

        public RoomLocation(int locationIndex, string name, string descriptionShort, string description, int[] locationMap)
        {
            LocationIndex = locationIndex;
            Name = name;
            DescriptionShort = descriptionShort;
            Description = description;
            LocationMap = locationMap;
            ItemRoomEvents = new Dictionary<string, string>();
        }


        public RoomLocation[] GenerateRoomData()
        {
            RoomLocation[] roomLocations = new RoomLocation[100];
            
            //Populate data manually... eventually I may move it to a DB, but as a proof of concept, it is self-contained.
            roomLocations[0] = new RoomLocation(0, "Front Porch", "You are standing on the Front Porch.", "Dried leaves and dirt cover the aging steps, and the paint has long since peeled off the thick door frame that surrounds the massive door looming up in front of you." , new[]{ 1, 0, 0, 0 });
            roomLocations[1] = new RoomLocation(1, "Foyer", "You are in a foyer to a large house.  Dust is everywhere!", "It looks as though nobody has stepped into this house for years.  A sudden, faint creaking can be heard from somewhere upstairs; it sends chills down your spine.", new[] { 2, 0, 0, 12 });
            roomLocations[2] = new RoomLocation(2, "Great Hall", "Suits of armor line the walls.", "", new[] { 3, 1, 0, 0 });
            roomLocations[3] = new RoomLocation(3, "Breakfast Room", "It is bright and cheery!", "", new[] { 0, 2, 4, 16 });
            roomLocations[4] = new RoomLocation(4, "Conservatory", "Through a window you see a hedge-maze.", "", new[] { 0, 5, 7, 3 });
            roomLocations[5] = new RoomLocation(5, "Red-walled Room", "You are in the red room.", "It looks very formal, but the red paint and dark wooden panneling is garish and dated.  A mahogony desk sits in one corner.", new[] { 4, 6, 0, 0 });
            roomLocations[6] = new RoomLocation(6, "Formal Parlor", "", "", new[] { 5, 0, 10, 0 });
            roomLocations[7] = new RoomLocation(7, "Green Drawing Room", "", "", new[] { 0, 0, 8, 4 });
            roomLocations[8] = new RoomLocation(8, "Trophy Room", "Animal heads line the walls", "", new[] { 0, 9, 0, 7 });
            roomLocations[9] = new RoomLocation(9, "Den", "", "", new[] { 8, 0, 0, 10 });
            roomLocations[10] = new RoomLocation(10, "Blue Drawing Room", "", "", new[] { 0, 11, 9, 6 });
            roomLocations[11] = new RoomLocation(11, "Library", "Empty shelves line the walls", "", new[] { 10, 0, 0, 0 });
            roomLocations[12] = new RoomLocation(12, "Dining Room", "", "", new[] { 0, 0, 1, 13 });
            roomLocations[13] = new RoomLocation(13, "Chinese Room", "", "", new[] { 15, 0, 12, 0 });
            roomLocations[14] = new RoomLocation(14, "$", "", "", new[] { 0, 0, 0, 0 });  //What on earth is this?  Will it throw everything else off?
            roomLocations[15] = new RoomLocation(15, "Kitchen", "Like old Mother Hubbard, the kitchen is quite bare.", "", new[] { 23, 13, 16, 0 });
            roomLocations[16] = new RoomLocation(16, "Pantry", "Dust covers the mahogany shelves", "", new[] { 0, 0, 3, 15 });
            roomLocations[17] = new RoomLocation(17, "Game Room", "", "", new[] { 0, 8, 0, 18 });
            roomLocations[18] = new RoomLocation(18, "Smoking Room", "The air is stale in here", "", new[] { 21, 0, 17, 19 });
            roomLocations[19] = new RoomLocation(19, "Portico", "A murky pool glimmers on the south side", "", new[] { 21, 0, 18, 20 });
            roomLocations[20] = new RoomLocation(20, "Hall of Mirrors", "What a fine place to reflect for a time.", "", new[] { 21, 21, 19, 19 });
            roomLocations[21] = new RoomLocation(21, "Ballroom", "it has a beautiful wooden dance floor!", "", new[] { 0, 19, 0, 20 });
            roomLocations[22] = new RoomLocation(22, "Chapel", "A tablet says 'Drop a religous item or die!!'", "", new[] { 0, 0, 0, 21 });
            roomLocations[23] = new RoomLocation(23, "Back Yard", "", "", new[] { 24, 15, 40, 25 });
            roomLocations[24] = new RoomLocation(24, "Black Forest", "", "", new[] { 24, 23, 24, 24 });
            roomLocations[25] = new RoomLocation(25, "Pool Area", "There is a large swimming pool here!", "", new[] { 26, 0, 23, 0 });
            roomLocations[26] = new RoomLocation(26, "Pump House", "There is pool machinery installed here", "", new[] { 0, 25, 0, 0 });
            roomLocations[27] = new RoomLocation(27, "Middle of the Western Hallway", "", "", new[] { 35, 0, 31, 28 });
            roomLocations[28] = new RoomLocation(28, "West Bedroom", "", "", new[] { 0, 0, 27, 0 });
            roomLocations[29] = new RoomLocation(29, "Front Balcony", "A large road can be seen below, weaving around the estate.", "", new[] { 39, 0, 0, 0 });
            roomLocations[30] = new RoomLocation(30, "$", "", "", new[] { 0, 0, 0, 0});
            roomLocations[31] = new RoomLocation(31, "Master Bedroom", "There's a huge four-poster bed", "", new[] { 0, 0, 38, 27 });
            roomLocations[32] = new RoomLocation(32, "Rear Balcony", "Below you, a large hedge maze can be seen!", "Why do you feel a sense of dread when casting your eyes upon it?", new[] { 0, 36, 0, 0 });
            roomLocations[33] = new RoomLocation(33, "East Bedroom", "", "", new[] { 34, 0, 0, 38 });
            roomLocations[34] = new RoomLocation(34, "Large Walk-in Closet", "", "", new[] { 0, 33, 0, 0 });
            roomLocations[35] = new RoomLocation(35, "Junction of the West Hall and the North-South Hallway", "", "", new[] { 0, 27, 36, 0 });
            roomLocations[36] = new RoomLocation(36, "Center of the North-South Hallway", "", "", new[] { 32, 0, 37, 35 });
            roomLocations[37] = new RoomLocation(37, "Junction of the East Hall and the North-South Hallway", "", "", new[] { 0, 38, 0, 36 });
            roomLocations[38] = new RoomLocation(38, "Middle of the East Hallway", "", "", new[] { 37, 39, 33, 31 });
            roomLocations[39] = new RoomLocation(39, "South End of the East Hallway", "", "", new[] { 38, 29, 0, 0 });
            roomLocations[40] = new RoomLocation(40, "Hedge Maze", "", "", new[] { 0, 42, 0, 41 });
            roomLocations[41] = new RoomLocation(41, "Hedge Maze", "", "", new[] { 44, 42, 0, 0 });
            roomLocations[42] = new RoomLocation(42, "Hedge Maze", "", "", new[] { 41, 44, 43, 0 });
            roomLocations[43] = new RoomLocation(43, "Hedge Maze", "", "", new[] { 41, 23, 0, 0 });
            roomLocations[44] = new RoomLocation(44, "Hedge Maze", "", "", new[] { 0, 42, 0, 45 });
            roomLocations[45] = new RoomLocation(45, "Hedge Maze", "", "", new[] { 0, 0, 44, 0 });
            roomLocations[46] = new RoomLocation(46, "Walk-in Vault", "", "", new[] { 0, 0, 0, 5 });
            roomLocations[47] = new RoomLocation(47, "Dungeon", "There is light above and to the south.", "", new[] { 0, 40, 0, 0 });
            roomLocations[48] = new RoomLocation(48, "Bottom of the Swimming Pool.", "A ladder leads up and out.", "", new[] { 0, 0, 0, 0 });

            //FIXME:  Why isn't this working?
            //Console.Write($"\n\n TESTING:  = {roomLocations[3].ToString()}");
            return roomLocations;
        }


        /*
         * This data provides special events amd interactions between specific items and rooms, and the player's 
         * interactions within those rooms with specific items adds extra depth and variety to the game.  
         * There is so much detail that I wanted to add to create richness, that I wasn't sure how to proceed.  
         * I knew that I couldn't simply make it a required property of each room, for some rooms will have 
         * zero events, whereas other rooms might provide complex and rich interactions.
         * 
         * By using a dictionary in C# (roughly equivilant to a Java Map), it allows me to put in as few or
         * as many key-value pairs.  The "key" is the item the player is carrying, and the "value" is the 
         * description that is used to display extra information to the player.
         * 
         * I'll try this and see what I think, but it should work well.  At least until I find a better way!
         * Rather than having a bunch of "if/else" or switch statements with multiple cases, it allows me
         * to provide this information in a more efficient, consolidated manner.  We'll see if it works!  :-)
         */ 
        public void PopulateItemRoomEvents()
        {
            //Add special room-item interactions and events, for each room by its index.
            MisersHouseMain.roomLocations[1].ItemRoomEvents.Add("DOOR_FRONT", "The door slams shut behind you!  You think you can hear maniacal laughter eminating from deep within the bowels of the house, and a draft of cold air blows past you, turning your blood to ice...");
        }


        /*
            * DISPLAY ROOM INFORMATION TO USER.  Clears screen each time this method is called.
            */
        public void ShowRoomInfo(RoomLocation currentRoom)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{dl} {currentRoom.Name}");
            Console.ResetColor();

            //Describe Room Location:
            var formattedRoomDescription = MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns,
                currentRoom.DescriptionShort + "  " + currentRoom.Description);
            Console.WriteLine($"{sl}{formattedRoomDescription}");

            //Display any special object/room interactions or events:
            List<GameItem> roomItems = GameItem.GetRoomItems(MisersHouseMain.playerLocation);
            foreach (GameItem item in roomItems)
            {
                if (currentRoom.ItemRoomEvents.TryGetValue(item.ItemId, out string eventDescription))
                {
                    if (item.State.Equals( GameItem.ObjectState.VISIBLE ))
                    {
                        Console.WriteLine(MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns, eventDescription));
                    }
                }
            }            

            //Describe any objects in the current room:            
            foreach (GameItem item in roomItems)
            {
                if (item.State >= GameItem.ObjectState.VISIBLE )
                {
                    Console.WriteLine($" There is a {item.Name} here.");
                }                
            }

            //Display specialized descriptions:
            string specialText = buildSpecialDescriptions(roomItems);            
            if (specialText.Length > 0) 
            { 
                Console.WriteLine($" {specialText}"); 
            }

            //Disclose Available Directions:
            Console.WriteLine($"\n Obvious Exits: {BuildCompassDirections(currentRoom.LocationMap)}");
        }


        /*
         * Helper method that generates a standardized set of compass directions for output to the player.
         */ 
        public string BuildCompassDirections(int[] locationMap)
        {
            //Compass Directions are standardized as North, South, East, West
            string[] possibleCompassLocations = {"N","S","E","W" };
            string validCompassLocations = "";

            for (int i = 0; i < locationMap.Length; i++)
            {
                validCompassLocations += locationMap[i] > 0 ? possibleCompassLocations[i] + ", " : "";
            }


            //return validCompassLocations.TrimEnd(',');  //Awesome! :-)  But I have to char's to nix!
            //return validCompassLocations.TrimEnd(validCompassLocations[validCompassLocations.Length - 1]);
            return validCompassLocations.Remove(validCompassLocations.Length - 2);  //trim trailing comma and space.
        }

        /*
         * Special Descriptions to add to the richness of the narriative, and to give hints where appropriate:
         */
        private string buildSpecialDescriptions(List<GameItem> roomItems)
        {            
            string specialText = "";
            switch (MisersHouseMain.playerLocation)
            {
                case 0:
                    GameItem itemDoor = GameItem.FindItem("DOOR", roomItems);
                    if (itemDoor != null && itemDoor.State.Equals(GameItem.ObjectState.VISIBLE)) //aka opened
                    { specialText = "The door stands open, beckoning you!"; }
                    break;
                case 1:
                    GameItem itemDoorFoyer = GameItem.FindItem("DOOR", roomItems);
                    if (itemDoorFoyer != null && itemDoorFoyer.State.Equals(GameItem.ObjectState.VISIBLE)) //aka opened
                    { itemDoorFoyer.State = GameItem.ObjectState.LOCKED; }
                    else if (itemDoorFoyer != null && itemDoorFoyer.State.Equals(GameItem.ObjectState.LOCKED)) //aka opened
                    {
                        specialText = itemDoorFoyer.StateDescription[GameItem.ObjectState.LOCKED];
                    }
                    break;
                default:
                    break;
            }
            return specialText;
        }

        
        //Overrides:
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            //return base.ToString();
            return "RoomLocation: " + "Location Index:" + LocationIndex + "Name" + Name;
        }
    }
}
