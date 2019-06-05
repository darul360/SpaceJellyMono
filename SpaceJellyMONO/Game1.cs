using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.BuildingSystem;
using SpaceJellyMONO.FSM;
using SpaceJellyMONO.FSM.States;
using SpaceJellyMONO.FSM.Trans;
using SpaceJellyMONO.GameObjectComponents;
using SpaceJellyMONO.PathFinding;
using SpaceJellyMONO.Repositories;
using SpaceJellyMONO.ResourcesGathering;
using SpaceJellyMONO.World;
using System.Diagnostics;
using System.Linq;

namespace SpaceJellyMONO
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D healthTexture;
        public Rectangle helathRectangle;
        public SpriteBatch spriteBatch2;
        public Camera camera;
        public BasicEffect effect;
        public GameObjectsRepository gameObjectsRepository;
        public PowderSourcesRepository powderSourcesRepository;
        public Scene scene;
        public FindPath findPath;
        public ClickCooridantes clickCooridantes;
        public int gridW, gridH;
        public BasicFloorGenerate basicFloorGenerate;
        public ContinueBuilding continueBuilding;
        public WriteStats writeStats;
        public SoundEffect soundEffect;
        public ResourcesStatistics resourcesStatistics;

        //sound

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1020;
            graphics.PreferredBackBufferWidth = 1920;
            IsMouseVisible = true;
            gameObjectsRepository = new GameObjectsRepository();
            powderSourcesRepository = new PowderSourcesRepository();
            gridW = 100;
            gridH = 100;
            findPath = new FindPath(gridW, gridH);
            resourcesStatistics = new ResourcesStatistics();

        }

        protected override void Initialize()
        {
            //TargetElapsedTime = new TimeSpan(TargetElapsedTime.Ticks / 2);
            //IsFixedTimeStep = false;
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 15f, 0f), new Vector3(0.8f, 0, 0), 10f, graphics);
            Components.Add(camera);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            scene = new Scene(camera, new Transform(new Vector3(0f, 0f, 0f), 0f, 0f, 0f, 1f));
            basicFloorGenerate = new BasicFloorGenerate(GraphicsDevice, gridW, gridH, spriteBatch, this);
            clickCooridantes = new ClickCooridantes(this);
            continueBuilding = new ContinueBuilding(this);
            writeStats = new WriteStats(this);
            Components.Add(continueBuilding);
            Components.Add(basicFloorGenerate);
            Components.Add(clickCooridantes);
            Components.Add(new Selector(this));
            Components.Add(new RenderEngine(this));
            Components.Add(new BaseBuildingBuilder(this));
            Components.Add(new WaterGathering(this));
            Components.Add(new UI(this));
            Components.Add(writeStats);
            Components.Add(new DrawPowderSources(this, 30));
            Components.Add(new GatherResources(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {

            soundEffect = Content.Load<SoundEffect>("jellybounce");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            State right = new MoveRigth();
            State left = new MoveLeft();
            FinateStateMachine move =
                new FinateStateMachineBuilder()
                .AddState(right)
                .AddState(left)
                .AddTransion(left, right, new TrueAfter100Frames().ChangeState)
                .AddTransion(right, left, new TrueAfter100Frames().ChangeState)
                .Build();

            FinateStateMachine aniamteZarlok = new FinateStateMachineBuilder()
                .AddState(new Animate("Take 001", 20, true))
                .Build();
            //scene.AddSceneObject("podloga", new GameObject("floor", this, new Vector3(46.3f, -0.1f, 48.5f), 1.571f, 0, 0, 1.05f, false, "floor"));

            scene.AddSceneObject("zarlok_001", new Enemy("zarlok_poprawiony", this, new Vector3(10f, 0, 10f), 0f, 3.14f, 0f, 0.05f, true, "enemy") { finateSatemachine = aniamteZarlok});

            GameObject jelly1 = new Jelly("Jelly", this, new Vector3(10f, 0f, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker")
            {
                finateSatemachine = move
            };
            Texture2D jellyTexture = Content.Load<Texture2D>("jelly_texture"); //wczytanie nowej teksury z Content Manager'a
            scene.AddSceneObject("galaretka_001", jelly1);
            scene.AddSceneObject("galaretka_002", new Jelly("jellyy", this, new Vector3(8f, 0, 8f), 0f, 0f, 0f, 0.01f, true,"worker"));

            foreach (ModelMesh mesh in scene.SceneObjects["galaretka_002"].model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    SkinnedEffect skinnedEffect = effect as SkinnedEffect;
                    if (skinnedEffect != null)
                        skinnedEffect.Texture = jellyTexture;
                }
            } //Ustawiam teksture recznie wewnatrz efektu.

            scene.AddSceneObject("galaretka_003", new Jelly("Jelly", this, new Vector3(6f, 0, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker"));
            scene.AddSceneObject("galaretka_004", new Jelly("Jelly", this, new Vector3(4f, 0, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker"));
            //GameObject gameObject2 = new GameObject("yellowChangePlatform", this, new Vector3(14f, 0, 8f), -1.57f, 0f, 0f, 0.03f, false, "platform");
            //scene.AddSceneObject("platform_001", gameObject2);
            scene.SceneObjects["zarlok_001"].StartAnimationClip("Take 001", 20, true);
            scene.SceneObjects["galaretka_002"].StartAnimationClip("jumping", 20, true);
            //scene.SceneObjects["zarlok_001"].StartAnimationClip("Take 001", 20, true);

            //init kurwa
            foreach (GameObject gameObject in scene.SceneObjects.Values)
            {
                gameObject.Initialize();
            }

        }

        public ContentManager exportContentManager()
        {
            return base.Content;
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Debug.WriteLine(1000.0f/gameTime.ElapsedGameTime.TotalMilliseconds);  FPS COUNTER
            foreach (GameObject gameObject in scene.SceneObjects.Values)
            {
                gameObject.update((float)gameTime.ElapsedGameTime.TotalMilliseconds, soundEffect);
                //if (gameObject.GameTag == "floor")
                //{
                //    gameObject.transform.XRotation += 0.0005f;
                //    Debug.WriteLine(gameObject.transform.XRotation);
                //}
                gameObject.Update(gameTime);
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }

}
