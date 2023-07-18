using System;

namespace Granata
{
    class Methods
    {
        internal static void PlayerTurn(int playerN)
        {
            Stage.RenderGrid();
            if (Stage.players[playerN].HP<=0)
            {
                return;
            }
            
            bool done = false;
            int actionCount = 0;
            int maxActionCount = 4;
            //By default the player can move up to 4 times, and throw only once.
            while (!done)
            {   
                System.Console.WriteLine($"Player {playerN+1} What do you want to do? WASD to Move(You have {maxActionCount-actionCount} move actions left to end turn)\nNumberpad numbers to Throw");
                char input = Console.ReadKey().KeyChar;
                System.Console.WriteLine();
                input=char.ToUpper(input);
                switch (input)
                {  
                    //For moving:
                    case 'W':
                    case 'A':
                    case 'S':
                    case 'D':
                        Stage.players[playerN].Move(input,playerN);
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
                        int intInput=input- '0';
                        var position=Stage.players[playerN].Position;
                        bool playerCollision=false;
                        int[] pos = new int[] { position[0], position[1]};
                        //TODO: Change frame or position so that player 1 cant hit player 2 turn 1
                        Stage.actualProjectile = new Projectile("rock", pos, input, 125, 30, 1, "⚾");
                        for (int i = 0;i<Stage.actualProjectile.Frames;i++)
                        {
                            Stage.players[playerN].Throw(intInput,"1",playerN);//TODO: Implement different projectile types.
                            (intInput,playerCollision)=Player.Collision(intInput);
                            if (playerCollision) break;
                        }  
                        Stage.actualProjectile=null;  
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
            //TODO: Supply projectiles
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
