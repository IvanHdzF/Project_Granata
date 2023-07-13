using System;

namespace Granata
{
    class Program
    {
        //Config parameters here:
        static int maxTurnCount = 30; //TODO: We should make the user change this settings from a config file
        static int playerCount = 2;
        static int refillCooldown = 10;

        static void Main(string[] args)
        {
            Game();

        }

        static void Game()
        {
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
