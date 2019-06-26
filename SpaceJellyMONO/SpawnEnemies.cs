using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO
{
    public class SpawnEnemies:DrawableGameComponent
    {
        Game1 game1;
        float timer = 5;
        const float TIMER = 5;
        List<Vector2> tempNodes;

        int i;
        GameObject go1, go2, go3, go4, go5, go6;
        bool start = false;
        public SpawnEnemies(Game1 game1):base(game1)
        {
            this.game1 = game1;
            i = 0;
            tempNodes = new List<Vector2>();
        }
        public override void Draw(GameTime gameTime)
        {
            string workerKey = game1.scene.FindKeyOfObject(game1.spawn);
            if (workerKey != null)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

                timer -= elapsed;
                if (timer < 0)
                {
                    go1 = new Enemy("zarlok", game1, new Vector3(89, 0, 11f), 0f, 3.14f, 0f, 0.02f, true, "enemy", 0.5f * 0.9f);
                    game1.scene.AddSceneObject("zarlokS" + i, go1);
                    game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("1", 20, true);
                    i++;

                    go2 = new Enemy("zarlok", game1, new Vector3(89, 0, 10f), 0f, 3.14f, 0f, 0.02f, true, "enemy", 0.5f * 0.9f);
                    game1.scene.AddSceneObject("zarlokS" + i, go2);
                    game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("1", 20, true);
                    i++;
                    start = true;
                    timer = TIMER;
                    game1.enemiesRepository.AddToRepo(go1);
                    game1.enemiesRepository.AddToRepo(go2);
                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (start)
            {
                spiralSpread(16, 15, 3);
                go1.targetX = (int)tempNodes[0].X;
                go1.targetY = (int)tempNodes[0].Y;
                go1.isMoving = true;
                go2.targetX = (int)tempNodes[1].X;
                go2.targetY = (int)tempNodes[1].Y;
                go2.isMoving = true;
                start = false;
            }
            base.Update(gameTime);
        }

        public void spiralSpread(int i, int j, int howManySelected)
        {
            tempNodes.Add(new Vector2(i, j));
            // (di, dj) is a vector - direction in which we move right now
            int di = 1;
            int dj = 0;
            // length of current segment
            int segment_length = 1;

            // current position (i, j) and how much of current segment we passed
            //int i = 0;
            //int j = 0;
            int segment_passed = 0;
            for (int k = 0; k < howManySelected; ++k)
            {
                // make a step, add 'direction' vector (di, dj) to current position (i, j)
                i += di;
                j += dj;
                ++segment_passed;
                tempNodes.Add(new Vector2(i, j));

                if (segment_passed == segment_length)
                {
                    // done with current segment
                    segment_passed = 0;

                    // 'rotate' directions
                    int buffer = di;
                    di = -dj;
                    dj = buffer;

                    // increase segment length if necessary
                    if (dj == 0)
                    {
                        ++segment_length;
                    }
                }
            }
        }
    }
}
