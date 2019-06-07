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

        private Game game;

        private FileStream fileStream; //for debugging

        public Effect ShadowMapEffect { get { return shadowMapEffect; } }
        public Effect ShadowedEffect { get { return shadowedEffect; } }

        public SMRenderer(Game game, int shadowMapWidth, int shadowMapHeight)
        {
            this.game = game;

            shadowMapEffect = game.Content.Load<Effect>("custom_effects/ShadowMap");
            shadowedEffect = game.Content.Load<Effect>("custom_effects/Shadowed");

            shadowMap = new RenderTarget2D(game.GraphicsDevice, shadowMapWidth, shadowMapHeight);

            fileStream = new FileStream("test.png", FileMode.Create); //for debugging
        }

        //Light parameters
        public Matrix LightsWorldMatrix { set{ shadowMapEffect.Parameters["worldMatrix"].SetValue(value);}}
        public Matrix LightsViewMatrix {set {shadowMapEffect.Parameters["viewMatrix"].SetValue(value);}}
        public Matrix LightsProjectionMatrix {  set { shadowMapEffect.Parameters["projectionMatrix"].SetValue(value); } }
        public Matrix LightsWorldViewProjectionMatrix { set { shadowMapEffect.Parameters["projectionMatrix"].SetValue(value); } }
        public Vector3 LightsPosition { set { shadowedEffect.Parameters["xLightPos"].SetValue(value); } }
        public float LightsPower { set { shadowedEffect.Parameters["xLightPower"].SetValue(value); } }
        public float LightsAmbientValue { set { shadowedEffect.Parameters["xAmbient"].SetValue(value); } }

        //Scene parameters
        public Matrix SceneWorldViewProjectionMatrix { set { shadowedEffect.Parameters["xWorldViewProjection"].SetValue(value); } }
        public Matrix SceneWorldMatrix { set { shadowedEffect.Parameters["xWorld"].SetValue(value); } }

        public void RenderShadowMap(Scene scene)
        {
            game.GraphicsDevice.SetRenderTarget(shadowMap);

            foreach (GameObject gameObject in scene?.SceneObjects.Values)
            {
                gameObject.Draw(shadowMapEffect);
            }

            shadowMap.SaveAsPng(fileStream, 800, 800); //for debugging

            shadowedEffect.Parameters["xShadowMap"].SetValue((Texture2D)shadowMap);
            game.GraphicsDevice.SetRenderTarget(null);
        }


    }
}
