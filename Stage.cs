using System;
using System.Collections.Generic;
using System.Text;

namespace Granata
{
    public class Stage
    {
        /***** INITIAL VARIABLES *****/
        // MAgic Numbers
        //TODO: Number of generated objects is wrong
        public static int MIN_NUMBER_OF_OBSTACLE = 10;
        public static int MAX_NUMBER_OF_OBSTACLE = 14;

        // Grid size
        public static int gridSize = 30;

        // Call and define variables
        public static List<Obstacle> objectObstacleList = new List<Obstacle>();
        public static List<Player> players = new List<Player>();

        public static int selectedNumberOfObstacle = 5;
        public static List<Obstacle> selectionOfObstacle;
        public static Projectile actualProjectile;
        private static string matrixLine="";

        //Function to initialize the object obstacle
        static internal void InitializeObstacule()
        {
            selectionOfObstacle = Obstacle.GenObstacleType();
        }

        //Function to initialize the object player

        static internal void InitializePlayer(int numberOfPlayer)
        {
            string[] playerSymbols = { "🤡", "👺", "🐒", "👽" };//TODO: Implement for more players
            int[] positions = {
            1, 1, //Position for player 1
            gridSize - 2, gridSize - 2, //Position for player 2
            1, gridSize - 2, //Position for player 3
            gridSize - 2, 1, //Position for player 4
            };

            for (int i = 0; i < numberOfPlayer; i++)
            {
                var dict = new Dictionary<string, int>(){
                {"1",10}, //rock
                {"2",20}, //grenade
                {"3",2} //mine
            };
                int[] position = { positions[i* 2] , positions[i* 2+1]  };

                players.Add(new Player(playerSymbols[i], 255, $"Player {i + 1}", 0, position, dict));
            }
        }

        // Function to get obstacles
        public static void SetListObstacle()
        {
            Random selectNumberOfObstacle = new Random();

            selectedNumberOfObstacle = selectNumberOfObstacle.Next(MIN_NUMBER_OF_OBSTACLE, MAX_NUMBER_OF_OBSTACLE);
            
            Console.WriteLine(selectedNumberOfObstacle);

            for (int i = 0; i < selectedNumberOfObstacle; i++)
            {
                objectObstacleList.Add(selectionOfObstacle[i]);
            }
        }

        public static void RandomSetPosition()
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

        public static void RenderGrid()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            System.Console.WriteLine("\n");


            matrixLine="";
            for (int y = 0; y < gridSize; y++)
            {
                
                for (int x = 0; x < gridSize; x++)
                {
                    if (CheckPlayers(x, y))
                    {
                        continue;
                    }
                    else if (CheckObstacles(x, y))
                    {
                        continue;
                    }
                    else if (actualProjectile != null)
                    {
                        if (x == actualProjectile.ProjectilePosition[0] && y == actualProjectile.ProjectilePosition[1])
                        {
                            matrixLine+=actualProjectile.Symbol;
                            continue;
                        }
                    }


                    matrixLine+="⬛";
                }
                matrixLine+="\n";
            }
            System.Console.WriteLine(matrixLine);
            System.Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public static bool CheckPlayers(int x, int y)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if ((x == players[i].Position[0] && y == players[i].Position[1]))
                {
                    matrixLine+=players[i].Symbol;
                    return true;
                }
            }
            return false;
        }

        public static bool CheckObstacles(int x, int y)
        {
 
            for (int i = 0; i < objectObstacleList.Count; i++)
            {   
                for (int k = objectObstacleList[i].positionY1; k < objectObstacleList[i].positionY2 + 1; k++)
                {
                    for (int j = objectObstacleList[i].positionX1; j < objectObstacleList[i].positionX2 + 1; j++)
                    {
                        if ((y == k && x == j))
                        {
                            matrixLine+=Obstacle.obstacleSymbol;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

    }
}