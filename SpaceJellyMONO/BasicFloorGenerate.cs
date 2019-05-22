using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO
{
    public class BasicFloorGenerate
    {
        private int fWidth, fHeight;
        private GraphicsDevice device;

        private bool[,] coordinatesOfPoints;
        private CylinderPrimitive [,] cylinders;

        public BasicFloorGenerate(GraphicsDevice device,int width,int height, SpriteBatch spriteBatch)
        {
            this.device = device;
            this.fWidth = width;
            this.fHeight = height;
            coordinatesOfPoints = new bool[fWidth,fHeight];
            cylinders = new CylinderPrimitive[fWidth,fHeight];
            for (int i = 0; i < fHeight; i++)
            {
                for(int j = 0; j < fWidth; j++)
                {
                    coordinatesOfPoints[i,j] = false;
                }
            }

            BuildFloorBuffer();
        }

        public void BuildFloorBuffer()
        {

            for (int i = 0; i < fHeight; i++)
            {
                for (int j = 0; j < fWidth; j++)
                {
                    cylinders[i,j] = new CylinderPrimitive(device, 0.5f, 0.2f, 20);
                }
            }
        }

        public Matrix world(Vector3 vector)
        {
            return Matrix.CreateScale(1.0f) * Matrix.CreateTranslation(vector);
        }

        public void Draw(Camera camera,BasicEffect basicEffect)
        {
            for (int i = 0; i < fHeight; i++)
            {
                for (int j = 0; j < fWidth; j++)
                {
                    //Matrix matrix = Matrix.CreateWorld(world(new Vector3(i,0,j)).Translation, Vector3.Forward, Vector3.Up);
                    //cylinders[i, j].Draw(matrix, camera.View, camera.Projection, new Color(255, 0, 0));
                }
            }
            
        }

    }
}
