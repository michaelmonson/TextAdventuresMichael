using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class MisersHouseMain
    {
        //Initialize Game Variables:
        public string playerName = "Zork Adventurer";
        public string playerInput = "";
        public int playerLocation = 0;
        public int totalPoints = 0;

        //Standardize Formatting:
        public int maxColumns = 112;
        private static string sl = "\n";  //single line
        private static string dl = "\n\n";  //double line
        private static string ql = "\n\n\n\n"; //quadline


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

            //Instantiate RoomLocation class and populate actual Room Data:
            var roomLocation = new RoomLocation();
            RoomLocation[] roomLocations = roomLocation.GenerateRoomData();

            int gameIsActive = 1;

            //Need to keep the game in a loop until it is ready to end.  Primative, but it works
            while (gameIsActive > 0)
            {
                //Display Room data.
                roomLocation.showRoomInfo(roomLocations[misersHouse.playerLocation]);

                //Get Player Input:
                while (misersHouse.playerInput.Trim().Length < 1)
                {
                    Console.Write($"{sl}   ? ");
                    misersHouse.playerInput = Console.ReadLine();
                }
                

                //...MORE CODE!  When ready to end, change the 'gameIsActive' code...
            }

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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t\t\t\t\t     By Michael Monson ");
            Console.WriteLine("\n\t\t   Based on 'The Miser's House' by M.J. Lansing in 1981 - nearly 40 years ago!");
            Console.ResetColor();

            Console.WriteLine("\n INSTRUCTIONS");
            Console.WriteLine("\n By playing this game, you are stepping back into time... to the golden age of text adventures!");
            Console.WriteLine(FormatTextWidth(maxColumns, "\n A text adventure is \"interactive fiction!\"  It is played without a mouse, and all controls are done through the keyboard.   Text Adventures are executed through a console or command-line interface (DOS).  The only graphics and sound affects in this game are those that come from your imagination!"));
            Console.WriteLine("\n To play a text adventure, you must interact with the game by entering commands, usually a verb followed by a noun.");
            Console.WriteLine(" Type commands and see what happens! To move about, type the letter representing the compass direction you wish to go.");
            Console.WriteLine(" ");
                        
            WriteColorizedLine(ConsoleColor.Cyan, "     > Press <ENTER> to begin thy adventure...");
            var input = Console.ReadLine();
            Console.Clear();

            //Game is starting...
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\n\t\t\t>> Game has been Optimized for {maxColumns} x 30 Character Resolution <<");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n EXPLORE THE MISER'S HOUSE!");
            Console.ResetColor();

            //Greet & Identify Player:
            Console.Write("\n Greetings Weary Traveler! ");
            Console.Write("\n\n   > What is thy name? ");
            var name = Console.ReadLine();
            var player = new Player();
            playerName = player.processPlayerName(name, playerName);

            Console.WriteLine(FormatTextWidth(maxColumns, "\n\n Many of the autumn leaves have fallen from the trees that surround the house.  You glance around at the grounds that must have been stately and well groomed at one time.  Clearly that was years ago, for most of the trees are now overgrown and a couple are encroaching upon the large house."));
            Console.WriteLine(FormatTextWidth(maxColumns, "\n The glass in many of the windows is warped, showing their great age.  Several of the windows have cracks in them.  One attic window, high above the porch, is of beautiful stained glass.  Clearly this home was built with pride and great skill, at a time where the architects and masons knew their craft well."));

            Console.Write($"{sl}   > Press enter to continue:");
            input = Console.ReadLine();
        }

       

        public void displayGameEnding()
        {
            //TODO:  Display Score and complete game:
            Console.Write($"{ql} Thy game hast ended, {playerName}!");
            Console.Write($"{dl} During thy journey this day, thou has amassed a total score of {totalPoints}.");
            Console.Write($"{ql}  Press any key to exit...\n\n\n");
            playerInput = Console.ReadLine();
        }


        /*
         * Format lines of text to fit within the desired size
         *   - Passes in the text to format, and the maximum number of characters to break each line at.
         */
        public static string FormatTextWidth (int maxCharColumns, string text)
        {
            var formattedString = new StringBuilder();          //This will be our completed sentance.
            List<string> sentanceLines = new List<string>();    //This collection will hold each line.

            string[] words = text.Split(' ');                   //Split words by space.  An Array holding each word.
            StringBuilder tempString = new StringBuilder("");

            foreach (var word in words)
            {
                if (word.Length + tempString.Length + 1 > maxCharColumns)
                {
                    sentanceLines.Add(tempString.ToString());
                    tempString.Clear();
                }
                tempString.Append((tempString.Length == 0 ? "" : " ") + word);
            }

            if (tempString.Length > 0)
            {
                sentanceLines.Add(tempString.ToString());
            }                

            var sentanceParts = sentanceLines.ToArray();        //Convert List of sentance lines to an Array. 

            // Iterate Sentances and append to a single String with line breaks, 
            // thus creating a paragraph with line limits for each.
            for (int i = 0; i < sentanceParts.Length; i++)
            {
                formattedString.Append(" " + sentanceParts[i] + "\n");
            }

            return formattedString.ToString();
        }


        /*
         * This method writes text to the console with the color specified.
         */            
        public static void WriteColorizedLine(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }


        /*
         * Standard output
         */
        private void OutputLine(String text)
        {
            //TODO:  Place future text here:            
        }
    }
}
