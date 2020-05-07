using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class LanguageParser
    {

        enum Verbs
        {
            GET, LOOK, TAKE, MOVE, SLIDE, PUSH, OPEN, READ,
            GO, NORTH, N, SOUTH, S, EAST, E, WEST, W, UP, DOWN, IN, OUT,
            TURN, JUMP, SWIM, FIX,
            I, INV, INVENTORY, QUIT, SCORE,
            DROP, SAY, POUR, FILL, UNLOCK,            
        }


        //RoomLocation




        //Evaluate Player Input
        public static bool AnalyzePlayerInput(string playerInput, int playerLocation /*probably more!*/)
        {
            //Since a room change initiates a screen refresh, only change value when the player changes their location:
            bool changeRooms = false;
            var misersHouse = new MisersHouseMain();
            RoomLocation roomLocation = MisersHouseMain.roomLocations[playerLocation];

            //Analyze player input and gather an array of individual words. Doesn't handle punctuation.
            string[] words = playerInput.Split(' ');

            string playerVerb = "";
            for (int i = 0; i < words.Length; i++)
            {
                if (Enum.IsDefined(typeof(Verbs), words[i].ToUpper()))
                {
                    playerVerb = words[i].ToUpper();
                }
            }

            if (playerVerb.Length < 1)
            {
                Console.WriteLine("\n" + OracleDoesNotUnderstand());
                return false;
            }

            //Validate Directions:
            if (playerVerb == Verbs.N.ToString() || playerVerb == Verbs.NORTH.ToString() )
            {
                if (roomLocation.locationMap[0] > 0)
                {
                    MisersHouseMain.playerLocation = roomLocation.locationMap[0];
                    changeRooms = true;
                }                
            }            

            return changeRooms;
        }


        /*
         * Randomly display a response from Computer:
         */
        private static string OracleDoesNotUnderstand()
        {

            Random rnd = new Random();
            int randomResponse = rnd.Next(1, 17);
            string oracleResponse = "";

            switch (randomResponse)
            {
                case 1:  oracleResponse = "What?";                  break;
                case 2:  oracleResponse = "Hmm...";                 break;
                case 3:  oracleResponse = "I don't understand!";    break;
                case 4:  oracleResponse = "Does not compute!";      break;
                case 5:  oracleResponse = "Are you joking?";        break;
                case 6:  oracleResponse = "Please try again...";    break;
                case 7:  oracleResponse = "Try a different command";break;
                case 8:  oracleResponse = "I didn't understand one or more of the words you used."; break;
                case 9:  oracleResponse = "Error 404!  Word could not be found!";       break;
                case 10: oracleResponse = "My limited intellect is no match for yours!";break;
                case 11: oracleResponse = "That's strange... nothing happened!";        break;
                case 12: oracleResponse = "That doesn't make any sense.";               break;   
                case 13: oracleResponse = "I'm too simple for a command like that!";    break;
                case 14: oracleResponse = "Too wordy... try a simpler command!";        break;
                case 15: oracleResponse = "One or two word commands work best!";        break;
                case 16: oracleResponse = "Please try using a \"Verb Noun\" command.";  break;
                default: oracleResponse = "Houston, we have a problem!  Invalid Case!"; break;
            }

            return oracleResponse;
        }


    }

}
