﻿using System;
using System.IO;

namespace Granata
{
    class Program
    {
        //Config parameters here:
        static int maxTurnCount { get; set; }
        static int playerCount { get; set; }
        static int supplyCooldown { get; set; }
        static int stageSize { get; set; }

        //Max values for config parameters:
        static int maxTurnCountMax = 60;
        static int playerCountMax = 4;
        static int supplyCooldownMax = 61;
        static int stageSizeMax = 100;
        static string filePath { get; } = "config.txt";

        static void Main(string[] args)
        {
            InitConfig();
            ReadConfig();
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
                return;
            }
            maxTurnCount = 30;
            playerCount = 2;
            supplyCooldown = 10;
            stageSize = 30;
            System.Console.WriteLine("Config.txt file doesn't exist, creating one...");
            WriteConfig(maxTurnCount, playerCount, supplyCooldown, stageSize);
        }

        static void UpdateConfig()
        {
            int temp = maxTurnCount;
            System.Console.WriteLine("Update the following settings:");
            System.Console.WriteLine("Maximum number of turns: (default is 30)");
            Methods.ValidateConfigInput(ref temp, 30, maxTurnCountMax);
            maxTurnCount = temp;
            System.Console.WriteLine("Number of players (up to 4, default is 2):");
            Methods.ValidateConfigInput(ref temp, 2, playerCountMax);
            playerCount = temp;
            System.Console.WriteLine("Number of turns between each projectile supply (default is 10):");
            Methods.ValidateConfigInput(ref temp, 10, supplyCooldownMax);
            supplyCooldown = temp;
            System.Console.WriteLine("Size of stage, in spaces (default is 30):");
            Methods.ValidateConfigInput(ref temp, 30, stageSizeMax);
            stageSize = temp;
            ClearConfig();
            WriteConfig(maxTurnCount, playerCount, supplyCooldown, stageSize);
        }
        static void ClearConfig()
        {
            File.Delete(filePath);
        }
        static void ReadConfig()
        {
            using (var sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {
                        var split = line.Split("=");
                        var selector = split[0].Trim();
                        switch (selector)
                        {
                            case "maxTurnCount":
                                maxTurnCount = int.Parse(split[1].Trim());
                                break;
                            case "playerCount":
                                playerCount = int.Parse(split[1].Trim());
                                break;
                            case "supplyCooldown":
                                supplyCooldown = int.Parse(split[1].Trim());
                                break;
                            case "stageSize":
                                stageSize = int.Parse(split[1].Trim());
                                break;
                        }
                    }
                    catch (System.Exception)
                    {
                        throw;
                    }

                }
            }

        }
        static void WriteConfig(int maxTurnCount, int playerCount, int supplyCooldown, int stageSize)
        {
            try
            {
                //Pass the filepath to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine($"maxTurnCount = {maxTurnCount}");
                sw.WriteLine($"playerCount = {playerCount}");
                sw.WriteLine($"supplyCooldown = {supplyCooldown}");
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
            //CreateStage(stageSize);
            for (int turnCounter = 0; turnCounter < maxTurnCount; turnCounter++)
            {
                if (turnCounter % supplyCooldown == 0) //Each 10 turns we refill, we count turn 0 as also one were we supply the projectiles 
                {
                    Methods.supplyProjectiles();

                }
                Methods.PlayerTurn(turnCounter % playerCount);
                CheckWinner();
            }
        }
        //TODO: CheckWinner should return a ref to a player object
        static void CheckWinner()
        {

            //TODO: Go throught the list of players and check their HP, add them to a list only if their HP is > 0, If len(list) ==1 then print player object
        }

    }
}
