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
            
            //Populate data manually... eventually I may move it to a DB, but asa proof of concept, it is self-contained.
            roomLocations[0] = new RoomLocation(0, "Front Porch", "You are standing on the Front Porch", "Dried leaves and dirt cover the aging steps, and the paint has long since peeled off the thick door frame that surrounds the massive door looming up in front of you." , new[]{ 1, 0, 0, 0 });
            roomLocations[1] = new RoomLocation(1, "Foyer", "You are in a foyer to a large house.  Dust is everywhere!", "It looks as though nobody has stepped into this house for years.  A sudden, faint creaking can be heard from somewhere upstairs; it sends chills down your spine.", new[] { 2, 0, 0, 12 });
            roomLocations[2] = new RoomLocation(2, "Great Hall", "Suits of armor line the walls", "", new[] { 3, 1, 0, 0 });
            roomLocations[3] = new RoomLocation(3, "Breakfast Room", "It is bright and cheery!", "", new[] { 0, 2, 4, 16 });
            roomLocations[4] = new RoomLocation(4, "Conservatory", "Through a window you see a hedge-maze.", "", new[] { 0, 5, 7, 3 });
            roomLocations[5] = new RoomLocation(5, "Red-walled Room", "You are in the red room.", "It looks very formal, but the red paint and dark wooden panneling is garish and dated.  A mahogony desk sits in one corner.", new[] { 4, 6, 0, 0 });
            roomLocations[6] = new RoomLocation(6, "Formal Parlor", "", "", new[] { 5, 0, 10, 0 });
            //roomLocations[7] = new RoomLocation(7, "", "", "", new[] { });
            //roomLocations[8] = new RoomLocation(8, "", "", "", new[] { });
            //roomLocations[9] = new RoomLocation(9, "", "", "", new[] { });
            //roomLocations[10] = new RoomLocation(10, "", "", "", new[] { });


            //FIXME:  Why isn't this working?
            //Console.Write($"\n\n TESTING:  = {roomLocations[3].ToString()}");
            return roomLocations;
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
