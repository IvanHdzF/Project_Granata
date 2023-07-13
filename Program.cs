using System;
using System.IO;

namespace Granata
{
    class Program
    {
        //Config parameters here:
        static int maxTurnCount { get; set; } //TODO: We should make the user change this settings from a config file
        static int playerCount { get; set; }
        static int refillCooldown { get; set; }
        static int stageSize { get; set; }
        static string filePath { get; }= "config.txt";

        static void Main(string[] args)
        {
            InitConfig();
            bool done = false;
            while (!done)
            {

                Console.WriteLine("Press ENTER to start game, c for configuration, q for quit");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "":
                        Game();
                        break;
                    case "c":
                        UpdateConfig();
                        break;
                    case "q":
                        done = true;
                        break;
                }
            }
            System.Console.WriteLine("Thanks for playing!!! :)");
        }

        static void InitConfig()
        {
            bool fileExists = File.Exists(filePath);
            if (fileExists)
            {
                System.Console.WriteLine("Config.txt file doesn't exist, creating one...");

            }
            maxTurnCount = 30; //TODO: We should make the user change this settings from a config file
            playerCount = 2;
            refillCooldown = 10;
            stageSize = 30;
        }

        static void UpdateConfig()
        {
            
            WriteConfig();
        }
        static void WriteConfig()
        {
            try
            {
                //Pass the filepath to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine($"maxTurnCount = {maxTurnCount}");
                sw.WriteLine($"playerCount = {playerCount}");
                sw.WriteLine($"refillCooldown = {refillCooldown}");
                sw.WriteLine($"stageSize = {stageSize}");
                //Close the file
                sw.Close();
                System.Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        static void Game()
        {
            //TODO: Create stage needs a size parameter?
            //CreateEsc();
            for (int turnCounter = 0; turnCounter < maxTurnCount; turnCounter++)
            {
                if (turnCounter % refillCooldown == 0) //Each 10 turns we refill, we count turn 0 as also one were we supply the projectiles 
                {
                    Methods.supplyProjectiles();

                }
                Methods.PlayerTurn(turnCounter % playerCount);
            }
        }

    }
}
