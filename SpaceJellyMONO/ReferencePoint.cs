using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    class ReferencePoint
    {
        Model model;
        String modelPath;
        Vector3 pos;
        Game1 mainClass;
        private float rotationAngle, scale;

        public BoundingBox box;
        float size = 0.8f;

        public DrawBoxCollider drawBoxCollider;

        public ReferencePoint(String path, Game1 game1, Vector3 pos, float YrotationAngle, float scale)
        {
            this.modelPath = path;
            this.mainClass = game1;
            this.pos = pos;
            model = mainClass.exportContentManager().Load<Model>(path);
            this.rotationAngle = YrotationAngle;
            this.scale = scale;
            box = new BoundingBox(new Vector3(pos.X - size / 2, pos.Y, pos.Z - size / 2), new Vector3(pos.X + size / 2, pos.Y + size, pos.Z + size / 2));
            drawBoxCollider = new DrawBoxCollider(game1.GraphicsDevice,game1,"texture2.jpg");
        }

        public void draw(Camera camera)
        {
            Vector3[] verticies = box.GetCorners();

            model = mainClass.exportContentManager().Load<Model>(modelPath);
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach (BasicEffect basicEffect in modelMesh.Effects)
                {
                    basicEffect.View = camera.View;
                    basicEffect.World = Matrix.CreateScale(scale)*Matrix.CreateRotationY(rotationAngle)* Matrix.CreateTranslation(pos);
                    basicEffect.Projection = camera.Projection;
                    basicEffect.EnableDefaultLighting();
                }
                modelMesh.Draw();
                drawBoxCollider.Draw(camera, verticies);
            }

        }
    }
}
