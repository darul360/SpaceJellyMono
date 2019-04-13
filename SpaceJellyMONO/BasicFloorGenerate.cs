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
        private Color[] floorColors = new Color[2] { Color.BlueViolet, Color.Brown };

        public BasicFloorGenerate(GraphicsDevice device,int width,int height)
        {
            this.device = device;
            this.fWidth = width;
            this.fHeight = height;
            BuildFloorBuffer();
        }

        public void BuildFloorBuffer()
        {
            List<VertexPositionColor> vertexPositionColors = new List<VertexPositionColor>();
            int counter = 0;
            for(int i = 0; i < fWidth; i++)
            {
                for(int j = 0; j < fHeight; j++)
                {
                    counter++;
                    foreach(VertexPositionColor vertex in FloorTile(i, j, floorColors[counter % 2]))
                    {
                        vertexPositionColors.Add(vertex);
                    }
                }
            }
            floorBuffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, vertexPositionColors.Count, BufferUsage.None);
            floorBuffer.SetData<VertexPositionColor>(vertexPositionColors.ToArray());
        }

        private List<VertexPositionColor> FloorTile(int xOffset,int zOffset,Color tileColor)
        {
            List<VertexPositionColor> vertices = new List<VertexPositionColor>();
            vertices.Add(new VertexPositionColor(new Vector3(0 + xOffset, 0, 0 + zOffset), tileColor));
            vertices.Add(new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor));
            vertices.Add(new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor));
            vertices.Add(new VertexPositionColor(new Vector3(1 + xOffset, 0, 0 + zOffset), tileColor));
            vertices.Add(new VertexPositionColor(new Vector3(1 + xOffset, 0, 1 + zOffset), tileColor));
            vertices.Add(new VertexPositionColor(new Vector3(0 + xOffset, 0, 1 + zOffset), tileColor));
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

    }
}
