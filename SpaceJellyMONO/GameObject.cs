using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.GameObjectComponents;
using SkinnedModel;

namespace SpaceJellyMONO
{
    public class GameObject : DrawableGameComponent
    {
        public Model model;
        public Transform transform;
        public Transform parentTransform;

        public Matrix WorldTransform { get { return parentTransform.World() * transform.World(); } }

        public MoveObject moveObject;
        public Collider collider;
        public Game1 mainClass;
        public Camera camera;
        private String modelPath;
        private bool isMovingActive;
        public bool isObjectSelected = false;
        public float scale;

        private AnimationPlayer skinnedAnimationPlayer = null;

        public GameObject(String path,Camera camera,Game1 game1, Vector3 translation, float rotationAngleX,float rotationAngleY,float rotationAngleZ,float scale,bool isMovingActive):base(game1)
        {
            this.modelPath = path;
            this.camera = camera;
            this.mainClass = game1;
            this.isMovingActive = isMovingActive;
            model = mainClass.exportContentManager().Load<Model>(modelPath);
            this.transform = new Transform(this, translation,rotationAngleX,rotationAngleY,rotationAngleZ,scale);
            this.scale = scale;
            this.moveObject = new MoveObject(this, isMovingActive,0.005f);
            this.collider = new Circle(this, scale*1.0f);
            game1.gameObjectsRepository.AddToRepo(this);

            SkinningData skinningDataValue = model.Tag as SkinningData;
            if(skinningDataValue != null)
                skinnedAnimationPlayer = new AnimationPlayer(skinningDataValue);
        }

        public void update(float deltatime)
        {
            moveObject.Move(deltatime);
        }
        public override void Update(GameTime gameTime)
        {
            skinnedAnimationPlayer?.Update(gameTime.ElapsedGameTime, WorldTransform);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            foreach (ModelMesh modelMesh in model.Meshes)
            {
                    foreach (Effect effect in modelMesh.Effects)
                    {
                        if (effect is BasicEffect)
                        {
                            BasicEffect basicEffect = (BasicEffect)effect;
                            basicEffect.World = WorldTransform;
                            basicEffect.View = camera.View;
                            basicEffect.Projection = camera.Projection;
                            basicEffect.EnableDefaultLighting();
                            basicEffect.PreferPerPixelLighting = true;
                        }
                        if (effect is SkinnedEffect)
                        {
                            SkinnedEffect skinnedEffect = (SkinnedEffect)effect;
                            skinnedEffect.SetBoneTransforms(skinnedAnimationPlayer.GetSkinTransforms());
                            skinnedEffect.View = camera.View;
                            skinnedEffect.Projection = camera.Projection;

                            skinnedEffect.EnableDefaultLighting();
                            skinnedEffect.PreferPerPixelLighting = true;
                        }
                        modelMesh.Draw();
                    }
                collider.DrawCollider();
               // if (isObjectSelected) Debug.WriteLine("I am selected" +" "+ modelPath);
               // if (!isObjectSelected) Debug.WriteLine("I am not selected"+" "+ modelPath);
            }

        }
        public void Draw(Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    if (effect is BasicEffect)
                    {
                        BasicEffect basicEffect = (BasicEffect)effect;
                        basicEffect.World = WorldTransform;
                        basicEffect.View = view;
                        basicEffect.Projection = projection;
                        basicEffect.EnableDefaultLighting();
                        basicEffect.PreferPerPixelLighting = true;
                    }
                    if (effect is SkinnedEffect)
                    {
                        SkinnedEffect skinnedEffect = (SkinnedEffect)effect;
                        skinnedEffect.SetBoneTransforms(skinnedAnimationPlayer.GetSkinTransforms());
                        skinnedEffect.View = view;
                        skinnedEffect.Projection = projection;

                        skinnedEffect.EnableDefaultLighting();
                        skinnedEffect.PreferPerPixelLighting = true;
                        skinnedEffect.SpecularPower = 300f;
                    }
                }
                mesh.Draw();
                collider.DrawCollider();
            }
        }
        public void StartAnimationClip(string clipName, int tempFrames, bool toggleRepeat)
        {
            if (this.skinnedAnimationPlayer == null)
                throw new NullReferenceException("This GameObject does not have animation.");

            skinnedAnimationPlayer.TemporaryFrames = tempFrames;
            skinnedAnimationPlayer.ToggleRepeat = toggleRepeat;
            skinnedAnimationPlayer.StartClip(clipName);
        }
        public void SetParent(GameObject parentObject)
        {
            parentTransform = parentObject.transform;
        }
    }
}
