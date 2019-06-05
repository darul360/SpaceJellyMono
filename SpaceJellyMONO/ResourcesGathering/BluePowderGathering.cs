using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace SpaceJellyMONO.ResourcesGathering
{
    class BluePowderGathering:GameComponent
    {
        Game1 game;
        float timer = 10;
        const float TIMER = 10;
        int numberOfWaterPumps = 0;
        public BluePowderGathering(Game1 game) : base(game)
        {
            this.game = game;
        }

        private int countWaterPumps()
        {
            int counter = 0;
            foreach (GameObject go in game.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "mine") counter++;
            }
            return counter;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                numberOfWaterPumps = countWaterPumps();
                if (numberOfWaterPumps != 0)
                {
                    game.resourcesStatistics.bluePowderStats += 3 * numberOfWaterPumps;
                }
                timer = TIMER;   //Reset Timer
            }


        }
    }
}
