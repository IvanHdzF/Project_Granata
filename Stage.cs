using System;
using System.Text;

namespace Granata
{
    public class Stage
    {
        /***** INITIAL VARIABLES *****/
        // MAgic Numbers
        public static int MIN_NUMBER_OF_OBSTACLE = 5;
        public static int MAX_NUMBER_OF_OBSTACLE = 8;

        // Grid size
        public static int gridSize = 30;

        // Call and define variables
        public static List<Obstacle> setObstacle = new List<Obstacle>;
        public static int selectedNumberOfObstacle = 5;

        /*//// Test variables ////*/ 
        public static int player1X = 0;
        public static int player1Y = 0;
        public static int player2X = gridSize - 1;
        public static int player2Y = gridSize - 1;
        public static int obstacleX1 = 10;
        public static int obstacleX2 = 15;
        public static int obstacleY1 = 15;
        public static int obstacleY2 = 15;
        public static int projectileX = 6;
        public static int projectileY = 10;
        public static string player1Symbol = "🤡";
        public static string player2Symbol = "👺";


        //Function to initialize the object obstacle
        internal void initializeObstacule()
        {
            var obstacle = new Obstacle();
        }

        //Function to initialize the object player
        internal void initializePlayer()
        {
            var player1 = new Player();
            var player2 = new Player();
        }

        //obstacle.obstaclearray[random].posicionx1 =   

        // Function to get obstacles
        public static void setObstacle()
        {
            Random selectObstacle = new Random();
            Random selectNumberOfObstacle = new Random();

            selectedNumberOfObstacle = selectNumberOfObstacle.Next(MIN_NUMBER_OF_OBSTACLE, MAX_NUMBER_OF_OBSTACLE)

            for (int i = 0; i < selectedNumberOfObstacle; i++)
            {
                setObstacle[i] = obstacle.selectionOfObstacle[selectObstacle.Next(10)];
            }
        }

        public static void RenderGrid()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    if (x == player1X && y == player1Y)
                    {
                        Console.Write(player1Symbol);
                    }
                    else if (x == player2X && y == player2Y)
                    {
                        Console.Write(player2Symbol);
                    }

                    for (int i = 0; i < selectedNumberOfObstacle; i++)
                    {
                        for (int j = setObstacle[i].obstacleX1; j < setObstacle[i].obstacleX2; j++)
                        {
                            for (int k = setObstacle[i].obstacleY1; k < setObstacle[i].obstacleY2; k++)
                            {
                                if ((x == j  && y == k ))
                                {
                                    Console.Write("🈴");
                                }
                            }
                        }
                    }

                    else if ((x == obstacleX1 && y == obstacleY1) || (x == obstacleX2 && y == obstacleY2))
                    {
                        Console.Write("🈴");
                    }
                    else if (x == projectileX && y == projectileY)
                    {
                        Console.Write("🔴");
                    }
                    else
                    {
                        Console.Write("⬛");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void PerformMove(char move, ref int x, ref int y)
        {
            switch (move)
            {
                case 'w':
                    if (y > 0) y--;
                    break;
                case 'a':
                    if (x > 0) x--;
                    break;
                case 's':
                    if (y < gridSize - 1) y++;
                    break;
                case 'd':
                    if (x < gridSize - 1) x++;
                    break;
                case 'x':
                    Console.WriteLine("X coordinate");
                    int i=int.Parse(Console.ReadLine());
                    Console.WriteLine("Y coordinate");
                    int j = int.Parse(Console.ReadLine());
                    ThrowProjectile(i, j);
                    break;
                default:
                    Console.WriteLine("Invalid move. Try again.");
                    break;
            }
        }

        public static void ThrowProjectile(int x, int y)
        {
            Console.WriteLine($"Player threw a projectile at ({x}, {y})!");

            // Add logic for projectile collision and effects
        }
    }
}