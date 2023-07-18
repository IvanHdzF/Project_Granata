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
    public class Player
    {

        //crear diccionario de tipos de armas tipo:cantidad
        public string Symbol { get; set; }
        public int HP { get; set; }
        public int[] Position { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Actions { get; set; }

        public Dictionary<string, int> Projectiles { get; set; }

        public Player(string symbol, int hp, string name, int score, int[] position, Dictionary<string, int> projectiles)
        {
            this.Symbol = symbol;
            this.HP = hp;
            this.Position = position;
            this.Name = name;
            this.Score = score;
            this.Projectiles = projectiles;
        }

        public void Move(char direction, int playerN)
        {
            System.Console.WriteLine(direction);
            System.Console.WriteLine(playerN);
            //validar si choco con mina, obstaculo y eje de mapa
            switch (direction)
            {
                case 'W':
                    if (Stage.CheckObstacles(Stage.players[playerN].Position[0], Stage.players[playerN].Position[1] - 1))
                    {
                        return;
                    }
                    if (Stage.players[playerN].Position[1] > 0) Stage.players[playerN].Position[1]--;
                    break;
                case 'A':
                    if (Stage.CheckObstacles(Stage.players[playerN].Position[0] - 1, Stage.players[playerN].Position[1]))
                    {
                        return;
                    }
                    if (Stage.players[playerN].Position[0] > 0)
                    {
                        Stage.players[playerN].Position[0]--;
                    }
                    break;
                case 'S':
                    if (Stage.CheckObstacles(Stage.players[playerN].Position[0], Stage.players[playerN].Position[1] + 1))
                    {
                        return;
                    }
                    if (Stage.players[playerN].Position[1] < Stage.gridSize - 1) Stage.players[playerN].Position[1]++;
                    break;
                case 'D':
                    if (Stage.CheckObstacles(Stage.players[playerN].Position[0] + 1, Stage.players[playerN].Position[1]))
                    {
                        return;
                    }
                    if (Stage.players[playerN].Position[0] < Stage.gridSize - 1) Stage.players[playerN].Position[0]++;
                    break;
                    // case ' ':
                    //     Console.WriteLine("Direccion");
                    //     int dir = int.Parse(Console.ReadLine());
                    //     Console.WriteLine("Tipo de Proyectil");
                    //     int type = int.Parse(Console.ReadLine());

                    //     for (int i = 0; i < actualProjectile.Frames; i++)
                    //     {
                    //         int mydelay = 500;
                    //         Throw(dir, type);
                    //         dir = collision(dir);
                    //         //System.Console.WriteLine($"ubicacion proyectil en X:{Stage.actualProjectile.ProjectilePosition[0]}  Y:{Stage.actualProjectile.ProjectilePosition[1]}");
                    //         Thread.Sleep(mydelay);
                    //     }
                    //     break;
                    // default:
                    //     Console.WriteLine("Invalid move. Try again.");
                    //     break;
            }
            Stage.RenderGrid();
        }

        public void ShowInventory(int playerN)
        {
            System.Console.WriteLine($"HP LEFT: {Stage.players[playerN].HP}");
            string[] types = { "Rock", "Grenade", "Mine" };
            foreach (var key in Stage.players[playerN].Projectiles.Keys)
            {
                System.Console.WriteLine($"{Stage.players[playerN].Symbol} has {types[int.Parse(key) - 1]}: {Stage.players[playerN].Projectiles[key]} ");
            }
        }


        public bool CheckProjectileAvailible(string type, int playerN)
        {
            //validation player still has projectiles types left    
            foreach (var proj in this.Projectiles)
            {
                if (Stage.players[playerN].Projectiles[type] > 0)
                {
                    Stage.players[playerN].Projectiles[type]--;
                    return true;
                }
                else
                {
                    System.Console.WriteLine("Not enough projectiles of that type!");
                    return false;
                }
            }
            return false;
        }
        public void Throw(int direction, string type, int playerN)
        {
            int mydelay = 250;
            switch (direction)
            {
                case 1:
                    Stage.actualProjectile.ProjectilePosition[0]--;
                    Stage.actualProjectile.ProjectilePosition[1]++;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 2:
                    Stage.actualProjectile.ProjectilePosition[1]++;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 3:
                    Stage.actualProjectile.ProjectilePosition[0]++;
                    Stage.actualProjectile.ProjectilePosition[1]++;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 4:
                    Stage.actualProjectile.ProjectilePosition[0]--;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 6:
                    Stage.actualProjectile.ProjectilePosition[0]++;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 7:
                    Stage.actualProjectile.ProjectilePosition[0]--;
                    Stage.actualProjectile.ProjectilePosition[1]--;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 8:
                    Stage.actualProjectile.ProjectilePosition[1]--;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 9:
                    Stage.actualProjectile.ProjectilePosition[0]++;
                    Stage.actualProjectile.ProjectilePosition[1]--;
                    Stage.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                default:
                    Console.WriteLine("Invalid move. Try again.");
                    Thread.Sleep(mydelay);
                    break;
            }
        }

        public void Refill()
        {
            Projectiles["1"] = 10;
            Projectiles["2"] = 20;
            Projectiles["3"] = 2;
        }

        public bool grenadeImpact()
        {
            int projX = Stage.actualProjectile.ProjectilePosition[0];
            int projY = Stage.actualProjectile.ProjectilePosition[1];

            for (int x = projX - 1; x < projX + 2; x++) //PRIMER FOR
            {
                for (int y = projY - 1; y < projY + 2; y++) //SEGUNDO FOR
                {
                    foreach (var player in Stage.players)
                    {
                        if (player.Position[0] == projX && player.Position[1] == projY)
                        {
                            continue;
                        }
                        else if (player.Position[0] == x && player.Position[1] == y)
                        {
                            player.HP -= Stage.actualProjectile.SplashDamage;
                            return true;
                        }
                    }
                }
            }
            return false; //not found
                          //returns 1 for direct hit and 2 for splash damage
        }

        public static (int, bool) Collision(int dir)
        {
            bool playerCollision = false;
            foreach (var player in Stage.players)
            {
                int projX = Stage.actualProjectile.ProjectilePosition[0];
                int projY = Stage.actualProjectile.ProjectilePosition[1];
                //direct hit
                if (projX == player.Position[0] && projY == player.Position[1])
                {
                    Console.WriteLine($"{player.Name} was hit!");
                    player.HP -= Stage.actualProjectile.Damage;
                    playerCollision = true;
                    if (player.HP <= 0) player.Position[0] = 200;



                    Sound(Stage.actualProjectile.Tipo);
                    return (dir, playerCollision);

                }
            }
            dir = ChangeDirection(dir);// check collision with obstacule
            return (dir, playerCollision);
        }
        static int ChangeDirection(int dir)
        {

            int NextCoordenateProyectileX = 0;
            int NextCoordenateProyectileY = 0;
            (NextCoordenateProyectileX, NextCoordenateProyectileY) = NextCoordenate(dir);

            if (Stage.actualProjectile.ProjectilePosition[0] < 1)
            {
                if (Stage.actualProjectile.ProjectilePosition[1] < 1 && dir == 7)
                    return 3; // upper left corner
                if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize - 2 && dir == 1)
                    return 9; // down left corner
                if (dir == 7)
                    return 9; // collision wall left
                if (dir == 4)
                    return 6; // collision wall left
                if (dir == 1)
                    return 3; // collision wall left
            }
            else if (Stage.actualProjectile.ProjectilePosition[0] > Stage.gridSize - 2)
            {
                if (Stage.actualProjectile.ProjectilePosition[1] < 1 && dir == 9)
                    return 1; // upper right corner
                if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize - 2 && dir == 3)
                    return 7; // down right corner
                if (dir == 9)
                    return 7; // collision wall right
                if (dir == 6)
                    return 4; // collision wall right
                if (dir == 3)
                    return 1; // collision wall right
            }

            if (Stage.actualProjectile.ProjectilePosition[1] < 1)
            {
                if (dir == 7)
                    return 1; // collision wall up
                if (dir == 8)
                    return 2; // collision wall up
                if (dir == 9)
                    return 3; // collision wall up
            }
            else if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize - 2)
            {
                if (dir == 1)
                    return 7; // collision wall down
                if (dir == 2)
                    return 8; // collision wall down
                if (dir == 3)
                    return 9; // collision wall down
            }
            //check obstacules collisions




            else if (Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY))
            {
                if (dir == 1 && (Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY + 1)) && !(Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY - 1)))
                    return 7; // collision wall down
                if (dir == 2)
                    return 8; // collision wall down
                if (dir == 3 && (Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY + 1)))
                    return 9; // collision wall down

                if (dir == 7 && (Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY - 1)) && !(Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY + 1)))
                    return 1; // collision wall up
                if (dir == 8)
                    return 2; // collision wall up
                if (dir == 9 && (Stage.CheckObstacles(NextCoordenateProyectileX, NextCoordenateProyectileY - 1)))
                    return 3; // collision wall up

                if (dir == 9)
                    return 7; // collision wall right
                if (dir == 6)
                    return 4; // collision wall right
                if (dir == 3)
                    return 1; // collision wall right

                if (dir == 7)
                    return 9; // collision wall left
                if (dir == 4)
                    return 6; // collision wall left
                if (dir == 1)
                    return 3; // collision wall left  
            }
            return dir;
        }
        static (int, int) NextCoordenate(int dir)
        {
            int NextCoordenateX = Stage.actualProjectile.ProjectilePosition[0];
            int NextCoordenateY = Stage.actualProjectile.ProjectilePosition[1];
            switch (dir)
            {
                case 1:
                    NextCoordenateX--;
                    NextCoordenateY++;
                    break;
                case 2:
                    NextCoordenateY++;
                    break;
                case 3:
                    NextCoordenateX++;
                    NextCoordenateY++;
                    break;

                case 4:
                    NextCoordenateX--;
                    break;

                case 6:
                    NextCoordenateX++;
                    break;

                case 7:
                    NextCoordenateX--;
                    NextCoordenateY--;
                    break;

                case 8:
                    NextCoordenateY--;
                    break;

                case 9:
                    NextCoordenateX++;
                    NextCoordenateY--;
                    break;

                default:
                    break;
            }
            return (NextCoordenateX, NextCoordenateY);
        }
        public static void Sound(string projType)
        {
            // Ruta al archivo de audio
            string[] audios = { "Bonk.wav", "Bum.wav", "Bum.wav" };
            string audioFilePath = audios[int.Parse(projType) - 1];

            // Crea un objeto WaveOut para la reproducción de audio
            using (var waveOut = new WaveOutEvent())
            {
                // Crea un objeto WaveFileReader para leer el archivo de audio
                using (var audioFileReader = new WaveFileReader(audioFilePath))
                {
                    // Asigna el objeto WaveFileReader al WaveOut
                    waveOut.Init(audioFileReader);

                    // Reproduce el audio
                    waveOut.Play();

                    // Espera a que se termine la reproducción
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(500);
                    }
                }
            }
        }
    }
}
