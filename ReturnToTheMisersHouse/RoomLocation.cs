using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class RoomLocation
    {
        public int LocationIndex { get; set; }
        public string Name { get; set; }
        public string DescriptionShort { get; set; } //for allowing toggle between terse/verbose
        public string Description { get; set; }
        public int[] locationMap = new int[4]; //TODO: Why is this set to '5' ?  Changing it to 4 (N,S,E,W)

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
            this.locationMap = locationMap;
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
         * DISPLAY ROOM INFORMATION TO USER.  Clears screen each time this method is called.
         */
        public void showRoomInfo(RoomLocation currentRoom)
        {
            //Access public Methods:
            var misersHouse = new MisersHouseMain();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{dl} {currentRoom.Name}");
            Console.ResetColor();

            //Describe Room Location:
            var formattedRoomDescription = MisersHouseMain.FormatTextWidth(misersHouse.maxColumns,
                currentRoom.DescriptionShort + "  " + currentRoom.Description);
            Console.WriteLine($"{sl}{formattedRoomDescription}");

            //Describe any objects:
            //*** replace with variable ***
            Console.WriteLine($"\n There is a mat here.");

            //Disclose Available Directions:
            Console.WriteLine($"\n Obvious Exits: {buildCompassDirections(currentRoom.locationMap)}");
        }

        public string buildCompassDirections(int[] locationMap)
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
