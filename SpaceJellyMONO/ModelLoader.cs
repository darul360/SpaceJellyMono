using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO
{
    public class ModelLoader : GameComponent
    {
        public Model model;
        public Transform transform;
        public Transform parentTransform;

        public Matrix WorldTransform { get { return transform.World() * parentTransform.World(); } }

        public MoveObject moveObject;
        public Collider collider;
        public Game1 mainClass;
        public Camera camera;
        private String modelPath;
        private bool isMovingActive;

        public ModelLoader(String path,Camera camera,Game1 game1, Vector3 translation, float rotationAngleX,float rotationAngleY,float rotationAngleZ,float scale,bool isMovingActive):base(game1)
        {
            this.modelPath = path;
            this.camera = camera;
            this.mainClass = game1;
            this.isMovingActive = isMovingActive;
            model = mainClass.exportContentManager().Load<Model>(modelPath);

            this.transform = new Transform(translation,rotationAngleX,rotationAngleY,rotationAngleZ,scale);
            this.moveObject = new MoveObject(this, isMovingActive);
            this.collider = new Collider(this, 0.8f);

            game1.gameObjectsRepository.AddToRepo(this);
        }


        public void update()
        {
            moveObject.Move();
        }
                                  
        public void draw()
        {

            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach(BasicEffect basicEffect in modelMesh.Effects)
                {
                    basicEffect.View = camera.View;
                    basicEffect.World = transform.World();  
                    basicEffect.Projection = camera.Projection;
                    basicEffect.EnableDefaultLighting();
                }
                modelMesh.Draw();
                collider.DrawBoxCollider();
            }

        }
        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh modelMesh in model.Meshes)
            {
                foreach (BasicEffect basicEffect in modelMesh.Effects)
                {
                    basicEffect.View = view;
                    basicEffect.World = WorldTransform;
                    basicEffect.Projection = projection;
                    basicEffect.EnableDefaultLighting();
                }
                modelMesh.Draw();
                collider.DrawBoxCollider();
            }
        }
    }
}
