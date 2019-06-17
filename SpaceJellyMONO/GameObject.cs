using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;
using SpaceJellyMONO.FSM;
using SpaceJellyMONO.GameObjectComponents;
using System;
using System.Diagnostics;

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
        public bool buildingFlag = false;
        public string modelPath;
        public bool isGameObjectMovable;
        public bool isObjectSelected = false;
        public float scale;
        private string gameTag;
        public string sceneID;
        public bool isMoving = false;
        public int targetX = 0, targetY = 0;
        public bool isFighting = false;

        public FinateStateMachine finateSatemachine;

        protected AnimationPlayer skinnedAnimationPlayer = null;

        public String GameTag
        {
            get { return gameTag;  }
            set { gameTag = value; }
        }


        public GameObject(string path,Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable,string tag,float colSize) : base(game1)
        {
            modelPath = path;
            this.camera = game1.camera;
            mainClass = game1;
            this.isGameObjectMovable = isMovable;
            model = mainClass.exportContentManager().Load<Model>(modelPath);
            transform = new Transform(this, translation, rotationAngleX, rotationAngleY, rotationAngleZ, scale);
            this.scale = scale;
            moveObject = new MoveObject(this, isMovable, 0.005f);
            collider = new Circle(this, colSize);
            GameTag = tag;
            if (GameTag != "bluePowder" && GameTag != "yellowPowder")
                game1.gameObjectsRepository.AddToRepo(this);
            else
                game1.powderSourcesRepository.AddToRepo(this);

            SkinningData skinningDataValue = model.Tag as SkinningData;
            if (skinningDataValue != null)
                skinnedAnimationPlayer = new AnimationPlayer(skinningDataValue);

            
        }

        virtual public void update(float deltatime, SoundEffect effect)
        {
            moveObject.CheckCollisions(deltatime);
            if (isMoving && isGameObjectMovable)
            {
                
                moveObject.Move(deltatime, effect, targetX, targetY);
            }
        }
        public override void Update(GameTime gameTime)
        {
            skinnedAnimationPlayer?.Update(gameTime.ElapsedGameTime, WorldTransform);
            finateSatemachine?.Update(gameTime, this);
            startWalking();
            stopWalking();
            startFighting();
            stopFighting();
            base.Update(gameTime);


        }

        public void startWalking() // idzie
        {
            if (gameTag == "worker" && isMoving)
            {
                if (skinnedAnimationPlayer.clipName != "1")
                mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("1", 20, true);
            }
            if (gameTag == "warrior" && isMoving)
            {
                if (skinnedAnimationPlayer.clipName != "1")
                    mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("1", 20, true);
            }
        }
        public void stopWalking() //stoi
        {
            if (gameTag == "worker" && !isMoving && !isFighting)
            {
                if (skinnedAnimationPlayer.clipName != "2")
                    mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("2", 20, true);
            }
            if (gameTag == "warrior" && !isMoving && !isFighting)
            {
                if (skinnedAnimationPlayer.clipName != "2")
                    mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("2", 20, true);
            }
        }

        public void startFighting() //walczy
        {
            if (gameTag == "worker" && isFighting)
            {
                Debug.WriteLine(isFighting);
                if (skinnedAnimationPlayer.clipName != "4")
                    mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("4", 20, true);
            }
            if (gameTag == "warrior" && isFighting)
            {
                if (skinnedAnimationPlayer.clipName != "4")
                    mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("4", 20, true);
            }
        }
        public void stopFighting()
        {
            if (gameTag == "worker" && isFighting && isMoving)
            {
                isFighting = false;
            }
            if (gameTag == "warrior" && isFighting && isMoving)
            {
                isFighting = false;
            }
        }


        public void Reload()
        {
            model = mainClass.exportContentManager().Load<Model>(modelPath);
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

                        basicEffect.SpecularPower = 200f;
                        basicEffect.LightingEnabled = true; // turn on the lighting subsystem.
                        basicEffect.DirectionalLight0.Direction = new Vector3(1, 0, 1);  // coming along the x-axis
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
                        skinnedEffect.SpecularPower = 100f;
                        skinnedEffect.DirectionalLight0.Direction = new Vector3(1, 0, 1);  // coming along the x-axis
                    }
                }
                collider.DrawCollider();
                modelMesh.Draw();
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
            if (gameTag == "worker")
            {
                mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("2", 20, true);
            }
            if (gameTag == "warrior")
            {
                mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("2", 20, true);
            }
            if (gameTag == "enemy")
            {
                mainClass.scene.SceneObjects[mainClass.scene.FindKeyOfObject(this)].StartAnimationClip("Take 001", 20, true);
            }
            Console.WriteLine("go init");
            finateSatemachine?.Initialize(this);
        }
		
		virtual public void TakeDmg(float dmg) { }
        virtual public float GetDmg() { return 0; }
        virtual public float GetHp() { return 0; }
    }
}

