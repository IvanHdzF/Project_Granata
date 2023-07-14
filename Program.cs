using System;
using System.Collections.Generic;
using System.Text;
namespace ObstacleGame
{
    class Program
    {
        static void Main()
        {
            Stage stage = new Stage();
            stage.GenerateObstacles(10); // Generate 10 obstacles
            stage.PrintStage(); // Print the grid with obstacles
            List<Obstacle> obstacles = stage.GetObstacles();
            foreach (Obstacle obstacle in obstacles)
            {
                Console.WriteLine($"Obstacle at position = (X1 = {obstacle.PositionX1}, Y1 = {obstacle.PositionY1}, X2 = {obstacle.PositionX2}, Y2 = {obstacle.PositionY2}) | HP: {obstacle.HitPoints}, H = {obstacle.Width}, W = {obstacle.Height} ");
            }
            
        }
    }
}
