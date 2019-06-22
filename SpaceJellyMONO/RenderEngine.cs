using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.Units;

namespace SpaceJellyMONO
{
    public class RenderEngine : DrawableGameComponent
    {
        private Camera camera;
        private SMRenderer shadowMapRenderer;
        private SpriteBatch spriteBatch;

        public Scene SceneToRender
        {
            get
            {
                return ((Game1)Game).scene;
            }
        }
        
        public RenderEngine(Game1 game, Camera camera) : base(game)
        {
            this.camera = camera;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        public override void Draw(GameTime gameTime)
        {
            //shadowMapRenderer.RenderShadowMap(SceneToRender);
            RenderScene(gameTime);
            RenderSprites();
            //RenderHUD();
            //RenderCursor();
        }

        private void RenderScene(GameTime gameTime)
        {
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (GameObject gameObject in SceneToRender?.SceneObjects.Values)
            {
                gameObject.Draw(gameTime);
            }

        }
        private void RenderSprites()
        {
            spriteBatch.Begin();
            foreach (GameObject gameObject in SceneToRender.SceneObjects.Values)
            {
                if (gameObject is Unit)
                {
                    Unit unit = gameObject as Unit;
                    spriteBatch.Draw(unit.HealthBarTexture, unit.HealthBar, Color.White);
                }
            }
            spriteBatch.End();
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
            shadowMapRenderer = new SMRenderer(Game, 4096, 3112);

            Vector3 lightsPosition = new Vector3(8f, 10f, 8f);
            Matrix lightsViewMatrix = Matrix.CreateLookAt(lightsPosition, new Vector3(10f, 0f, 10f), Vector3.Up);
            Matrix lightsProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 4f/3 , 5f, 100f);

            shadowMapRenderer.LightsPosition = lightsPosition;
            shadowMapRenderer.LightsViewMatrix = lightsViewMatrix;
            shadowMapRenderer.LightsProjectionMatrix = lightsProjectionMatrix;
            shadowMapRenderer.LightsWorldViewProjectionMatrix = Matrix.Identity * lightsViewMatrix * lightsProjectionMatrix;

            shadowMapRenderer.LightsAmbientValue = 0.2f;
            shadowMapRenderer.LightsPower = 1f;

            shadowMapRenderer.SceneWorldMatrix = Matrix.Identity;

            shadowMapRenderer.Texture = Game.Content.Load<Texture2D>("PancakeTexture");

            //Selection Renderer Setup
        }
        public override void Update(GameTime gameTime)
        {
            shadowMapRenderer.SceneWorldViewProjectionMatrix = Matrix.Identity * camera.View * camera.Projection;

            base.Update(gameTime);
        }

    }
}
