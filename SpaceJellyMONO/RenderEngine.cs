using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class RenderEngine : DrawableGameComponent
    {
        private Camera camera;
        private BasicFloorGenerate basicFloor;

        //Shadow Mapping
        private RenderTarget2D customTarget;
        private Effect shadowMapEffect;

        public Scene SceneToRender
        {
            get
            {
                return ((Game1)Game).scene;
            }
        }
        
        public RenderEngine(Game1 game, Camera camera, BasicFloorGenerate basicFloor, int shadowMapWidth, int shadowMapHeight, Effect shadowMapEffect) : base(game)
        {
            this.camera = camera;
            this.basicFloor = basicFloor;

            customTarget = new RenderTarget2D(Game.GraphicsDevice, shadowMapWidth, shadowMapHeight);
            this.shadowMapEffect = shadowMapEffect;
        }
        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.SetRenderTarget(customTarget);
            RenderShadowMap();
            Game.GraphicsDevice.SetRenderTarget(null);
            basicFloor.Draw(SceneToRender.RootTransform.World(), camera.View, camera.Projection, customTarget);
            RenderScene();
            //RenderHUD();
            //RenderCursor();
        }
        private void RenderScene()
        {
                foreach(GameObject gameObject in SceneToRender?.SceneObjects.Values)
                {
                    gameObject.Draw(camera.View, camera.Projection);
                }

        }
        private void RenderHUD()
        {
        }
        private void RenderCursor()
        {

        }
        private void RenderShadowMap()
        {
                foreach (GameObject gameObject in SceneToRender?.SceneObjects.Values)
                {
                    gameObject.Draw(camera.View, camera.Projection, shadowMapEffect);
                }
        }

    }
}
