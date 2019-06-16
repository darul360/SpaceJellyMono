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
            List<VertexPositionColor> vertexPositionColors = new List<VertexPositionColor>();
            int counter = 0;

            for (int i = 0; i < fHeight; i++)
            {
                for (int j = 0; j < fWidth; j++)
                {
                    PathCollidersRepository.cylinders[i, j] = new CirclePath(null, 0.5f, new Vector3(i, 0, j));
                    counter++;
                    foreach (VertexPositionColor vertex in FloorTile(i, j, floorColors[counter % 2]))
                    {
                        vertexPositionColors.Add(vertex);
                    }
                    //cylinders[i, j] = new CylinderPrimitive(device, 0.5f, 0.2f, 20);
                }
            }
            floorBuffer = new VertexBuffer(device, VertexPositionColor.VertexDeclaration, vertexPositionColors.Count, BufferUsage.None);
            floorBuffer.SetData<VertexPositionColor>(vertexPositionColors.ToArray());
        }

        private List<VertexPositionColor> FloorTile(int xOffset, int zOffset, Color tileColor)
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
                            && go.GameTag != "platform" && go.GameTag != "baza" && go.GameTag != "warrior")
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
    


        public override void Draw(GameTime gameTime)
        {
            //base.Draw(gameTime);
            //basicEffect.VertexColorEnabled = true;
            //basicEffect.View = game1.camera.View;
            //basicEffect.Projection = game1.camera.Projection;
            //basicEffect.World = Matrix.Identity;


            //foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    device.SetVertexBuffer(floorBuffer);
            //    device.DrawPrimitives(PrimitiveType.TriangleList, 0, floorBuffer.VertexCount / 3);
            //}

            //for (int i = 0; i < fHeight; i++)
            //{
            //    for (int j = 0; j < fWidth; j++)
            //    {
            //        //cylinders[i, j].Draw(matrix, camera.View, camera.Projection, new Color(255, 0, 0));
            //    }
            //}

            

        }
    }
    
}
