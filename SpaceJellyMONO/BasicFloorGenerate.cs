﻿using System;
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
            List<VertexPositionNormal> vertexPositionColors = new List<VertexPositionNormal>();
            int counter = 0;
            for(int i = 0; i < fWidth; i++)
            {
                for(int j = 0; j < fHeight; j++)
                {
                    counter++;
                    foreach(VertexPositionNormal vertex in FloorTile(i, j))
                    {
                        vertexPositionColors.Add(vertex);
                    }
                }
            }
            floorBuffer = new VertexBuffer(device, VertexPositionNormal.VertexDeclaration, vertexPositionColors.Count, BufferUsage.None);
            floorBuffer.SetData(vertexPositionColors.ToArray());
        }

        private List<VertexPositionNormal> FloorTile(int xOffset,int zOffset)
        {
            List<VertexPositionNormal> vertices = new List<VertexPositionNormal>();
            vertices.Add(new VertexPositionNormal(new Vector3(0 + xOffset, 0, 0 + zOffset), Vector3.Up));
            vertices.Add(new VertexPositionNormal(new Vector3(1 + xOffset, 0, 0 + zOffset), Vector3.Up));
            vertices.Add(new VertexPositionNormal(new Vector3(0 + xOffset, 0, 1 + zOffset), Vector3.Up));
            vertices.Add(new VertexPositionNormal(new Vector3(1 + xOffset, 0, 0 + zOffset), Vector3.Up));
            vertices.Add(new VertexPositionNormal(new Vector3(1 + xOffset, 0, 1 + zOffset), Vector3.Up));
            vertices.Add(new VertexPositionNormal(new Vector3(0 + xOffset, 0, 1 + zOffset), Vector3.Up));
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
