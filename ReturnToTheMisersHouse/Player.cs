using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class Player
    {
        private string dl = "\n\n"; //double line

        public string processPlayerName(string userEnteredName, string playerName)
        {
            
            switch (userEnteredName.Trim().ToUpper())
            {
                case "MICHAEL":
                    playerName = "Sir Orlando";
                    Console.Write($"{dl} Ah my friend!  You are one of our own!  Welcome back to the Miser's House, '{playerName}!'");
                    break;
                case "SARIAH":
                    playerName = "Princess " + userEnteredName;
                    Console.Write($"{dl} Sariah!  I know thee!  But thou art a Princess of the Lord! Welcome to the Miser's House, '{playerName}!'");
                    break;
                case "RUTH":
                    playerName = "Foxy " + userEnteredName;
                    Console.Write($"{dl} Ruth!  I know thee!  You are a friend of all foxes! Welcome to the Miser's House, '{playerName}!'");
                    break;
                case "CELESTE":
                    playerName = userEnteredName;
                    Console.Write($"{dl} Celeste!  Welcome to the old Miser's house... did you know your father visited here on a Commodore64 interface?");
                    break;
                default:
                    if (userEnteredName.Trim().Length > 0)
                    {
                        playerName = userEnteredName;
                        Console.Write($"{dl} Welcome {playerName}!  Let us begin your adventure this day!'");
                    } 
                    else
                    {
                        Console.Write($"{dl} Very well... if thou shall not reveal thy true identity, I shall call thee... '{playerName}!'");
                    }
                    break;

            }

            return playerName;
        }
        

    }
}
