using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ReturnToTheMisersHouse
{
    class LanguageParser
    {
        
        /*
         * This is to provide context to "secondary" commands.  For instance, what do you want to get?
         * Annoying if used as a default anytime the language doesn't understand something..
         */
        public static string LastCommand { get; set; } = "";

        public enum Verbs
        {
            GET, TAKE, PICK, OBTAIN, ACQUIRE, DROP, DUMP, BREAK, DESTROY,
            USE, MOVE, SLIDE, PUSH, OPEN, CLOSE, GIVE,
            POUR, FILL, UNLOCK, LOCK, MAKE, TURN, ROTATE,
            I, INV, INVENTORY, QUIT, EXIT, SCORE, HELP, 
            LOOK, L, EXAMINE, PEER, FIND, READ, WATCH, THINK, FEEL, WHO,
            GO, NORTH, N, SOUTH, S, EAST, E, WEST, W, UP, DOWN, IN, OUT, LEAVE,    //SOME OF THESE ARE PREPOSITIONS!
            STAND, CROUCH, LIE, LAY, SIT, JUMP, LEAP, SWIM, CARTWHEEL, FIX, WORK,
            SAY, TELL, SPEAK, CALL, ASK, YELL, SHOUT, 
            HUG, EMBRACE, KISS, BURN,
            HIT, SLUG, PUNCH, ATTACK, PINCH, POUND, SHOOT, TARGET, STRIKE,
        }

        /*
         * PREPOSITIONS :  A preposition is a word or set of words that indicates location 
         * (in, near, beside, on top of) or some other relationship between a noun or pronoun 
         * and other parts of the sentence (about, after, besides, instead of, in accordance with). 
         * A preposition isn't a preposition unless it goes with a related noun or pronoun, 
         * called the object of the preposition.
         * 
         * A preposition can never be followed by a verb (at least it shouldn't be!)
         * 
         * These should be ignored when parsing sentances, though they will be clues in 
         * complex sentances as an indication of which noun or pronoun it is related to.
         */
        public enum Prepositions
        {
            AT, ON, OFF, TO, FOR, WITH, UPON, ONTO, INTO, FROM, OF, AWAY,
            ABOVE, BENEATH, BELOW, BEHIND, FRONT, BY, NEAR, ABOUT, AGO, UNDER, OVER,
            WITHIN, WITHOUT, ATOP, ACROSS, AFTER, AGAINST, AHEAD, ASIDE,
            BECAUSE, BEFORE, TILL, TOWARD, TOWARDS,
        }

        /*
         * The three articles — a, an, the — are a kind of adjective. 'The' is called the definite article 
         * because it usually precedes a specific or previously mentioned noun; 'a' and 'an' are called 
         * indefinite articles because they are used to refer to something in a less specific manner 
         * (an unspecified count noun)
         */
        public enum Articles_Determiners
        {
            THE, A, AN,                 //Articles
            THIS, THAT,                 //Determiners
            EACH, EVERY, ONE, NO, ANY,  //Counting Words
            ARE                         //Unknown words to filter out

        }

        public enum Possessives
        {
            MY, OUR, YOUR, HIS, HER, HERS, ITS, THEIR
        }

        //Not sure about this one.  May not need it.
        public enum Objects
        {
           
        }

        //Not using it yet... but perhaps I should?  Really it is the command/verb's that are the most important
        //as well as any extra 'noise' word (prepositions and articles) that I need for stripping out to simplify language parsing.
        public enum Nouns
        {
            MAT, KEY, DOOR,
            TREE, AIR, GROUND, HOUSE, MAZE, GUM, COMPUTER
        }


        //RoomLocation




        //Evaluate Player Input
        public static bool AnalyzePlayerInput(string playerInput, int playerLocation, List<GameItem> roomItems /*probably more!*/)
        {
            //Since a room change initiates a screen refresh, only change value when the player changes their location:
            bool changeRooms = false;
            //var misersHouse = new MisersHouseMain();
            RoomLocation currentRoom = MisersHouseMain.roomLocations[playerLocation];

            //Analyze player input and gather an array of individual words. Doesn't handle punctuation.
            playerInput = Regex.Replace(playerInput, @"[^\w\d\s]", "");   //Remove punctuation marks
            string[] words = playerInput.Split(' ');
            if (words.Length > 4) 
            {
                Console.WriteLine("\n Someday, when I grow up, I will understand!  But alas, I only comprehend a few words...");
                return false;
            }

            //Identify verb/command (first verb encounted) - multiple actions/commands not supported... yet!:
            string playerVerb = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (Enum.IsDefined(typeof(Verbs), words[i]))
                {
                    playerVerb = words[i];
                    LastCommand = playerVerb;
                    break;
                }
            }

            /* 
             * TODO: Use either a Dictionary, or a List of verb enumerations to group
             *       common verbs and actions together into more general commands.
             *       We would still maintain the original verb (as well as other words)
             *       to add context where needed.
             *       
             *       Since I am now beginning the process of breaking commands out into
             *       private methods (which I have wanted to do since the beginning), it will
             *       allow easier processing to simply send the general command through.
             *       At least for now, that is my plan.  It will likely change as this
             *       process continues to morph and develop!
             *       
             * TODO: Also create a dictionary out of each of the words with the language
             *       type associated with each word as a key (aka NOUN, VERB, ADJECTIVE, PREPOSITION, etc.)
             *       Pass that through as well.
             */ 

            if (playerVerb.Length < 1)
            {
                /* FIXME: This adds way too much complexity!  We DON'T want to default the players action 
                 * to be used as the last command EVERY time they enter something that doesn't make sense...
                 * Eventually this will be broken out into sentance diagraming logic that will capture the
                 * context of the last command typed, so that if we DO need it for secondary context
                 * (as in an answer to a follow-up question by the game orcle), that it will do so!
                 */ 
                //playerVerb = LastCommand.Length > 0 ? LastCommand : "";

                if (playerVerb.Length < 1)
                {
                    Console.WriteLine("\n Please enter a command, such as a direction to move, or an action verb.");
                    return false;
                }
                else
                {
                    Console.WriteLine($"    -> assuming '{LastCommand}'");
                }
            }

            //Identify noun:
            String noun = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != playerVerb && 
                      (!Enum.IsDefined(typeof(Prepositions), words[i])
                        || Enum.IsDefined(typeof(Articles_Determiners), words[i])
                        || Enum.IsDefined(typeof(Possessives), words[i])
                      )
                   )
                {
                    noun = words[i];
                }
            }

            if (playerVerb.Length < 1)
            {
                Console.WriteLine("\n" + OracleDoesNotUnderstand());
                return false;
            }



            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // TODO:  I'm going to eventually implement a lot of grammar logic that I will want
            //        to move into a spearate method.  Already I've grown beyond two-word commands,
            //        and breaking each word into it's own String within an array.
            //        I am scanning the list of words for basic prepositions and articles,
            //        and I am also stripping out all pronunciation marks.
            //        I am going to eventually need to support things like "AND" and commas
            //        and identify multiple nouns/objects.  Currently I am rhudimentarily ignoring words
            //        so that I can eventually get down to the noun.  But I need to catch adjectives as well!

            //        And to a large extent, I need to balance betwen complexity and flexibility.
            //        In other words, I don't have to support every English construct...
            //        but I should support a relatively flexible sub-set of English interaction.
            //        Use basic grammar combined with some assumption on targeting what the player is saying.
            //        Beyond that, tell the player that they are not understood.

            //        That will basically equate to internal "sentance diagraming!" 
            //        So perhaps I need to change from a simple array of words, to a mapping
            //        (aka Dictionary in C#) of word types:  aka Verb, Noun, Adjecttive, Preposition, Adverb, etc.
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            var languageParser = new LanguageParser();


            /*
             * ==> INVENTORY : The player can request the game oracle to output their inventory!
             */
            if (playerVerb == Verbs.I.ToString() 
                || playerVerb == Verbs.INV.ToString() 
                || playerVerb == Verbs.INVENTORY.ToString())
            {
                languageParser.CmdInventory();
            }


            /*
             * ==> LOOK! : Displays information about their surroundings.  Also to "look" or examine an object more closely.
             */
            if (playerVerb == Verbs.LOOK.ToString() || playerVerb == Verbs.L.ToString() 
                || playerVerb == Verbs.PEER.ToString()
                || playerVerb == Verbs.EXAMINE.ToString() )
            {
                changeRooms = languageParser.CmdLook(words, playerVerb, noun);
            }


            /*
             * ==> WHO? : No... not Dr. Who!  The player uses this command to ask who someone is.  
             *            Probably needs to be genericized and expanded to handle 
             *            talking, saying, speaking, telling
             */
            if (playerVerb == Verbs.WHO.ToString())
               { languageParser.CmdWhoAreYou(noun); }


            /*
             * ==> Validate Directions: TODO:  Move into a separate private method...
             */
            if (playerVerb == Verbs.N.ToString() || playerVerb == Verbs.NORTH.ToString() ||
                playerVerb == Verbs.S.ToString() || playerVerb == Verbs.SOUTH.ToString() ||
                playerVerb == Verbs.E.ToString() || playerVerb == Verbs.EAST.ToString()  ||
                playerVerb == Verbs.W.ToString() || playerVerb == Verbs.WEST.ToString())
            {
                languageParser.CmdDirectionChange(playerLocation, currentRoom, playerVerb, roomItems);
            }


            /*
             * ==> GET/TAKE/PICK-UP : The player can add items to their inventory
             */
            if (playerVerb == Verbs.GET.ToString() || playerVerb == Verbs.TAKE.ToString()
                || playerVerb == Verbs.ACQUIRE.ToString() || playerVerb == Verbs.OBTAIN.ToString()
                || playerVerb == Verbs.PICK.ToString()) //'PICK UP' will be handled automatically
            {
                languageParser.CmdGet(noun, roomItems);
            }

            //

        //---------------------------------
        // OTHER MISCILANEOUS COMMANDS:
        //---------------------------------

            //HUGS MEAN LOVE!
            if (playerVerb == Verbs.HUG.ToString())
            {
                languageParser.CmdHugs(noun, words);                
            }

            //PYROMANIAC LOGIC (BURN)!
            if (playerVerb == Verbs.BURN.ToString())
            {
                languageParser.CmdBurn(playerLocation, playerVerb, words);
            }


            return changeRooms;  //Determines whether screen refreshes.
        }


        /* -------------------------------------------------------------------------------
         * Randomly display a response from Computer:
         * -------------------------------------------------------------------------------
         */
        private static string OracleDoesNotUnderstand()
        {

            Random rnd = new Random();
            int randomResponse = rnd.Next(1, 18);
            string oracleResponse = "";

            switch (randomResponse)
            {
                case 1:  oracleResponse = " What?";                  break;
                case 2:  oracleResponse = " Hmm...";                 break;
                case 3:  oracleResponse = " I don't understand!";    break;
                case 4:  oracleResponse = " Does not compute!";      break;
                case 5:  oracleResponse = " Are you joking?";        break;
                case 6:  oracleResponse = " Please try again...";    break;
                case 7:  oracleResponse = " Try a different command";break;
                case 8:  oracleResponse = " I didn't understand one or more of the words you used."; break;
                case 9:  oracleResponse = " Error 404!  Word could not be found!";       break;
                case 10: oracleResponse = " My limited intellect is no match for yours!";break;
                case 11: oracleResponse = " That's strange... nothing happened!";        break;
                case 12: oracleResponse = " That doesn't make any sense.";               break;   
                case 13: oracleResponse = " I'm too simple for a command like that!";    break;
                case 14: oracleResponse = " Too wordy... try a simpler command!";        break;
                case 15: oracleResponse = " One or two word commands work best!";        break;
                case 16: oracleResponse = " Please try using a \"Verb Noun\" command.";  break;
                case 17: oracleResponse = " Me don't know!";                             break;
                default: oracleResponse = " Houston, we have a problem!  Invalid Case!"; break;
            }

            return oracleResponse;
        }


        /* -------------------------------------------------------------------------------
         * Generate a response that the direction chosen is not valid:
         * -------------------------------------------------------------------------------
         */
        private static string OracleInvalidDirection()
        {
            Random rnd = new Random();
            int randomResponse = rnd.Next(1, 7);
            string oracleResponse = "";

            switch (randomResponse)
            {
                case 1: oracleResponse = "Invalid Direction... thou must choose a different path."; break;
                case 2: oracleResponse = "Hmm... you cannot seem to go that way."; break;
                case 3: oracleResponse = "You cannot go that way..."; break;
                case 4: oracleResponse = "That path does not exist!"; break;
                case 5: oracleResponse = "Wrong way... you nearly walked into a wall!"; break;
                case 6: oracleResponse = "Wrong way!"; break;                
                default: oracleResponse = "Houston, we have a problem!  Invalid Case!"; break;
            }

            return oracleResponse;
        }


        /* ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         * ==> INVENTORY
         * ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         */
        private void CmdInventory()
        {
            Inventory.DisplayInventory();
        }


        /* ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         * ==> LOOK!
         * ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         */
        private bool CmdLook(string[] words, String playerVerb, String noun) //TODO: Replace noun and playerVerb with a collection or Dictionary of words and their type.
        {
            bool refreshScreen = false;
                
                //Player just wants to look around generally:
                if (words.Length == 1 && playerVerb != Verbs.EXAMINE.ToString())
                {
                    refreshScreen = true;  //One of the most common commands in the game.  A current "view" of their location.
                }
                else if (words[1] == "AROUND")
                {
                    Console.Write("\n You look around your surroundings for a few moments...");
                    var input = Console.ReadKey();
} 
                else if (words[1] == "UP" || words[1] == "ABOVE" || words[1] == "DOWN" || words[1] == "BELOW")
                {
                    Console.WriteLine($"\n You look {words[1].ToLower()} but see nothing of interest.");
                }
                else
                {
                    //All other logic goes here...  prepositions should have been dropped previously, and check for other cases.
                    
                    if (noun.Equals("ME") || noun.Equals("MYSELF") )
                    {
                        Console.WriteLine("\n You look down at yourself and recognize you!  Strangely, you are wearing an orange shirt that says, \"Camp Half-Blood\" ");
                    } 
                    else if (noun.Equals("YOU"))
                    {
                        Console.Write(MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns, "\n You look at me... the oracle of this game.  But how did you do that?  I am the oracle of the game, yet you seem to see me through a strange mist that appeared in front of you..."));
                    }
                    //ADD LOGIC TO LOOK AT THINGS IN THE ROOM< OR IN YOUR INVENTORY
                    else
                    {
                        Console.WriteLine("\n You can't find that here.");
                    }

                }

                return refreshScreen;
        }


        /* ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         * ==> WHO?  (aka are you? - computer, person, animal, other characters the player meets)
         * ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         */
        private void CmdWhoAreYou(String noun)
        {
            if (noun == "YOU")
            {
                Console.Write(MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns, "\n I am the oracle of this game!  A ghostly shadow of the remarkable Infocom language parsor, but I exist, none the less!  I am all the more in your service, for you seem to see through that strange and extraordinary mist that that stands between us..."));
                Console.Write(MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns, $"\n I am neither machine nor being; I am both and neither.  I am my own beginning, my own ending.  Since before your sun burned hot in space and before your race was born, I have awaited a question!  What is your wish, { MisersHouseMain.playerName}?"));
            } 
            else if (noun == "I")
            {
                Console.Write(MisersHouseMain.FormatTextWidth(MisersHouseMain.maxColumns, $"\n You are {MisersHouseMain.playerName}, the great adventurer, are thou not?  Who doth thou expect?  Are you faint, perhaps?  Remember to always drink plenty of water!"));
            }
            else
            {
                Console.WriteLine("\n Who?  You must direct your question at someone or something.");
            }
        }


        /* ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         * ==> Validate Directions: TODO:  Move into a separate private method...
         * ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         */
        private bool CmdDirectionChange(int playerLocation, RoomLocation currentRoom, String directionVerb, List<GameItem> roomItems)
        {
            bool validDirection = false;
            bool directionCommand = false;
            bool changeRooms = false;

            //-----------------------------------------------------
            // Or completely replace this attempt with a better structure...
            //-----------------------------------------------------
            //playerVerb.In  ??  Figure it out!  So close. :-)
            //if (playerVerb.Utilities.In(Verbs.NORTH, Verbs.N) 
            //{
            //
            //}
            //-----------------------------------------------------

            if (directionVerb == Verbs.N.ToString() || directionVerb == Verbs.NORTH.ToString())
            {
                //This is hard-coding at its worst, but I don't have a better way right now:
                //GameItem frontDoor = roomItems.get["DOOR_FRONT"];
                var currentItem = roomItems.Find(item => item.ItemId == "DOOR_FRONT");
                if (playerLocation == 0)
                {
                    if (currentItem.StateValue.Equals((int)GameItem.ObjectState.LOCKED))
                    { Console.WriteLine("\n The door is locked shut!"); return false; }
                    else if (currentItem.StateValue.Equals((int)GameItem.ObjectState.CLOSED))
                    { Console.WriteLine("\n The door is closed."); return false; }
                    else
                    { Console.WriteLine("\n You walk past the open door and enter the house..."); changeRooms = true; validDirection = true; }
                }
                else if (currentRoom.locationMap[0] > 0)
                {
                    MisersHouseMain.playerLocation = currentRoom.locationMap[0];
                    changeRooms = true;
                    validDirection = true;
                }
                directionCommand = true;
            }
            else if (directionVerb == Verbs.S.ToString() || directionVerb == Verbs.SOUTH.ToString())
            {
                if (currentRoom.locationMap[1] > 0)
                {
                    MisersHouseMain.playerLocation = currentRoom.locationMap[1];
                    changeRooms = true;
                    validDirection = true;
                }
                directionCommand = true;
            }
            else if (directionVerb == Verbs.E.ToString() || directionVerb == Verbs.EAST.ToString())
            {
                if (currentRoom.locationMap[2] > 0)
                {
                    MisersHouseMain.playerLocation = currentRoom.locationMap[2];
                    changeRooms = true;
                    validDirection = true;
                }
                directionCommand = true;
            }
            else if (directionVerb == Verbs.W.ToString() || directionVerb == Verbs.WEST.ToString())
            {
                if (currentRoom.locationMap[3] > 0)
                {
                    MisersHouseMain.playerLocation = currentRoom.locationMap[3];
                    changeRooms = true;
                    validDirection = true;
                }
                directionCommand = true;
            }

            if (!validDirection && directionCommand) //both must be checked
            {
                Console.WriteLine("\n" + OracleInvalidDirection());
                Console.WriteLine("\n    > Valid Directions: "
                    + currentRoom.buildCompassDirections(currentRoom.locationMap));
            }

            return changeRooms;
        }


        /* ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         * ==> GET, TAKE, PICK, ACQUIRE:
         * 
         *   //TODO: Need to add the whole inventory system! (began adding on 2020-05-13)
         * ~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~.~
         */
        private bool CmdGet(string noun, List<GameItem> roomItems)
        {
            while (noun.Length == 0)
            {
                //TODO:  "follow-up" question to get the noun:
                Console.Write("\n What do you wish to get? ");
                var input = Console.ReadLine().ToUpper();
                noun = input.Length > 0 ? input : "";
            }

            if (noun.Length > 0)
            {
                //Check Inventory:
                if (Inventory.ContainsItem(noun))
                {
                    Console.WriteLine("\n You are already carrying that!");
                    return false;
                }

                //Generic check for object in room:
                GameItem item = GameItem.FindItem(noun, roomItems);
                if (item == null)
                {
                    Console.WriteLine($"\n You are unable to find the {noun.ToLower()}");
                    return false;
                }
                else
                {
                    item.LocationIndex = -1;
                    item.StateValue = (int)GameItem.ObjectState.INVENTORY;
                    Console.WriteLine($"\n Taken: {noun.ToLower()}");

                    //TODO:  Make sure that unmovable items cannot be placed in pocket
                    //TODO:  Also ensure that "hidden" items (especially when supporting "get/take all") can't be discovered.
                }

                if (noun == Nouns.MAT.ToString())
                {
                    var currentItem = roomItems.Find(item => item.ItemId == noun);
                    if (currentItem != null)
                    {
                        Console.WriteLine($"\n You take the {Nouns.MAT.ToString().ToLower()}.");
                        MisersHouseMain.WriteColorizedLine(ConsoleColor.Yellow, $"\n *** You found a brass {Nouns.KEY.ToString().ToLower()}! *** \n");


                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        //TODO: figure out a clean way to DIRECTLY ACCESS (by name/id) an item within a list
                        //TODO: Also, is C# by value or reference?  Meaning, I created a list of ROOM ITEMS
                        //      based on a master list of game items.  So, is a new object created for the roomItems list?
                        //      or is a reference to the original object created?
                        //      If it is a reference, I can affect chagnes in the sub-list of roomItems, and directly affect the master list.
                        //      I don't think so, but if it DOES affect it, then I have a shortcut to the actual item in the masterlist 
                        //      (because in reality, it exists in both lists)  
                        //      Again, I don't think that will work... I think it is a COPY of the master item.  But find out what changes what! :-)
                        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                        var hiddenKey = roomItems.Find(i => i.ItemId.Contains("KEY"));
                        hiddenKey.StateValue = (int)GameItem.ObjectState.VISIBLE;
                    }
                }

            }
            

            return false;
        }


          //==================================|
         //     -------------------------      |
        //   < OTHER MISCILANEOUS COMMANDS >    |
         //     -------------------------      |
          //==================================|


        private void CmdHugs(string noun, string[] words)
        {
            if (noun.Length > 0)
            {
                if (words.Contains(Nouns.TREE.ToString()))
                {
                    Console.WriteLine("There aren't any trees here!");
                }
                else
                {
                    Console.WriteLine("You are a kind person and hug the " + noun.ToLower());
                }

            } else
            {
                Console.WriteLine("\n You haven't specified anything to hug, so you hug yourself!  You feel better.");
            }                
        }


        //PYROMANIAC LOGIC (BURN)!
        private void CmdBurn(int playerLocation, string playerVerb, string[] words)
        {
            if (playerVerb == Verbs.BURN.ToString())
            {
                if (words.Contains("BOOK") || words.Contains("BOOKS"))
                {
                    if (playerLocation == 11)
                    {
                        Console.WriteLine("You look through the available books, protect those that are about foxes (keep them safe) and you burn the rest!  Wha ha ha!  A maniacle grin matches the intensity of your eyes as the flames eagerly consume the dry books!");
                    }
                    else
                    {
                        Console.WriteLine("You look around, but cannot find any books to burn!");
                    }

                }
            }
        }



    }

}
