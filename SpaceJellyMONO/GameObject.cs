﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;
using SpaceJellyMONO.FSM;
using SpaceJellyMONO.GameObjectComponents;
using System;

namespace SpaceJellyMONO
{
    public class GameObject : DrawableGameComponent
    {
        public Model model;
        public Transform transform;
        public Transform parentTransform;

        public Matrix WorldTransform => parentTransform.World() * transform.World();

        public MoveObject moveObject;
        public Collider collider;
        public Game1 mainClass;
        public Camera camera;
        private string modelPath;
        private bool isGameObjectMovable;
        public bool isObjectSelected = false;
        public float scale;
        private string gameTag;

        public FinateStateMachine finateSatemachine;

        private AnimationPlayer skinnedAnimationPlayer = null;

        public String GameTag
        {
            get { return gameTag;  }
            set { gameTag = value; }
        }

        public GameObject(string path,Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable,string tag) : base(game1)
        {
            modelPath = path;
            this.camera = game1.camera;
            mainClass = game1;
            this.isGameObjectMovable = isMovable;
            model = mainClass.exportContentManager().Load<Model>(modelPath);
            transform = new Transform(this, translation, rotationAngleX, rotationAngleY, rotationAngleZ, scale);
            this.scale = scale;
            moveObject = new MoveObject(this, isMovable, 0.005f);
            collider = new Circle(this, scale * 0.9f);
            game1.gameObjectsRepository.AddToRepo(this);
            GameTag = tag;

            SkinningData skinningDataValue = model.Tag as SkinningData;
            if (skinningDataValue != null)
                skinnedAnimationPlayer = new AnimationPlayer(skinningDataValue);
        }

        virtual public void update(float deltatime, SoundEffect effect)
        {
            moveObject.Move(deltatime, effect);
        }
        public override void Update(GameTime gameTime)
        {
            skinnedAnimationPlayer?.Update(gameTime.ElapsedGameTime, WorldTransform);
            finateSatemachine?.Update(gameTime, this);
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
            if (skinnedAnimationPlayer == null)
                throw new NullReferenceException("This GameObject does not have animation.");

            skinnedAnimationPlayer.TemporaryFrames = tempFrames;
            skinnedAnimationPlayer.ToggleRepeat = toggleRepeat;
            skinnedAnimationPlayer.StartClip(clipName);
        }
        public void SetParent(GameObject parentObject)
        {
            parentTransform = parentObject.transform;
        }

        public override void Initialize()
        {
            base.Initialize();
            finateSatemachine.Initialize();
        }

        virtual public void TakeDmg(float dmg) { }
        virtual public float GetDmg() { return 0; }
        virtual public float GetHp() { return 0; }

    }
}
