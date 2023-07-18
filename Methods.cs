using System;

namespace Granata
{
    class Methods
    {
        internal static void PlayerTurn(int playerN)
        {
            Stage.RenderGrid();
            Stage.players[playerN].ShowInventory(playerN);
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
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.White;
                System.Console.Write($"Player {playerN+1} What do you want to do? WASD to Move(You have {maxActionCount-actionCount} move actions left to end turn)\nNumberpad numbers to Throw.............................................................");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                System.Console.WriteLine("\n");
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
                        int intInput=input - '0';
                        Stage.players[playerN].ShowInventory(playerN);
                        System.Console.Write("Select a projectile (1 rock, 2 grenade, 3 mine): ");
                        string projType = Console.ReadLine(); //ask user which type of projectile want 1,2,3
                        if (Stage.players[playerN].CheckProjectileAvailible(projType,playerN))
                        {
                            var position=Stage.players[playerN].Position;
                            bool playerCollision=false;
                            int[] pos = new int[] { position[0], position[1]};
                            
                            Stage.actualProjectile = GetProjectile(projType, playerN, intInput); 
                            for (int i = 0;i<Stage.actualProjectile.Frames;i++)
                            {
                                Stage.players[playerN].Throw(intInput,projType,playerN);
                                (intInput,playerCollision)=Player.Collision(intInput);
                                if (playerCollision) break;
                            }
                            Stage.players[playerN].grenadeImpact();
                            //Player.Sound(Stage.actualProjectile.Tipo);
                            
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

        internal static Projectile GetProjectile(string type, int playerN, int dir)
        {

            int[] pos = {Stage.players[playerN].Position[0], Stage.players[playerN].Position[1]};
            switch(type)
            {
                case "1":
                return new Projectile("1", pos, dir, 25, 130, 0, "⚾");
                break;

                case "2":
                return new Projectile("2", pos, dir, 75, 5, 25, "💣");
                break;

                case "3":
                return new Projectile("3", pos, dir, 75, 25, 25, "🧨");
                break;

                default:
                    System.Console.WriteLine("not a valid type!");
                    return null;
                    break;
            }            
        }
        internal static void supplyProjectiles()
        {            
            System.Console.WriteLine("Supplying projectiles");
            foreach (var player in Stage.players)
            {
                player.Refill();
            }            
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
