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
    class SMRenderer
    {
        private Effect shadowMapEffect;
        private Effect shadowedEffect;
        private RenderTarget2D shadowMap;
        private Stream stream;

        private Game game;

        public Effect ShadowMapEffect { get { return shadowMapEffect; } }
        public Effect ShadowedEffect { get { return shadowedEffect; } }

        private Matrix lightsViewMatrix = Matrix.Identity;
        private Matrix lightsProjectionMatrix = Matrix.Identity;

        public SMRenderer(Game game, int shadowMapWidth, int shadowMapHeight)
        {
            this.game = game;

            shadowMapEffect = game.Content.Load<Effect>("custom_effects/ShadowMap");
            shadowedEffect = game.Content.Load<Effect>("custom_effects/Shadowed");

            shadowMap = new RenderTarget2D(game.GraphicsDevice, shadowMapWidth, shadowMapHeight);

            stream = new FileStream("test.png", FileMode.OpenOrCreate);
        }

        //Light parameters
        public Matrix LightsViewMatrix { get { return lightsViewMatrix; } set { lightsViewMatrix = value; shadowMapEffect.Parameters["viewMatrix"].SetValue(value); shadowedEffect.Parameters["lightsViewMatrix"].SetValue(value); } }
        public Matrix LightsProjectionMatrix { get { return lightsProjectionMatrix; } set { lightsProjectionMatrix = value; shadowMapEffect.Parameters["projectionMatrix"].SetValue(value); shadowedEffect.Parameters["lightsProjectionMatrix"].SetValue(value); } } 
        public Vector3 LightsPosition { set { shadowedEffect.Parameters["xLightPos"].SetValue(value); } }
        public float LightsPower { set { shadowedEffect.Parameters["xLightPower"].SetValue(value); } }
        public float LightsAmbientValue { set { shadowedEffect.Parameters["xAmbient"].SetValue(value); } }
        public Texture2D Texture { set { shadowedEffect.Parameters["xTexture"].SetValue(value); } }

        //Scene parameters
        public Matrix SMWorldMatrix { set { shadowMapEffect.Parameters["worldMatrix"].SetValue(value); } }
        public Matrix WorldMatrix { set { shadowedEffect.Parameters["worldMatrix"].SetValue(value); } }
        public Matrix CameraViewMatrix { set { shadowedEffect.Parameters["cameraViewMatrix"].SetValue(value); } }
        public Matrix CameraProjectionMatrix { set { shadowedEffect.Parameters["cameraProjectionMatrix"].SetValue(value); } }

        public void RenderShadowMap(Scene scene)
        {
            game.GraphicsDevice.SetRenderTarget(shadowMap);
            game.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            foreach (GameObject gameObject in scene?.SceneObjects.Values)
            {
                shadowMapEffect.Parameters["worldMatrix"].SetValue(gameObject.WorldTransform);
                gameObject.Draw(shadowMapEffect);
            }

            shadowedEffect.Parameters["xShadowMap"].SetValue(shadowMap);
            //shadowMap.SaveAsPng(stream, shadowMap.Width, shadowMap.Height);
            game.GraphicsDevice.SetRenderTarget(null);
            game.GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
        }


    }
}
