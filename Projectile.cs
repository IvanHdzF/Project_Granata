using System;
namespace Granata
{
    public class Projectile
    {
        public int Tipo {get;set;}
        public int[] Position {get;set;}
        public int Direction {get;set;}
        public int Damage {get;set;}
        public int Frames {get;set;}
        public int Area {get;set;}
        public string Symbol {get;set;}
        

        public Projectile(int tipo, int[] position, int direction, int damage, int frames, int area, string symbol)
        {
            this.Tipo = tipo; //tipo 1 PP, tipo 2 Granada, tipo 3 Sticky
            this.Position = position;
            this.Direction = direction;
            this.Damage = damage;
            this.Frames = frames;
            this.Area = area;
            this.Symbol = symbol;

        }

    }
}