using System;

class Program
{
    static void Main()
    {
        int gridSize = 30;
        char[,] grid = new char[gridSize, gridSize];

        Random random = new Random();

        void InitializeGrid()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = '.';
                }
            }
        }

        void AddObstacle(int x, int y, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[y + j, x + i] = '■';
                }
            }
        }  

        void DisplayGrid()
        {
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        void RefreshGrid()
        {
            InitializeGrid();

            // Generate random 2x2 obstacles
            for (int k = 0; k < 10; k++)
            {
                int x = random.Next(4, gridSize - 4); // Avoid corners
                int y = random.Next(4, gridSize - 4); // Avoid corners
                AddObstacle(x, y, 2, 2);
            }

            // Generate random 3x2 obstacles
            for (int k = 0; k < 10; k++)
            {
                int x = random.Next(4, gridSize - 4); // Avoid corners
                int y = random.Next(4, gridSize - 4); // Avoid corners
                AddObstacle(x, y, 3, 2);
            }

            Console.Clear();
            DisplayGrid();
        }

        // Initialize the grid
        InitializeGrid();

        // Display the initial grid
        while (true)
        {
            Console.WriteLine("Press any key to refresh the grid...");
            Console.ReadKey(true);

            // Refresh the grid
            RefreshGrid();

        }
    }
}
