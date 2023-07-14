using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public Projectile actualProjectile { get; set; }

        public Dictionary<string, int> Projectiles { get; set; }

        public Player(string symbol, int hp, string name, int score, int actions, int[] position, Dictionary<string, int> projectiles)
        {
            this.Symbol = symbol;
            this.HP = hp;
            this.Position = position;
            this.Name = name;
            this.Score = score;
            this.Actions = actions;
            this.Projectiles = projectiles;
        }


        public void PrintInventory()
        {
            System.Console.WriteLine($"Player 1 name: {Name}");
            System.Console.WriteLine("CURRENT INVENTORY:");
            foreach (var proj in Projectiles)
            {
                System.Console.WriteLine($"{proj.Key.Symbol} / {Projectiles[proj.Key]}");
            }

        }

        public void Move(char direction)
        {
            //validar si choco con mina, obstaculo y eje de mapa
            int gridSize = 30;
            switch (direction)
            {
                case 'w':
                    if (Position[1] > 0) Position[1]--;
                    break;
                case 'a':
                    if (Position[0] > 0) Position[0]--;
                    break;
                case 's':
                    if (Position[1] < gridSize - 1) Position[1]++;
                    break;
                case 'd':
                    if (Position[0] < gridSize - 1) Position[0]++;
                    break;
                case ' ':
                    Console.WriteLine("Direccion");
                    int dir = int.Parse(Console.ReadLine());
                    Console.WriteLine("Tipo de Proyectil");
                    int type = int.Parse(Console.ReadLine());

                    for (int i = 0; i < actualProjectile.Frames; i++)
                    {
                        int mydelay = 500;
                        Throw(dir, type);
                        dir = collision(dir);
                        //System.Console.WriteLine($"ubicacion proyectil en X:{ppX}  Y:{ppY}");
                        Thread.Sleep(mydelay);
                    }
                default:
                    Console.WriteLine("Invalid move. Try again.");
                    break;
            }
        }
        public void Throw(int direction, int type)
        {
            //validation player still has projectiles types left            
            foreach (var proj in this.Projectiles)
            {
                if (proj.Key.Tipo == type && this.Projectiles[proj.Key] > 0)
                {
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
                    actualProjectile.Position[0]--;
                    actualProjectile.Position[1]++;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 2:
                    actualProjectile.Position[1]++;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 3:
                    actualProjectile.Position[0]++;
                    actualProjectile.Position[1]++;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 4:
                    actualProjectile.Position[0]--;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 6:
                    actualProjectile.Position[0]++;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 7:
                    actualProjectile.Position[0]--;
                    actualProjectile.Position[1]--;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 8:
                    actualProjectile.Position[1]--;
                    RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 9:
                    actualProjectile.Position[0]++;
                    actualProjectile.Position[1]--;
                    RenderGrid();
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
        static int collision(int dir)
        {
            if (actualProjectile.Position[0] == this.Position[0] && actualProjectile.Position[1] == this.Position[1]) // check collision with player 1
            {
                Console.WriteLine("Player 2 hit Player 1! Game over."); // enviar daño player 1
                
            }
            if (ppX == player2X && ppY == player2Y) // check collision with player 1
            {
                Console.WriteLine("Player 1 hit Player 2! Game over."); // enviar daño player 1
                
            }
            dir = ChangeDirection(dir);// check collision with obstacule
            return dir;
        }
        static int ChangeDirection(int dir)
        {
            if (ppX < 0)
            {
                if (ppY < 0 && dir == 7)
                    return 3; // upper left corner
                if (ppY > 29 && dir == 1)
                    return 9; // down left corner
                if (dir == 7)
                    return 9; // collision wall left
                if (dir == 4)
                    return 6; // collision wall left
                if (dir == 1)
                    return 3; // collision wall left
            }
            else if (ppX > 29)
            {
                if (ppY < 0 && dir == 9)
                    return 1; // upper right corner
                if (ppY > 29 && dir == 3)
                    return 7; // down right corner
                if (dir == 9)
                    return 7; // collision wall right
                if (dir == 6)
                    return 4; // collision wall right
                if (dir == 3)
                    return 1; // collision wall right
            }

            if (ppY < 0)
            {
                if (dir == 7)
                    return 1; // collision wall up
                if (dir == 8)
                    return 2; // collision wall up
                if (dir == 9)
                    return 3; // collision wall up
            }
            else if (ppY > 29) //colocar limite derecho de stage en los '29'
            {
                if (dir == 1)
                    return 7; // collision wall down
                if (dir == 2)
                    return 8; // collision wall down
                if (dir == 3)
                    return 9; // collision wall down
            }
            // collision obstacule


            return dir;
        }



    }
}