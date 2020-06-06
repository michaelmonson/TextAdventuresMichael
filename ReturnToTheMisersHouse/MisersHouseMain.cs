using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class MisersHouseMain
    {
        //Initialize Game Variables:
        public static RoomLocation[] roomLocations = null;
        //public static GameItem[] gameItems = GameItem.GetGameItems;
        public static string playerName = "Zork Adventurer";
        public string playerInput = "";
        public static int playerLocation = 0;
        public static int totalGamePointsPossible = 2;
        public static int playerPoints = 0;
        public static int gameIsActive = 1;

        //Standardize Formatting:
        public static int maxColumns = 112;
        private static string sl = "\n";  //single line
        private static string dl = "\n\n";  //double line
        private static string ql = "\n\n\n\n"; //quadline


        /* ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         *  TODO: Implement a chronometer into the game. :-)  In other words, track the passage of time.
         *        Perhaps something simple, such as one minute for each move, and display a digital clock 
         *        readout above, when the room screen refresh occurs.   Time :   3:43 pm  (or even just the time)
         *        
         *        So, there are two types of time passage I am thinking about:
         *          1.) The game is ALWAYS turn-based.  In other words, time only passes when a command 
         *              is actually entered by the player.  Otherwise, time stands still.
         *                  
         *          2.) Game's Global time:  For every move, time passes.  This allows us to display 
         *              general, time-related events, such as "a full moon shines its rays through the window."
         *              And global events can happen in the game.  "The chiming of a great clock can be heard!"
         *              
         *          3.) Also, specific passage of time can be captured in relation to a room or location.
         *              Time is captured when entering the room, and something will happen if the player
         *              doesn't respond within 'n' number of moves.  
         *              
         *        Since a great deal of exploration by the player occurs in teh game, it would lose a 
         *        significant element of fun if EVERYTHING in the game was time-based move to move!
         *        
         *        NOTE:  These are things to think about... I have far more to get working first, before implementing this!
         * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
         */

        static void Main(string[] args)
        {
            //Add inventory items:
            Inventory.InitializeInventory();

            Console.CursorSize = 100;


        // ----------------------------------------------------------------------------------------------------
        //TODO:  In order to call non-static methods, I must move them out of the Main class
        //       Then in order to call them, I must create an instance of the class.
        //       At first I will do it within this class (i.e. "Program"), but they should be 
        //       moved out eventually into separate classes.  Then I can re-use them in other text adventures.
        // ----------------------------------------------------------------------------------------------------
                
            var misersHouse = new MisersHouseMain();
            misersHouse.DisplayGameIntro();

            //Instantiate RoomLocation class and populate actual Room Data:
            var roomLocationClass = new RoomLocation();
            roomLocations = roomLocationClass.GenerateRoomData();
            roomLocationClass.PopulateItemRoomEvents();
            
            //GameItems is a static object arry of items used throughout the game:
            //var gameItem = new GameItem();
            //gameItems = GameItem.GenerateGameObjectData();
            

            //gameIsActive = 1;

            //Need to keep the game in a loop until it is ready to end.  Primative, but it works
            while (gameIsActive > 0)
            {
                //Display Room data.
                roomLocationClass.ShowRoomInfo(roomLocations[MisersHouseMain.playerLocation]);

                //Generate current room object list:
                List<GameItem> roomItems = GameItem.GetRoomItems(MisersHouseMain.playerLocation);

                bool refreshRoom = false;
                while (!refreshRoom)
                {

                    //refreshRoom = false;
                    
                    //Get Player Input:
                    Console.Write($"{sl}   ? ");
                    misersHouse.playerInput = Console.ReadLine().ToUpper();
                    
                    //Player entered something... time to analyze!
                    refreshRoom = LanguageParser.AnalyzePlayerInput(misersHouse.playerInput, MisersHouseMain.playerLocation, roomItems);

                    if (LanguageParser.PlayerConfused > 5)
                    {
                        Console.WriteLine(MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns, "\n You appear to be confused or frustrated.  None of your commands make any sense!  If you need help, please type: 'HELP' as a command!  I am here to serve you!"));
                        LanguageParser.PlayerConfused = 0;
                    }
                }

                //...MORE CODE?  Not yet...  The "quit" command will change the 'gameIsActive' value;
            }

            misersHouse.DisplayGameEnding();
        }

        private void DisplayGameIntro()
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

            DisplayHelpInfo();
            Console.Clear();

            //Game is starting...
            WriteColorizedLine(ConsoleColor.DarkYellow, $"\n\t\t\t>> Game has been Optimized for {maxColumns} x 30 Character Resolution <<");
            WriteColorizedLine(ConsoleColor.Green, $"\n{dl} EXPLORE THE MISER'S HOUSE!");

            //Render ASCII HOUSE:
            WriteColorizedLine(ConsoleColor.White,      "\n\n      ':.                ");
            WriteColorizedLine(ConsoleColor.DarkGray, "\n         []___________         ");
            WriteColorizedLine(ConsoleColor.DarkRed, "\n        /\\            \\       ");
            WriteColorizedLine(ConsoleColor.DarkRed, "\n    ___/  \\_____/\\_____\\__    ");
            WriteColorizedLine(ConsoleColor.DarkYellow, "\n---/\\___\\ |''''''''''''|__\\-------");
            WriteColorizedLine(ConsoleColor.DarkYellow, "\n   ||'''| |'' ' || ' ''|''|");
            WriteColorizedLine(ConsoleColor.Green, "\n   ``\"\"\"`\"`\"\"\"\"'))'\"\"\"\"`\"\"``");
            WriteColorizedLine(ConsoleColor.Green, "\n               //                 ");

            //Greet & Identify Player:
            Console.Write($"{dl} Greetings Weary Traveler! ");
            WriteColorizedLine(ConsoleColor.Cyan, $"{dl}   > What is thy name? ");
            var name = Console.ReadLine();
            var player = new Player();
            playerName = player.processPlayerName(name, playerName);
            WriteColorizedLine(ConsoleColor.Yellow, $"{dl} Let us Begin...");
            Console.WriteLine(FormatTextWidth(maxColumns, $"{dl} As you slowly walk up the cobblestone driveway, you notice that many of the autumn leaves have fallen from the trees that surround the house.  You glance around at the grounds that must have been stately and well groomed at one time.  Clearly that was years ago, for most of the trees are now overgrown and a couple are encroaching upon the large house."));
            Console.WriteLine(FormatTextWidth(maxColumns, " The glass in many of the windows is warped, showing their great age.  Several of the windows have cracks in them.  One attic window, high above the porch, is of beautiful stained glass.  Clearly this home was built with pride and great skill, at a time where the architects and masons knew their craft well."));

            WriteColorizedLine(ConsoleColor.Cyan, "   > Press a key to continue:");
            Console.ReadKey();
        }


        public void DisplayHelpInfo()
        {
            Console.WriteLine("\n By playing this game, you are stepping back into time... to the golden age of text adventures!");
            Console.WriteLine(FormatTextWidth(maxColumns, "\n A text adventure is \"interactive fiction!\"  It is played without a mouse, and all controls are done through the keyboard.   Text Adventures are executed through a console or command-line interface (DOS).  The only graphics and sound affects in this game are those that come from your imagination!"));
            Console.WriteLine(" To play a text adventure, you must interact with the game by entering commands, usually a verb followed by a noun.");
            Console.WriteLine("    * Type commands and see what happens!  Part of the joy is learning how to interact.  Use your imagination!");
            Console.WriteLine("    * To move about, type the letter representing the compass direction you wish to go.");
            Console.WriteLine("    * Or you can type the full direction name if you are verbose:  North(N), South(S), East(E), West(W)");
            Console.WriteLine("    * Example of Commands (Verb/Noun): move mat, get treasure, drop brick, feed cat.");
            Console.WriteLine("    * Pick up things by typing: get rope, get bucket, and eventually 'get all' and 'pick up' may be understood.");
            Console.WriteLine("    * Drop objects in a similar fashion: drop rope, drop onion, etc.");
            Console.WriteLine("    * Other important commands include: look, inventory(i), say, score");
            Console.WriteLine("    * Many other words and commands are understood, but you'll have to discover 'em!");
            Console.WriteLine("\n Many treasures can be found within the walls of the Miser's house.  May you find them all!");
            Console.WriteLine(" Of course, a number of dangers will lurk, deep within the recesses of the Miser's house... beware!");
            Console.WriteLine(" Try to escape from the house without losing your life! \n");

            WriteColorizedLine(ConsoleColor.Cyan, "     > Press <ENTER> to begin thy adventure...");
            Console.ReadLine();
        }
              

        private void DisplayGameEnding()
        {
            //TODO:  Display Score and complete game:
            WriteColorizedLine(ConsoleColor.Cyan, $"{ql} Thy game hast ended, {playerName}!");
            WriteColorizedLine(ConsoleColor.Yellow, $"{dl} During thy journey this day, thou has amassed a total score of [ {playerPoints} ].");
            WriteColorizedLine(ConsoleColor.Yellow, $"{dl} Had you surived your quest and collected all treasure, a total score of [ {totalGamePointsPossible} ] is possible.");

            //TODO:  Add in a "ranking" of scores, such as apprentace, Grand Adventurer (or if dealing with Star Trek, Lieutenant through Captain or even Admiral)
            var ranking = playerPoints / totalGamePointsPossible;
            
            WriteColorizedLine(ConsoleColor.Yellow, $"{dl}    RANK ►  {totalGamePointsPossible} ] is possible.");

            Console.Write($"{dl}  Press any key to exit...\n\n\n");
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
         * Add points to the player's score
         */
        public static void AddPlayerPoints(int newPoints)
        {
            MisersHouseMain.playerPoints += newPoints;
        }


        /*
         * Get the player's current total points to the player's score
         */
        public static int GetPlayerPoints()
        {
            return MisersHouseMain.playerPoints;
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
