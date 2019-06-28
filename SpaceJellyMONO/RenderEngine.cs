using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.Units;
using SpaceJellyMONO.World;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO
{
    public class RenderEngine : DrawableGameComponent
    {
        private Camera camera;
        public Scene SceneToRender { get => ((Game1)Game).scene; }

        //Scene renderers
        private SMRenderer shadowMapRenderer;

        //Sprite renderers
        private SpriteBatch spriteBatch;
        private FloatingTextRenderer floatingTextRenderer;

        public FloatingTextRenderer FloatingTextRenderer { get => floatingTextRenderer; }

        //HUD renderers
        public WriteStats writeStats;
        private ShowInfoAboutBuilding showInfoAbout;
        
        public RenderEngine(Game1 game, Camera camera) : base(game)
        {
            this.camera = camera;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            shadowMapRenderer = new SMRenderer(Game, 4096, 3112);
            writeStats = new WriteStats(game);
            showInfoAbout = new ShowInfoAboutBuilding(game);
            floatingTextRenderer = new FloatingTextRenderer(game, spriteBatch, camera);
        }
        public override void Draw(GameTime gameTime)
        {
            Vector3 lightsPosition = camera.Position;
            lightsPosition.Y -= 5f;
            Vector3 lightsLookAt = camera.CameraLookAt;
            lightsLookAt.Y -= 5f;
            shadowMapRenderer.LightsPosition = camera.Position;
            shadowMapRenderer.LightsViewMatrix = Matrix.CreateLookAt(lightsPosition, lightsLookAt, Vector3.Up);

            shadowMapRenderer.RenderShadowMap(SceneToRender);

            if (SceneToRender.Floor != null)
            {
                shadowMapRenderer.LightsWorldMatrix = SceneToRender.Floor.WorldTransform;
                shadowMapRenderer.WorldMatrix = SceneToRender.Floor.WorldTransform;
                shadowMapRenderer.CameraViewMatrix = camera.View;
                shadowMapRenderer.CameraProjectionMatrix = camera.Projection;
                SceneToRender.Floor.Draw(shadowMapRenderer.ShadowedEffect);
                SceneToRender.Floor.IsVisible = false;
            }

            RenderScene(gameTime);
            RenderSprites(gameTime);
            RenderHUD(gameTime);
            //RenderCursor();
        }

        private void RenderScene(GameTime gameTime)
        {
            Game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            foreach (GameObject gameObject in SceneToRender?.SceneObjects.Values)
            {
                if(gameObject.IsVisible)
                    gameObject.Draw(gameTime);
            }

        }
        private void RenderSprites(GameTime gameTime)
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

            floatingTextRenderer.Draw(gameTime);
            spriteBatch.End();
        }
        private void RenderHUD(GameTime gameTime)
        {
            writeStats.Draw(gameTime);
            showInfoAbout.Draw(gameTime);

        }
        private void RenderCursor()
        {

        }
        public override void Initialize()
        {
            //Shadow Map Renderer Setup
            Matrix lightsProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 4f/3 , 5f, 100f);

            shadowMapRenderer.LightsProjectionMatrix = lightsProjectionMatrix;

            shadowMapRenderer.LightsAmbientValue = 0.2f;
            shadowMapRenderer.LightsPower = 1f;

            shadowMapRenderer.Texture = Game.Content.Load<Texture2D>("mountains/mountains_DefaultMaterial_BaseColor");

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            floatingTextRenderer.Update(gameTime);
        }

    }
}
