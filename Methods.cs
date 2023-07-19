using System;

namespace Granata
{
    class Methods
    {
        internal static void PlayerTurn(int playerN)
        {
            Stage.RenderGrid();
            if (Stage.players[playerN].HP <= 0)
            {
                return;
            }

            bool done = false;
            int actionCount = 0;
            int maxActionCount = 10;
            //By default the player can move up to 4 times, and throw only once.
            while (!done)
            {   
                Console.WriteLine("   🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥");
                System.Console.Write($"   🟥Player {playerN+1} What do you want to do? WASD to Move You have {maxActionCount-actionCount} move actions left to end turn🟥\n   🟥Numberpad numbers to Throw............................................................🟥\n");
                Console.WriteLine("   🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥");
                System.Console.WriteLine("\n");
                char input = Console.ReadKey().KeyChar;
                System.Console.WriteLine();
                input = char.ToUpper(input);
                switch (input)
                {
                    //For moving:
                    case 'W':
                    case 'A':
                    case 'S':
                    case 'D':
                        Stage.players[playerN].Move(input, playerN);
                        Stage.RenderGrid();
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
                        int intInput = input - '0';
                        Stage.players[playerN].ShowInventory(playerN);
                        System.Console.Write("Select a projectile (1 rock, 2 grenade, 3 mine): ");

                        string projType = StringIntInput(1, 3);
                        Player.Sound("Ok.wav");
                        if (Stage.players[playerN].CheckProjectileAvailible(projType, playerN))
                        {
                            var position = Stage.players[playerN].Position;
                            bool playerCollision = false;
                            int[] pos = new int[] { position[0], position[1] };
                            int[] posProj = { Stage.players[playerN].Position[0], Stage.players[playerN].Position[1] };
                            Stage.actualProjectile = GetProjectile(projType, posProj, intInput);
                            for (int i = 0; i < Stage.actualProjectile.Frames; i++)
                            {
                                Stage.players[playerN].Throw(intInput, projType, playerN);
                                (intInput, playerCollision) = Player.Collision(intInput);
                                if (playerCollision) break;
                            }

                            if (!playerCollision && Stage.players[playerN].grenadeImpact())
                                Player.Sound("Bum.wav");
                            int[] posMines = {Stage.actualProjectile.ProjectilePosition[0], Stage.actualProjectile.ProjectilePosition[1]};
                            if (!playerCollision && Stage.players[playerN].plantMine(posMines))
                            {
                                Player.Sound("Planted.wav");
                                System.Console.WriteLine("MINA");
                            }
                                
                                //Player.Sound("Bum.wav");
                            

                        }
                        Stage.actualProjectile = null;
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Enter a valid action!");
                        break;
                }
            }
        }

        public static Projectile GetProjectile(string type,int[] pos, int dir)
        {            
            switch (type)
            {
                case "1":
                    return new Projectile("1", pos, dir, 25, 30, 0, "⚾");
                    break;

                case "2":
                    return new Projectile("2", pos, dir, 75, 25, 25, "💣");
                    break;

                case "3":
                    return new Projectile("3", pos, dir, 100, 25, 25, "🧨");
                    break;

                default:
                    System.Console.WriteLine("Not a valid type!");
                    return null;
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
        internal static void ValidateConfigInput(ref int value, int defaultValue, int maxValue)
        {
            bool done = false;
            while (!done)
            {
                var input = Console.ReadLine();
                if (input == "") //if input is empty we take the default value
                {
                    value = defaultValue;
                    break;
                }
                done = int.TryParse(input, out value);
                if (!done)
                {
                    System.Console.WriteLine("Please input an integer value!");
                    continue;
                }
                if (value > maxValue)
                {
                    done = false;
                    System.Console.WriteLine($"Max value is {maxValue}!");
                }
            }
        }


        internal static string StringIntInput(int minValue, int maxValue)
        {
            //validates that input int string eg "2" is within values of minValue and maxValue            
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result >= minValue && result <= maxValue)
                {
                    return input;
                }
                else
                {
                    System.Console.WriteLine($"Select a value between {minValue} and {maxValue}");
                }

            }
        }
    }
}
