using System;
using System.Collections.Generic;
using System.Text;

namespace Granata
{
    public class Stage
    {
        /***** INITIAL VARIABLES *****/
        // MAgic Numbers
        public int MIN_NUMBER_OF_OBSTACLE = 5;
        public int MAX_NUMBER_OF_OBSTACLE = 8;

        // Grid size
        public int gridSize = 30;

        // Call and define variables
        public List<Obstacle> objectObstacleList = new List<Obstacle>();

        public int selectedNumberOfObstacle = 5;


        // /*//// Test variables ////*/ 
        // public int player1X = 0;
        // public int player1Y = 0;
        // public int player2X = gridSize - 1;
        // public int player2Y = gridSize - 1;
        // public int obstacleX1 = 10;
        // public int obstacleX2 = 15;
        // public int obstacleY1 = 15;
        // public int obstacleY2 = 15;
        // public int projectileX = 6;
        // public int projectileY = 10;
        // public string player1Symbol = "🤡";
        // public string player2Symbol = "👺";
        // public string obstacleSymbol = "🈴";
        // public string projectileSymbol = "🔴";
        // public string voidSymbol = "🔴";



        //Function to initialize the object obstacle
        internal void InitializeObstacule()
        {
            var obstacle = new Obstacle();
        }

        //Function to initialize the object player
        //TODO: Initial conditions for player
        internal void InitializePlayer(int numberOfPlayer)
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < numberOfPlayer; i++)
            {
                players.Add(new Player());
            }
        }

        // Function to get obstacles
        public void SetListObstacle()
        {
            Random selectObstacle = new Random();
            Random selectNumberOfObstacle = new Random();

            selectedNumberOfObstacle = selectNumberOfObstacle.Next(MIN_NUMBER_OF_OBSTACLE, MAX_NUMBER_OF_OBSTACLE);

            for (int i = 0; i < selectedNumberOfObstacle; i++)
            {
                objectObstacleList[i].Add(obstacle.selectionOfObstacle[selectObstacle.Next(10)]);
            }
        }

        public void RandomSetPosition()
        {
            List<int> checkObstaclesX = new List<int>();
            List<int> checkObstaclesY = new List<int>();
            Random random = new Random();
            int randomNumberX = 0;
            int randomNumberY = 0;

            for (int i = 0; i < objectObstacleList.Count; i++)
            {
                while (true)
                {
                    int count = 0;
                    randomNumberX = random.Next(4, gridSize - 4);
                    randomNumberY = random.Next(4, gridSize - 4);

                    for (int j = 0; j < checkObstaclesX.Count; j++)
                    {
                        if ((randomNumberX > checkObstaclesX[j]) && (randomNumberX < checkObstaclesX[j] + objectObstacleList[j].width) &&
                            (randomNumberY > checkObstaclesY[j]) && (randomNumberY < checkObstaclesY[j] + objectObstacleList[j].height))
                        {
                            count++;
                        }
                    }

                    if (count == 0)
                    {
                        break;
                    }
                }

                objectObstacleList[i].positionX1 = randomNumberX; // Avoid corners
                checkObstaclesX.Add(randomNumberX);

                objectObstacleList[i].positionY1 = randomNumberY; // Avoid corners
                checkObstaclesY.Add(randomNumberY);

                objectObstacleList[i].positionX2 = objectObstacleList[i].positionX1 + objectObstacleList[i].width;
                objectObstacleList[i].positionY2 = objectObstacleList[i].positionY1 + objectObstacleList[i].height;
            }
        }

        public void RenderGrid()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if (CheckPlayers(x, y))
                    {
                        continue;
                    }
                    else if (x == projectileX && y == projectileY)
                    {
                        Console.Write(projectileSymbol);
                        continue;
                    }
                    else if (CheckObstacles(x, y))
                    {
                        Console.Write(obstacle.symbol);
                        continue;
                    }
                    Console.Write("⬛");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool CheckPlayers(int x, int y)
        {
            for (int i = 0; i < players.Count(); i++)
            {
                if ((x == players[i].positionX && y == players[i].positionY))
                {
                    Console.Write(players[i].symbol);
                    return true;
                }
            }
            return false;
        }

        public bool CheckObstacles(int x, int y)
        {
            for (int i = 0; i < selectedNumberOfObstacle; i++)
            {
                for (int j = objectObstacleList[i].positionX1; j < objectObstacleList[i].positionX2; j++)
                {
                    for (int k = objectObstacleList[i].positionY1; k < objectObstacleList[i].positionY2; k++)
                    {
                        if ((x == j && y == k))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}