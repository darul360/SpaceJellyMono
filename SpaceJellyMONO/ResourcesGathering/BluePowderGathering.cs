using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceJellyMONO.GameObjectComponents;


namespace SpaceJellyMONO.ResourcesGathering
{
    class BluePowderGathering:GameComponent
    {
        Game1 game;
        float timer = 10;
        const float TIMER = 10;
        int numberOfWaterPumps = 0;
        List<GameObject> powderMines = new List<GameObject>();

        public BluePowderGathering(Game1 game) : base(game)
        {
            this.game = game;
        }

        private int countWaterPumps()
        {
            powderMines.Clear();
            foreach (GameObject go in game.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "mine")
                {
                    powderMines.Add(go);
                }
            }
            return powderMines.Count;
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
                    foreach (GameObject mine in powderMines)
                        game.renderEngine.FloatingTextRenderer.Add(new FloatingText(Vector3.Zero, mine.WorldTransform, "+3", Color.Blue));
                    game.resourcesStatistics.bluePowderStats += 3 * numberOfWaterPumps;
                }
                timer = TIMER;   //Reset Timer
            }


        }
    }
}
