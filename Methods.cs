using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Media;
using NAudio.Wave;

namespace Granata
{
    class Methods
    {
        public static string IntroSound()
        {
            // Ruta al archivo de audio
            //string[] audios = { "Bonk.wav", "Bum.wav", "Bum.wav" };
            string audioFilePath = "intro.wav";

            // Crea un objeto WaveOut para la reproducción de audio
            using (var waveOut = new WaveOutEvent())
            {
                // Crea un objeto WaveFileReader para leer el archivo de audio
                using (var audioFileReader = new WaveFileReader(audioFilePath))
                {
                    char iniciar = '?';
                    // Asigna el objeto WaveFileReader al WaveOut
                    waveOut.Init(audioFileReader);

                    // Reproduce el audio
                    waveOut.Play();

                    Console.OutputEncoding = Encoding.UTF8;
                    string title= "\n\n\n\n\n             🟥🟥🟥🟥🟥 🟥🟥🟥🟥🟥 🟥      🟥 🟥🟥🟥🟥🟥 🟥🟥🟥🟥🟥 🟥🟥🟥🟥🟥";
                    string title2= "             🟥         🟥      🟥 🟥      🟥         🟥     🟥             🟥";
                    string title3= "             🟥         🟥🟥🟥🟥🟥 🟥🟥🟥🟥🟥 🟥🟥🟥🟥🟥     🟥     🟥🟥🟥🟥🟥";
                    string title4= "             🟥         🟥         🟥      🟥 🟥      🟥     🟥     🟥      🟥";
                    string title5= "             🟥         🟥         🟥      🟥 🟥🟥🟥🟥🟥     🟥     🟥🟥🟥🟥🟥\n";
                    System.Threading.Thread.Sleep(2000);
                    Console.WriteLine(title);
                    System.Threading.Thread.Sleep(1600);
                    Console.WriteLine(title2);
                    System.Threading.Thread.Sleep(1600);
                    Console.WriteLine(title3);
                    System.Threading.Thread.Sleep(1600);
                    Console.WriteLine(title4);
                    System.Threading.Thread.Sleep(1600);
                    Console.WriteLine(title5);
                    System.Threading.Thread.Sleep(1500);

                    Console.WriteLine("          🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥");
                    Console.WriteLine("          🟥  Press ENTER ↩️  to start game, 🅰️  for configuration, 🅱️  for quit   🟥");
                    Console.WriteLine("          🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥");

                    // Espera a que se termine la reproducción
                    while (waveOut.PlaybackState == PlaybackState.Playing && iniciar =='?')
                    {
                        iniciar = Console.ReadKey().KeyChar;
                        System.Threading.Thread.Sleep(500);
                        return iniciar.ToString();
                    }

                    return "";
                }
            }
        }
        internal static void PlayerTurn(int playerN)
        {
            Stage.RenderGrid();
            bool done = false;
            int actionCount = 0;
            int maxActionCount = 10;
            //By default the player can move up to 4 times, and throw only once.
            while (!done)
            {   
                if (Stage.players[playerN].HP <= 0)
                {
                    return;
                }
                Stage.players[playerN].ShowInventory(playerN);
                string offset_title= " ";
                for (int i = 0; i < Stage.gridSize - 23; i ++)
                {
                    offset_title+= " ";

                }   
                Console.WriteLine(offset_title+"🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥");
                System.Console.Write($"{offset_title}🟥 Player {playerN+1} What do you want to do? WASD to 🕹️ . You have {maxActionCount-actionCount} move actions left to end turn 🟥\n{offset_title}🟥 Numberpad numbers to Throw............................................................ 🟥\n");
                Console.WriteLine(offset_title+"🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥🟥");
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
                        string offset_text= " ";
                        for (int i = 0; i < Stage.gridSize + 12; i ++)
                        {
                            offset_text+= " ";
                        } 
                        Console.WriteLine($"{offset_text}🔷🔷🔷🔷🔷🔷🔷🔷🔷🔷🔷🔷");
                        System.Console.Write($"\n{offset_text}   Select a projectile\n\n{offset_text}   1️⃣  for ⚾\n{offset_text}   2️⃣  for ⛔\n{offset_text}   3️⃣  for 📛\n");
                        Console.WriteLine($"\n{offset_text}🔷🔷🔷🔷🔷🔷🔷🔷🔷🔷🔷🔷");

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

                case "2":
                    return new Projectile("2", pos, dir, 75, 25, 25, "⛔");

                case "3":
                    return new Projectile("3", pos, dir, 100, 25, 25, "📛");

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
