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
        float timer = 15;
        const float TIMER = 15;
        int i;
        GameObject go1, go2, go3, go4, go5, go6;
        bool start = false;
        public SpawnEnemies(Game1 game1):base(game1)
        {
            this.game1 = game1;
            i = 0;
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
                    go1 = new Enemy("zarlok_poprawiony", game1, new Vector3(89, 0, 11f), 0f, 3.14f, 0f, 0.05f, true, "enemy", 0.5f * 0.9f);
                    game1.scene.AddSceneObject("zarlokS" + i, go1);
                    go1.finateSatemachine = game1.aniamteZarlok;
                    game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("Take 001", 20, true);
                    i++;

                    go2 = new Enemy("zarlok_poprawiony", game1, new Vector3(89, 0, 10f), 0f, 3.14f, 0f, 0.05f, true, "enemy", 0.5f * 0.9f);
                    game1.scene.AddSceneObject("zarlokS" + i, go2);
                    go2.finateSatemachine = game1.aniamteZarlok;
                    game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("Take 001", 20, true);
                    i++;

                    go3 = new Enemy("zarlok_poprawiony", game1, new Vector3(89, 0, 9f), 0f, 3.14f, 0f, 0.05f, true, "enemy", 0.5f * 0.9f);
                    game1.scene.AddSceneObject("zarlokS" + i, go3);
                    go3.finateSatemachine = game1.aniamteZarlok;
                    game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("Take 001", 20, true);
                    i++;

                    //go4 = new Enemy("zarlok_poprawiony", game1, new Vector3(87, 0, 11f), 0f, 3.14f, 0f, 0.05f, true, "enemy", 0.5f * 0.9f);
                    //game1.scene.AddSceneObject("zarlokS" + i,go4);
                    //go4.finateSatemachine = game1.aniamteZarlok;
                    //game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("Take 001", 20, true);
                    //i++;

                    //go5 = new Enemy("zarlok_poprawiony", game1, new Vector3(87, 0, 10f), 0f, 3.14f, 0f, 0.05f, true, "enemy", 0.5f * 0.9f);
                    //game1.scene.AddSceneObject("zarlokS" + i,go5);
                    //go5.finateSatemachine = game1.aniamteZarlok;
                    //game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("Take 001", 20, true);
                    //i++;

                    //go6 = new Enemy("zarlok_poprawiony", game1, new Vector3(87, 0, 9f), 0f, 3.14f, 0f, 0.05f, true, "enemy", 0.5f * 0.9f);
                    //game1.scene.AddSceneObject("zarlokS" + i,go6);
                    //go6.finateSatemachine = game1.aniamteZarlok;
                    //game1.scene.SceneObjects["zarlokS" + i].StartAnimationClip("Take 001", 20, true);
                    //i++;
                    start = true;
                    timer = TIMER;

                }
            }
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (start)
            {

                go1.targetX = (int)15;
                go1.targetY = (int)15;
                go1.isMoving = true;
                go2.targetX = (int)15;
                go2.targetY = (int)15;
                go2.isMoving = true;
                go3.targetX = (int)15;
                go3.targetY = (int)15;
                go3.isMoving = true;
                //go4.targetX = (int)15;
                //go4.targetY = (int)15;
                //go4.isMoving = true;
                //go5.targetX = (int)15;
                //go5.targetY = (int)15;
                //go5.isMoving = true;
                //go6.targetX = (int)15;
                //go6.targetY = (int)15;
                //go6.isMoving = true;
            }
            base.Update(gameTime);
        }
    }
}
