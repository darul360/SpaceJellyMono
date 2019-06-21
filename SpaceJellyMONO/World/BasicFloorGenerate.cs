using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.Collision;
using SpaceJellyMONO.GameObjectComponents;
using SpaceJellyMONO.Repositories;
using System.Linq;

namespace SpaceJellyMONO
{
    public class BasicFloorGenerate: DrawableGameComponent
    {
        private int fWidth, fHeight;
        private GraphicsDevice device;
        private VertexBuffer floorBuffer;
        private Color[] floorColors = new Color[2] { Color.BlueViolet, Color.Brown };
        private bool[,] coordinatesOfPoints;
        private CylinderPrimitive[,] cylinders;
        private Game1 game1;
        BasicEffect basicEffect;

        public BasicFloorGenerate(GraphicsDevice device, int width, int height, SpriteBatch spriteBatch,Game1 game1):base(game1)
        {
            this.game1 = game1;
            this.device = device;
            this.fWidth = width;
            this.fHeight = height;
            basicEffect = new BasicEffect(game1.GraphicsDevice);
            PathCollidersRepository.cylinders = new CirclePath[fWidth, fHeight];
            coordinatesOfPoints = new bool[fWidth, fHeight];
            cylinders = new CylinderPrimitive[fWidth, fHeight];
            for (int i = 0; i < fHeight; i++)
            {
                for (int j = 0; j < fWidth; j++)
                {
                    coordinatesOfPoints[i, j] = false;
                }
            }
            BuildFloorBuffer();
        }

        public void BuildFloorBuffer()
        {
            List<VertexPositionNormalTexture> vertexPositionNormalTexture = new List<VertexPositionNormalTexture>();
            for(int i = 0; i < fWidth; i++)
            {
                for(int j = 0; j < fHeight; j++)
                {
					PathCollidersRepository.cylinders[i, j] = new CirclePath(null, 0.5f, new Vector3(i, 0, j));
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

        public Matrix world(Vector3 vector)
        {
            return Matrix.CreateScale(1.0f) * Matrix.CreateTranslation(vector);
        }

        public void updateGrid()
        {
            for (int i = 0; i < fHeight; i++)
            {
                for (int j = 0; j < fWidth; j++)
                {
                    foreach (GameObject go in game1.gameObjectsRepository.getRepo())
                    {
                        if (PathCollidersRepository.cylinders[i, j].Intersect(go.collider) 
                            && go.GameTag != "baseBuilding" && go.GameTag != "firstPartOfBuilding"
                            && go.GameTag != "bluePowder" && go.GameTag != "yellowPowder"
                            && go.GameTag != "platform" && go.GameTag != "baza" && go.GameTag != "warrior"
                            && go.GameTag != "worker" && go.GameTag != "enemy" && go.GameTag != "spawn")
                        {
                            game1.findPath.setBlockCell(i, j);
                        }
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

                
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
