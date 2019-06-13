using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class BasicFloorGenerate
    {
        private int fWidth, fHeight;
        private VertexBuffer floorBuffer;
        private GraphicsDevice device;

        public BasicFloorGenerate(GraphicsDevice device,int width,int height)
        {
            this.device = device;
            this.fWidth = width;
            this.fHeight = height;
            BuildFloorBuffer();
        }

        public void BuildFloorBuffer()
        {
            List<VertexPositionNormalTexture> vertexPositionNormalTexture = new List<VertexPositionNormalTexture>();
            for(int i = 0; i < fWidth; i++)
            {
                for(int j = 0; j < fHeight; j++)
                {
                    foreach(VertexPositionNormalTexture vertex in FloorTile(i, j, fWidth, fHeight))
                    {
                        vertexPositionNormalTexture.Add(vertex);
                    }
                }
            }
            floorBuffer = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration, vertexPositionNormalTexture.Count, BufferUsage.WriteOnly);
            floorBuffer.SetData(vertexPositionNormalTexture.ToArray());
        }

        private List<VertexPositionNormalTexture> FloorTile(int xOffset,int zOffset, int width, int height)
        {
            List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
            vertices.Add(new VertexPositionNormalTexture(new Vector3(0 + xOffset, 0f, 0 + zOffset), Vector3.Up, new Vector2((0 + xOffset) * 1f/ width, (0 + zOffset) * 1f / height)));
            vertices.Add(new VertexPositionNormalTexture(new Vector3(1 + xOffset, 0f, 0 + zOffset), Vector3.Up, new Vector2((1 + xOffset) * 1f / width, (0 + zOffset) * 1f/ height)));
            vertices.Add(new VertexPositionNormalTexture(new Vector3(0 + xOffset, 0f, 1 + zOffset), Vector3.Up, new Vector2((0 + xOffset) * 1f / width, (1 + zOffset) * 1f/ height)));
            vertices.Add(new VertexPositionNormalTexture(new Vector3(1 + xOffset, 0f, 0 + zOffset), Vector3.Up, new Vector2((1 + xOffset) * 1f / width, (0 + zOffset) * 1f/ height)));
            vertices.Add(new VertexPositionNormalTexture(new Vector3(1 + xOffset, 0f, 1 + zOffset), Vector3.Up, new Vector2((1 + xOffset) * 1f / width, (1 + zOffset) * 1f/ height)));
            vertices.Add(new VertexPositionNormalTexture(new Vector3(0 + xOffset, 0f, 1 + zOffset), Vector3.Up, new Vector2((0 + xOffset) * 1f / width, (1 + zOffset) * 1f/ height)));
            return vertices;
        }

        public void Draw(Camera camera,BasicEffect basicEffect)
        {
            basicEffect.VertexColorEnabled = true;
            basicEffect.View = camera.View;
            basicEffect.Projection = camera.Projection;
            basicEffect.World = Matrix.Identity;

            foreach(EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(floorBuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, floorBuffer.VertexCount / 3);

            }
        }
        public void Draw(Effect effect)
        {
            foreach (EffectPass pass in effect?.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.SetVertexBuffer(floorBuffer);
                device.DrawPrimitives(PrimitiveType.TriangleList, 0, floorBuffer.VertexCount / 3);

            }
        }

    }
}
