using System;

namespace ReturnToTheMisersHouse
{
    class MisersHouseMain
    {
        //Initialize Game Variables:
        public string playerName = "Zork Adventurer";
        public string playerInput = "";
        public int totalPoints = 0;
        public string dl = "\n\n"; //double line
        public string ql = "\n\n\n\n"; //quadline


        static void Main(string[] args)
        {
            //FIXME: Failing because of a CS0120 error: Object reference is required for non-static fields.
            //displayGameIntro();
            //displayGameEnding();

            // ----------------------------------------------------------------------------------------------------
            //TODO:  In order to call non-static methods, I must move them out of the Main class
            //       Then in order to call them, I must create an instance of the class.
            //       At first I will do it within this class (i.e. "Program"), but they should be 
            //       moved out eventually into separate classes.  Then I can re-use them in other text adventures.
            // ----------------------------------------------------------------------------------------------------

            var misersHouse = new MisersHouseMain();
            misersHouse.displayGameIntro();

            var roomLocation = new RoomLocation();
            RoomLocation[] roomLocations = roomLocation.GenerateRoomData();

            misersHouse.showRoomInfo(); // Just temporary for testing...

            misersHouse.displayGameEnding();


        }

        public void displayGameIntro()
        {
       

        Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n");
            Console.WriteLine("\t\t\t\t ╔══════════════════════════════════════════╗");
            Console.WriteLine("\t\t\t\t ║   *** RETURN TO THE MISER'S House! ***   ║");
            Console.WriteLine("\t\t\t\t ╚══════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("\n Many of the autumn leaves have fallen from the trees that surround the house.  You glance around at the grounds"
                            + "\n that must have been stately and well groomed at one time.  Clearly that was years ago, for most of the trees"
                            + "\n are now overgrown and a couple are encroaching upon the large house.");
            Console.WriteLine("\n The glass in many of the windows is warped, clearly showing their age.  Several of the windows have cracks "
                            + "\n in them.  One attic window, high above the porch, is of beautiful stained glass.  Clearly this home was built"
                            + "\n with pride and great skill, at a time where the architects and masons knew their craft well.");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n               >> Game has been Optimized for 112 x 30 Character Resolution <<");
            Console.ResetColor();

            Console.Write("\n Greetings Weary Traveler! ");
            Console.Write("\n\n   > What is thy name? ");
            var name = Console.ReadLine();

            var player = new Player();
            playerName = player.processPlayerName(name, playerName);
        }

        public void showRoomInfo()
        {
            //Move into a separate function:
            Console.WriteLine($"{dl} You are on the Front Porch");

            //replace with variable
            Console.WriteLine($"\n There is a mat here.");

            Console.Write($"\n  Press any key to continue.");
            playerInput = Console.ReadLine();
        }

        public void displayGameEnding()
        {
            //TODO:  Display Score and complete game:
            Console.Write($"{ql} Thy game hast ended, {playerName}!");
            Console.Write($"{dl} During thy journey this day, thou has amassed a total score of {totalPoints}.");
            Console.Write($"{ql}  Press any key to exit...\n\n\n");
            playerInput = Console.ReadLine();
        }


        



        //Standard output
        private void outputLine (String text)
        {
            //TODO:  Place future text here:            
        }


    }
}
