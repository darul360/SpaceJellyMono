using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class ModelLoader
    {
        private Model model;
        private String modelPath;
        private Camera camera;
        private Game1 mainClass;
        public ModelLoader(String path,Camera camera,Game1 game1)
        {
            this.modelPath = path;
            this.camera = camera;
            this.mainClass = game1;
        }
       public void draw()
        {
            model = mainClass.exportContentManager().Load<Model>(modelPath);
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach(BasicEffect basicEffect in modelMesh.Effects)
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
