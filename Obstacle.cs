using System;
using System.Collections.Generic;
using System.Text;

namespace Granata
{
    public class Obstacle
    {
        private const int MinObstacleSize = 2;
        private const int MaxObstacleSize = 3;

        private static Random random = new Random();

        private static List<Obstacle> selectionOfObstacle = new List<Obstacle>();

        public int positionX1 { get; set; }
        public int positionX2 { get; set; }
        public int positionY1 { get; set; }
        public int positionY2 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int hitPoints { get; private set; }
        public static string obstacleSymbol = "🈴";


        public Obstacle(int w, int h)
        {
            width = w;
            height = h;

            hitPoints = 5;

            selectionOfObstacle.Add(this);
        }

        public void HitObstacle()
        {
            if (hitPoints > 0)
            {
                hitPoints--;
                Console.WriteLine("Obstacle hit! Remaining hit points: " + hitPoints);
            }
            else
            {
                Console.WriteLine("Obstacle has no more hit points!");
            }
        }

        public static List<Obstacle> GenObstacleType()
        {
            for (int i = 0; i < 5; i++)
            {
                Obstacle obstacle = GenerateRandomObstacle();
                selectionOfObstacle.Add(obstacle);
            }

            return selectionOfObstacle;
        }

        //TODO: This generation should NOT be random, must be predefined
        public static Obstacle GenerateRandomObstacle()
        {
            int w = random.Next(MinObstacleSize, MaxObstacleSize + 1);
            int h = random.Next(MinObstacleSize, MaxObstacleSize + 1);

            // int positionX = random.Next(0, gridSize - w + 1);
            // int positionY = random.Next(0, gridSize - h + 1);
            return new Obstacle(w, h);

        }
    }
}
