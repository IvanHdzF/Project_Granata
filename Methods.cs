using System;

namespace Granata
{
    class Methods
    {
        //Config parameters here:
        int maxTurnCount = 30; //TODO: We should make the user change this settings from a config file
        int playerCount = 2;

        internal static void PlayerTurn(int playerN)
        {
            System.Console.WriteLine($"{playerN} What do you want to do? Move/Throw"); //TODO: Change Move/Throw with detailed instructions in regards to the movement implementation
            string input = Console.ReadLine();
            bool done = false;
            int actionCount = 0;
            int maxActionCount = 4;//TODO:Let user customize this?
            //By default the player can move up to 4 times, and throw only once.
            while (!done)
            {
                switch (input)
                {
                    //For moving:
                    case "w":
                    case "a":
                    case "s":
                    case "d":
                        //playerlist[playerN].move(input);
                        if (actionCount >= maxActionCount)
                        {
                            done = true;
                        }
                        actionCount += 1;
                        break;
                    
                    //For throwing projectile:
                    case "7":
                    case "8":
                    case "9":
                    case "4":
                    case "5":
                    case "6":
                    case "1":
                    case "2":
                    case "3":
                        //playerlist[playerN].throw(input);
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Enter a valid action!");
                        break;
                }
            }

        }
        internal static void supplyProjectiles()
        {
            //foreach (player in playerlist)
            //player.supply()
        }

    }
}
