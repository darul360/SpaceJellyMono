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
        private Game game;

        private Effect postProcessingEffect;
        private SpriteBatch spriteBatch;

        public PostProcessing(Game game)
        {
            postProcessingEffect = game.Content.Load<Effect>("custom_effects/BlackAndWhite");
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.game = game;
        }

        public void RenderPostProcessing(Texture2D screenTexture)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            postProcessingEffect.CurrentTechnique.Passes[0].Apply();
            spriteBatch.Draw(screenTexture, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
    }
}
