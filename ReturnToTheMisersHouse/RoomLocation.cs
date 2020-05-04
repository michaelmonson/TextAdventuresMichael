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
        public int[] locationMap = new int[5];

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
            RoomLocation[] roomLocations = new RoomLocation[30];
            roomLocations[0] = new RoomLocation(0, "Front Porch", "You are standing on the Front Porch", "later..." , new[]{ 1, 0, 0, 0 });

            Console.Write($"\n TEST: Room[0] = {roomLocations[0].Name} : {roomLocations[0].DescriptionShort} : Directions: ");
            return roomLocations;
        }



    }
}
