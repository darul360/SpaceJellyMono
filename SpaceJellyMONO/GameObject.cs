using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.GameObjectComponents;

namespace SpaceJellyMONO
{
    public class GameObject : GameComponent
    {
        public Model model;
        public Transform transform;
        public MoveObject moveObject;
        public Collider collider;
        public Game1 mainClass;
        public Camera camera;
        private String modelPath;
        private bool isMovingActive;

        public GameObject(String path,Camera camera,Game1 game1, Vector3 translation, float rotationAngleX,float rotationAngleY,float rotationAngleZ,float scale,bool isMovingActive):base(game1)
        {
            this.modelPath = path;
            this.camera = camera;
            this.mainClass = game1;
            this.isMovingActive = isMovingActive;
            model = mainClass.exportContentManager().Load<Model>(modelPath);

            this.transform = new Transform(this, translation,rotationAngleX,rotationAngleY,rotationAngleZ,scale);
            this.moveObject = new MoveObject(this, isMovingActive);
            this.collider = new Circle(this, 100.8f);

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
                collider.DrawCollider();
            }

        }
    }
}
