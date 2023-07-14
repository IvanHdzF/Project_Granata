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

        //public Projectile[] Projectile { get; set; }

        public Dictionary<Projectile, int> Projectiles { get; set; }

        public Player(string symbol, int hp, string name, int score, int actions, int[] position, Dictionary<Projectile, int> projectiles)
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
                case '1':
                    Throw(1, 1);
                    break;
                case '2':
                    Throw(2, 1);
                    break;
                case '3':
                    Throw(3, 1);
                    break;
                case '4':
                    Throw(4, 1);
                    break;
                case '6':
                    Throw(6, 1);
                    break;
                case '7':
                    Throw(7, 1);
                    break;
                case '8':
                    Throw(8, 1);
                    break;
                case '9':
                    Throw(9, 1);
                    break;
                default:
                    Console.WriteLine("Invalid move. Try again.");
                    break;
            }
        }
        public static Projectile useProj1 = new Projectile(1, Position, 1, 25, 10, 1, "PP");

        public void Throw(int direction, int type)
        {
            //  validation player still has projectiles types left
            foreach (var proj in this.Projectiles)
            {
                if (proj.Key.Tipo == type && this.Projectiles[proj.Key] > 0)
                {
                    this.Projectiles[proj.Key]--;

                    break;
                }
                else
                {
                    //if not projectile type valid nor projectile type left in players deck, return exit code 1 error
                    break;
                }
            }



            if (type == 1)
            {
                useProj1.Tipo = 1;
                useProj1.Position = this.Position;
                useProj1.Direction = direction;
                useProj1.Damage = 25;
                useProj1.Frames = 10;
                useProj1.Area = 1;
                useProj1.Symbol = "PP";
            }
            else if (type == 2)
            {
                useProj1.Tipo = 2;
                useProj1.Position = this.Position;
                useProj1.Direction = direction;
                useProj1.Damage = 25;
                useProj1.Frames = 10;
                useProj1.Area = 1;
                useProj1.Symbol = "GR";
            }
            else
            {
                useProj1.Tipo = 3;
                useProj1.Position = this.Position;
                useProj1.Direction = direction;
                useProj1.Damage = 25;
                useProj1.Frames = 10;
                useProj1.Area = 1;
                useProj1.Symbol = "ST";
            }




            int mydelay = 500;
            switch (direction)
            {
                case 1:
                    useProj1.Position[0]--;
                    useProj1.Position[1]++;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 2:
                    useProj1.Position[1]++;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 3:
                    useProj1.Position[0]++;
                    useProj1.Position[1]++;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 4:
                    useProj1.Position[0]--;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 6:
                    useProj1.Position[0]++;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 7:
                    useProj1.Position[0]--;
                    useProj1.Position[1]--;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 8:
                    useProj1.Position[1]--;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                case 9:
                    useProj1.Position[0]++;
                    useProj1.Position[1]--;
                    Program.RenderGrid();
                    Thread.Sleep(mydelay);
                    break;

                default:
                    Console.WriteLine("Invalid move. Try again.");
                    Thread.Sleep(mydelay);
                    break;
            }











        }
    }
}