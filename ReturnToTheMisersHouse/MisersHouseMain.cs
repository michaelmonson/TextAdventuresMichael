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

            misersHouse.showRoomInfo();

            misersHouse.displayGameEnding();


        }

        public void displayGameIntro()
        {
            Console.WriteLine("        *** RETURN TO THE MISER'S House! ***");


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
