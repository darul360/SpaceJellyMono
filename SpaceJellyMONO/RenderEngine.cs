using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace SpaceJellyMONO
{
    public class RenderEngine : DrawableGameComponent
    {
        private Camera camera;
        private BasicFloorGenerate basicFloor;
        private SMRenderer shadowMapRenderer;

        public Scene SceneToRender
        {
            get
            {
                return ((Game1)Game).scene;
            }
        }
        
        public RenderEngine(Game1 game, Camera camera, BasicFloorGenerate basicFloor) : base(game)
        {
            this.camera = camera;
            this.basicFloor = basicFloor;
        }
        public override void Draw(GameTime gameTime)
        {
            shadowMapRenderer.RenderShadowMap(SceneToRender);
            basicFloor.Draw(shadowMapRenderer.ShadowedEffect);
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
        public override void Initialize()
        {
            //Shadow Map Renderer Setup
            shadowMapRenderer = new SMRenderer(Game, 1920, 1080);

            Vector3 lightsPosition = new Vector3(10f, 30f, 20f);
            Matrix lightsViewMatrix = Matrix.CreateLookAt(lightsPosition, new Vector3(0f, 0f, 0f), Vector3.Up);
            Matrix lightsProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 1f, 5f, 100f);

            shadowMapRenderer.LightsPosition = lightsPosition;
            shadowMapRenderer.LightsWorldMatrix = Matrix.Identity;
            shadowMapRenderer.LightsViewMatrix = lightsViewMatrix;
            shadowMapRenderer.LightsProjectionMatrix = lightsProjectionMatrix;
            shadowMapRenderer.LightsWorldViewProjectionMatrix = Matrix.Identity * lightsViewMatrix * lightsProjectionMatrix;

            shadowMapRenderer.LightsAmbientValue = 0.2f;
            shadowMapRenderer.LightsPower = 10f;

            shadowMapRenderer.SceneWorldMatrix = Matrix.Identity;
            shadowMapRenderer.SceneWorldViewProjectionMatrix = Matrix.Identity * camera.View * camera.Projection;

        }

    }
}
