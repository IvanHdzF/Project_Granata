using System;

namespace Granata
{
    class Methods
    {
        internal static void PlayerTurn(int playerN)
        {
            System.Console.WriteLine($"Player {playerN+1} What do you want to do? WASD to Move(Max 4 times before spending your turn)/SPACE to Throw");
            bool done = false;
            int actionCount = 0;
            int maxActionCount = 4;
            //By default the player can move up to 4 times, and throw only once.
            while (!done)
            {
                char input = Console.ReadKey().KeyChar;
                System.Console.WriteLine();
                switch (char.ToUpper(input))
                {  
                    //For moving:
                    case 'W':
                    case 'A':
                    case 'S':
                    case 'D':
                        //playerlist[playerN].move(input);
                        if (actionCount >= maxActionCount)
                        {
                            done = true;
                        }
                        actionCount += 1;
                        break;
                    
                    //For throwing projectile:
                    case '7':
                    case '8':
                    case '9':
                    case '4':
                    case '5':
                    case '6':
                    case '1':
                    case '2':
                    case '3':
                        //playerlist[playerN].throw(int.Parse(input));
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
            System.Console.WriteLine("Supplying projectiles");
            //foreach (player in playerlist)
            //player.supply()
        }
        internal static void ValidateConfigInput(ref int value,int defaultValue,int maxValue)
        {
            bool done=false;
            while (!done){
                var input=Console.ReadLine();
                if (input == "") //if input is empty we take the default value
                {
                    value = defaultValue;
                    break;
                }
                done=int.TryParse(input,out value);
                if (!done)
                {
                    System.Console.WriteLine("Please input an integer value!");
                    continue;
                }
                if (value>maxValue)
                {
                    done=false;
                    System.Console.WriteLine($"Max value is {maxValue}!");
                }  
            }
        }
    }
}
