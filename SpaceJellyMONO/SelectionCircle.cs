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

        private VertexBuffer vbuffer;

        private Vector4 tintColor;
        public Vector4 TintColor { get { return tintColor; } }

        public SelectionCircle(Vector3 position, Vector2 radius, Vector4 tintColor, Game game)
        {
            this.game = game;
            this.tintColor = tintColor;

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
        public void Draw(Effect effect)
        {
            game.GraphicsDevice.SetVertexBuffer(vbuffer);
            game.GraphicsDevice.BlendState = BlendState.Additive;

            BasicEffect basicEffect = new BasicEffect(game.GraphicsDevice);


            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                game.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }

            game.GraphicsDevice.BlendState = BlendState.Opaque;
        }
    }
}
