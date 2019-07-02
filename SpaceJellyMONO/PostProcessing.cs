using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    public class PostProcessing
    {
        private Texture2D screenTexture;

        private Effect postProcessingEffect;
        private SpriteBatch spriteBatch;

        public Effect PostProcessingEffect { get { return postProcessingEffect; } }

        public Texture2D ScreenTexture { set { postProcessingEffect.Parameters["ScreenTexture"].SetValue(value); } }

        public PostProcessing(Game game)
        {
            postProcessingEffect = game.Content.Load<Effect>("custom_effects/BlackAndWhite");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void RenderPostProcessing()
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
            SamplerState.LinearClamp, DepthStencilState.Default,
            RasterizerState.CullNone, postProcessingEffect);

            spriteBatch.Draw(screenTexture, new Rectangle(0, 0, 1920, 1020), Color.White);

            spriteBatch.End();
        }
    }
}
