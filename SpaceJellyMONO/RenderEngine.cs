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
            RenderScene();
            //RenderHUD();
            //RenderCursor();
        }
        private void RenderScene()
        {
            if(SceneToRender != null)
            {
                foreach(ModelLoader gameObject in SceneToRender.SceneObjects.Values)
                {
                    gameObject.Draw(SceneToRender.Camera.View, SceneToRender.Camera.Projection);
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
