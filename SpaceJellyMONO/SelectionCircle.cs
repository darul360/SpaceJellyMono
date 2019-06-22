using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class SelectionCircle
    {
        private Game game;
        private Effect circleEffect;

        private VertexBuffer vbuffer;

        public Vector4 Color { set { circleEffect.Parameters["tintColor"].SetValue(value); } }
        public Matrix WorldMatrix { set { circleEffect.Parameters["worldMatrix"].SetValue(value); } }
        public Matrix ViewMatrix { set { circleEffect.Parameters["viewMatrix"].SetValue(value); } }
        public Matrix ProjectionMatrix { set { circleEffect.Parameters["projectionMatrix"].SetValue(value); } }

        public SelectionCircle(Vector3 position, Vector2 radius, Game game)
        {
            this.game = game;
            this.circleEffect = game.Content.Load<Effect>("custom_effects/SelectionCircle");
            circleEffect.Parameters["circleTexture"].SetValue(game.Content.Load<Texture2D>("SelectionCircleAlpha"));

            List<VertexPositionTexture> vertices = new List<VertexPositionTexture>(6);
            vertices.Add(new VertexPositionTexture(new Vector3((position.X + radius.X), 0.1f, position.Z + radius.Y), new Vector2(1f, 1f)));
            vertices.Add(new VertexPositionTexture(new Vector3((position.X - radius.X), 0.1f, position.Z + radius.Y), new Vector2(0f, 1f)));
            vertices.Add(new VertexPositionTexture(new Vector3((position.X + radius.X), 0.1f, position.Z - radius.Y), new Vector2(1f, 0f)));
            vertices.Add(new VertexPositionTexture(new Vector3((position.X - radius.X), 0.1f, position.Z + radius.Y), new Vector2(0f, 1f)));
            vertices.Add(new VertexPositionTexture(new Vector3((position.X - radius.X), 0.1f, position.Z - radius.Y), new Vector2(0f, 0f)));
            vertices.Add(new VertexPositionTexture(new Vector3((position.X + radius.X), 0.1f, position.Z - radius.Y), new Vector2(1f, 0f)));
            vbuffer = new VertexBuffer(game.GraphicsDevice, VertexPositionTexture.VertexDeclaration, vertices.Count, BufferUsage.WriteOnly);
            vbuffer.SetData<VertexPositionTexture>(vertices.ToArray());
        }
        public void Draw()
        {
            game.GraphicsDevice.SetVertexBuffer(vbuffer);
            game.GraphicsDevice.BlendState = BlendState.Additive;

            BasicEffect basicEffect = new BasicEffect(game.GraphicsDevice);


            foreach (EffectPass pass in circleEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                game.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }

            game.GraphicsDevice.BlendState = BlendState.Opaque;
        }
    }
}
