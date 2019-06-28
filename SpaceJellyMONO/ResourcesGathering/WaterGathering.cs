using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO.ResourcesGathering
{
    public class WaterGathering : GameComponent
    {
        Game1 game;
        float timer = 10;         
        const float TIMER = 10;
        int numberOfWaterPumps = 0;
        //FloatingText floatingText;
        List<GameObject> waterPumps = new List<GameObject>();
        public WaterGathering(Game1 game) : base(game)
        {
            this.game = game;
        }

        private int countWaterPumps()
        {
            waterPumps.Clear();
            foreach(GameObject go in game.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "waterpump")
                    waterPumps.Add(go);
            }
            return waterPumps.Count;
        }

        public override void Update(GameTime gameTime)
        {
            //floatingText = new FloatingText(game, temp.transform, "siema");
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapsed;
            if (timer < 0)
            {
                numberOfWaterPumps = countWaterPumps();
                if (numberOfWaterPumps != 0)
                {
                    foreach(GameObject waterPump in waterPumps)
                        game.renderEngine.FloatingTextRenderer.Add(new FloatingText(Vector3.Zero, waterPump.WorldTransform, "+3", Color.Aqua));
                    game.resourcesStatistics.waterStats += 3 * numberOfWaterPumps;
                }
                timer = TIMER;   //Reset Timer
                
            }
        }
    }
}
