using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

        public ReferencePoint(String path, Game1 game1, float scale, Vector3 pos)
        {
            this.modelPath = path;
            this.mainClass = game1;
            this.pos = pos;
            //box = new BoundingBox(new Vector3(translation.X - size / 2, translation.Y, translation.Z - size / 2),
        }

        public void draw(Camera camera)
        {

            model = mainClass.exportContentManager().Load<Model>(modelPath);
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach (BasicEffect basicEffect in modelMesh.Effects)
                {
                    basicEffect.View = camera.View;
                    basicEffect.World = Matrix.Identity;
                    basicEffect.Projection = camera.Projection;
                    basicEffect.EnableDefaultLighting();
                }
                modelMesh.Draw();
            }

        }
    }
}
