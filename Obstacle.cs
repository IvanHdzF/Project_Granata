using System;
using System.Collections.Generic;
using System.Text;

namespace ObstacleGame
{
    public class Obstacle
    {
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


    }
//test
    public class Stage
    {
        private const int GridSize = 30;
        private const int MinObstacleSize = 2;
        private const int MaxObstacleSize = 3;

        private readonly Obstacle[,] grid;
        private readonly Random random;
        private readonly List<Obstacle> obstacles; // New list field

        public Stage()
        {
            Console.OutputEncoding = Encoding.UTF8;
            grid = new Obstacle[GridSize, GridSize];
            random = new Random();
            obstacles = new List<Obstacle>(); // Initialize the list
        }

        public void GenerateObstacles(int obstacleCount)
        {
            for (int i = 0; i < obstacleCount; i++)
            {
                int width = random.Next(MinObstacleSize, MaxObstacleSize + 1);
                int height = random.Next(MinObstacleSize, MaxObstacleSize + 1);

                int positionX = random.Next(0, GridSize - width + 1);
                int positionY = random.Next(0, GridSize - height + 1);

                var obstacle = new Obstacle(positionX, positionY, width, height);
                PlaceObstacle(obstacle);
                obstacles.Add(obstacle); // Add the obstacle to the list
            }
        }

        private void PlaceObstacle(Obstacle obstacle)
        {
            for (int i = obstacle.PositionX1; i <= obstacle.PositionX2; i++)
            {
                for (int j = obstacle.PositionY1; j <= obstacle.PositionY2; j++)
                {
                    grid[i, j] = obstacle;
                }
            }
        }

        public void PrintStage()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (grid[i, j] != null)
                    {
                        Console.Write("⬜"); // Full block character for obstacle
                    }
                    else
                    {
                        Console.Write("⬛"); // Empty space
                    }
                }
                Console.WriteLine();
            }
        }

        public List<Obstacle> GetObstacles()
        {
            return obstacles;
        }
    }
}
