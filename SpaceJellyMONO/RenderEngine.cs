using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceJellyMONO
{
    public class RenderEngine : DrawableGameComponent
    {
        public Scene SceneToRender
        {
            get
            {
                return ((Game1)Game).scene;
            }
        }
        
        public RenderEngine(Game1 game) : base(game)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            RenderScene(gameTime);
            //RenderHUD();
            //RenderCursor();
        }

        private void RenderScene(GameTime gameTime)
        {
            if(SceneToRender != null)
            {
                foreach(GameObject gameObject in SceneToRender.SceneObjects.Values)
                {
                    gameObject.Draw(gameTime);
                }
            }

        }
        private void RenderHUD()
        {
        }
        private void RenderCursor()
        {

        }

    }
}
