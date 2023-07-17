using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
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


        public void PrintInventory()
        {
            System.Console.WriteLine($"Player 1 name: {Name}");
            System.Console.WriteLine("CURRENT INVENTORY:");
            foreach (var proj in Projectiles)
            {
                System.Console.WriteLine($"{Projectiles[proj.Key]} / {proj.Key}");
            }

        }

        public void Move(char direction,int playerN)
        {
            System.Console.WriteLine(direction);
            System.Console.WriteLine(playerN);
            //validar si choco con mina, obstaculo y eje de mapa
            switch (direction)
            {
                case 'W':
                    if (Stage.players[playerN].Position[1] > 0) Stage.players[playerN].Position[1]--;
                    break;
                case 'A':
                    if (Stage.players[playerN].Position[0] > 0)
                    {
                        Stage.players[playerN].Position[0]--;
                        System.Console.WriteLine($"{Stage.players[playerN].Position[0]}");
                    } 
                    break;
                case 'S':
                    if (Stage.players[playerN].Position[1] < Stage.gridSize - 1) Stage.players[playerN].Position[1]++;
                    break;
                case 'D':
                    if (Stage.players[playerN].Position[0] < Stage.gridSize - 1) Stage.players[playerN].Position[0]++;
                    System.Console.WriteLine($"{Stage.players[playerN].Position[0]}");
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
        public void Throw(int direction, string type, int playerN)
        {

            //validation player still has projectiles types left    
            System.Console.WriteLine($"{direction}       {type}");   
            bool valid = false;
            foreach (var proj in this.Projectiles)
            {
                if (Stage.players[playerN].Projectiles["rock"] > 0)
                {
                    System.Console.WriteLine("***************************");
                    this.Projectiles[proj.Key]--;
                    valid = true;
                    break;
                }
                else
                {
                    System.Console.WriteLine("Not enough projectiles of that type!");
                    return;
                }
            }

            int mydelay = 500;
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
            Projectiles["PP"] = 10;
            Projectiles["Granada"] = 2;
            Projectiles["Sticky"] = 2;
        }
        public static (int,bool) Collision(int dir)
        {
            bool playerCollision=false;
            foreach (var player in Stage.players)
            {
                if (Stage.actualProjectile.ProjectilePosition[0] == player.Position[0] && Stage.actualProjectile.ProjectilePosition[1] == player.Position[1]) // check collision with player 1
                {
                    Console.WriteLine($"{player.Name} was hit!"); 
                    player.HP-=Stage.actualProjectile.Damage;
                    if (player.HP<=0)player.Position[0] = 200;
                    playerCollision=true;
                    return (dir,playerCollision);


                }
            }
            dir = ChangeDirection(dir);// check collision with obstacule
            return (dir,playerCollision);
        }
        static int ChangeDirection(int dir)
        {
            if (Stage.actualProjectile.ProjectilePosition[0] < 0)
            {
                if (Stage.actualProjectile.ProjectilePosition[1] < 0 && dir == 7)
                    return 3; // upper left corner
                if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize-1 && dir == 1)
                    return 9; // down left corner
                if (dir == 7)
                    return 9; // collision wall left
                if (dir == 4)
                    return 6; // collision wall left
                if (dir == 1)
                    return 3; // collision wall left
            }
            else if (Stage.actualProjectile.ProjectilePosition[0] > Stage.gridSize-1)
            {
                if (Stage.actualProjectile.ProjectilePosition[1] < 0 && dir == 9)
                    return 1; // upper right corner
                if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize-1 && dir == 3)
                    return 7; // down right corner
                if (dir == 9)
                    return 7; // collision wall right
                if (dir == 6)
                    return 4; // collision wall right
                if (dir == 3)
                    return 1; // collision wall right
            }

            if (Stage.actualProjectile.ProjectilePosition[1] < 0)
            {
                if (dir == 7)
                    return 1; // collision wall up
                if (dir == 8)
                    return 2; // collision wall up
                if (dir == 9)
                    return 3; // collision wall up
            }
            else if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize-1) 
            {
                if (dir == 1)
                    return 7; // collision wall down
                if (dir == 2)
                    return 8; // collision wall down
                if (dir == 3)
                    return 9; // collision wall down
            }
            else if (Stage.CheckObstacles(Stage.actualProjectile.ProjectilePosition[0],Stage.actualProjectile.ProjectilePosition[1]))
            {
                //System.Console.WriteLine("ENTRÓ");
                if (Stage.actualProjectile.ProjectilePosition[1] < 0 && dir == 9)
                    return 1; // upper right corner
                if (Stage.actualProjectile.ProjectilePosition[1] > Stage.gridSize-1 && dir == 3)
                    return 7; // down right corner
                if (dir == 9)
                    return 7; // collision wall right
                if (dir == 6)
                    return 4; // collision wall right
                if (dir == 3)
                    return 1; // collision wall right
                if (dir == 1)
                    return 7; // collision wall down
                if (dir == 2)
                    return 8; // collision wall down
                if (dir == 3)
                    return 9; // collision wall down
                if (dir == 7)
                    return 1; // collision wall up
                if (dir == 8)
                    return 2; // collision wall up
                if (dir == 9)
                    return 3; // collision wall up
                if (dir == 7)
                    return 9; // collision wall left
                if (dir == 4)
                    return 6; // collision wall left
                if (dir == 1)
                    return 3; // collision wall left
            }


            return dir;
        }
    }
}