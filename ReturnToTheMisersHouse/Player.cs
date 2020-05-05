using System;
using System.Collections.Generic;
using System.Text;

namespace ReturnToTheMisersHouse
{
    class Player
    {
        private string sl = "\n";   //single line
        private string dl = "\n\n"; //double line

        public string processPlayerName(string userEnteredName, string playerName)
        {
            
            switch (userEnteredName.Trim().ToUpper())
            {
                case "MICHAEL":
                    playerName = "Sir Orlando";
                    Console.Write($"{sl} Ah my friend!  You are one of our own!  Welcome back to the Miser's House, '{playerName}!'");
                    break;
                case "MARY":
                    playerName = "Gryphongirl!";
                    Console.Write($"{sl} Greetings {playerName}!  Thou art the great love of the brave Sir Orlando!  Welcome to this realm... may you find that which you seek!");
                    break;
                case "SARIAH":
                    playerName = "Princess " + userEnteredName;
                    Console.Write($"{sl} Sariah!  I know thee!  But thou art a Princess of the Lord! Welcome to the Miser's House, '{playerName}!'");
                    break;
                case "RUTH":
                    playerName = "Foxy " + userEnteredName;
                    Console.Write($"{sl} Ruth!  I know thee!  You are a friend of all foxes! Welcome to the Miser's House, '{playerName}!'");
                    break;
                case "CELESTE":
                    playerName = userEnteredName + " - eldest born";
                    Console.Write($"{sl} Celeste!  Welcome to the old Miser's house... did you know your father visited here on a Commodore64 interface?");
                    break;
                case "SAMUEL":
                    playerName = userEnteredName + " the brazen";
                    Console.Write($"{sl} Ho Samuel!  The youth with three sisters!  I know your father well... wilt thou accept the challenge that has been placed before thee?");
                    break;
                default:
                    if (userEnteredName.Trim().Length > 0)
                    {
                        playerName = userEnteredName;
                        Console.Write($"{sl} Welcome {playerName}!  Let us begin your adventure this day!'");
                    } 
                    else
                    {
                        Console.Write($"{sl} Very well... if thou shall not reveal thy true identity, I shall call thee... '{playerName}!'");
                    }
                    break;

            }

            return playerName;
        }
        

    }
}
