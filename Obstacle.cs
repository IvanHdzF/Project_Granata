using System;
using System.Collections.Generic;
using System.Text;

namespace ObstacleGame
{
    public class Obstacle
    {
        private const int MinObstacleSize = 2;
        private const int MaxObstacleSize = 3;

        private static Random random = new Random();

        private static List<Obstacle> obstacles = new List<Obstacle>();

        public int PositionX1 { get; }
        public int PositionX2 { get; }
        public int PositionY1 { get; }
        public int PositionY2 { get; }
        public int Width { get; }
        public int Height { get; }
        public int HitPoints { get; private set; }

        public Obstacle(int positionX1, int positionY1, int width, int height)
        {
            PositionX1 = positionX1;
            PositionY1 = positionY1;
            Width = width;
            Height = height;

            PositionX2 = positionX1 + width - 1;
            PositionY2 = positionY1 + height - 1;

            HitPoints = 5;

            obstacles.Add(this);
        }

        public void HitObstacle()
        {
            if (HitPoints > 0)
            {
                HitPoints--;
                Console.WriteLine("Obstacle hit! Remaining hit points: " + HitPoints);
            }
            else
            {
                Console.WriteLine("Obstacle has no more hit points!");
            }
        }

        public static List<Obstacle> GetAllObstacles()
        {
            return obstacles;
        }

        public static Obstacle GenerateRandomObstacle(int gridSize)
        {
            int width = random.Next(MinObstacleSize, MaxObstacleSize + 1);
            int height = random.Next(MinObstacleSize, MaxObstacleSize + 1);

            int positionX = random.Next(0, gridSize - width + 1);
            int positionY = random.Next(0, gridSize - height + 1);

            return new Obstacle(positionX, positionY, width, height);
        }
    }
}
