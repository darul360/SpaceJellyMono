using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpaceJellyMONO.BuildingSystem;
using SpaceJellyMONO.FSM;
using SpaceJellyMONO.FSM.States;
using SpaceJellyMONO.FSM.Trans;
using SpaceJellyMONO.GameObjectComponents;
using SpaceJellyMONO.PathFinding;
using SpaceJellyMONO.Repositories;
using SpaceJellyMONO.ResourcesGathering;
using SpaceJellyMONO.UnitsFolder;
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
        public SelectedObjectsRepository selectedObjectsRepository;
        public Scene scene;
        public FindPath findPath;
        public ClickCooridantes clickCooridantes;
        public int gridW, gridH;
        public BasicFloorGenerate basicFloorGenerate;
        public ContinueBuilding continueBuilding;
        public WriteStats writeStats;
        public SoundEffect soundEffect;
        public ResourcesStatistics resourcesStatistics;
        GameObject flr,platform;
        Vector3 temporaryRot, temporaryPos;
        bool switcher = false;
        KeyboardState lastKeyboardState = new KeyboardState();
        VideoPlayer player,player2;
        Video video,video2;
        public FloatingText floatingText;
        public FinateStateMachine aniamteZarlok;
        public FinateStateMachine animateJelly;
        public GameObject spawn;
        public MovingController movingController;
        //sound

        enum GameState
        {
            MainMenu,
            Tutorial,
            Playing,
        }
        GameState currentGameState = GameState.MainMenu;


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
            selectedObjectsRepository = new SelectedObjectsRepository();
            gridW = 100;
            gridH = 100;
            findPath = new FindPath(gridW, gridH);
            resourcesStatistics = new ResourcesStatistics(this);

        }

        protected override void Initialize()
        {
            //TargetElapsedTime = new TimeSpan(TargetElapsedTime.Ticks / 2);
            //IsFixedTimeStep = false;
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 25f, 0f), new Vector3(1.2f, 0, 0), 10f, graphics);
            Components.Add(camera);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            scene = new Scene(new Transform(new Vector3(0f, 0f, 0f), 0f, 0f, 0f, 1f));
            Components.Add(new RenderEngine(this, camera));
            Components.Add(new Selector(this));
            basicFloorGenerate = new BasicFloorGenerate(GraphicsDevice, gridW, gridH, spriteBatch, this);
            clickCooridantes = new ClickCooridantes(this);
            continueBuilding = new ContinueBuilding(this);
            //writeStats = new WriteStats(this);
            Components.Add(continueBuilding);
            Components.Add(basicFloorGenerate);
            Components.Add(clickCooridantes);
            Components.Add(new GenerateWorker(this));
            Components.Add(new RemoveDeadUnits(this));
            Components.Add(new BaseBuildingBuilder(this));
            Components.Add(new WaterGathering(this));
            Components.Add(new DrawPowderSources(this, 30));
            Components.Add(new GatherResources(this));
            Components.Add(new BluePowderGathering(this));
            platform = new GameObject("yellowChangePlatform", this, new Vector3(23, 0, 7), -1.57f, 0, 0, 0.05f, false, "platform", 2); 
            Components.Add(new ChangeToWarrior(this, platform));
            movingController = new MovingController(this);
            Components.Add(movingController);
            //Components.Add(writeStats);
            Components.Add(new MovingController(this));
            //Components.Add(writeStats);
            Components.Add(new ShowInfoAboutBuilding(this));
            Components.Add(new MoveEnemyToWarrior(this));
            video = Content.Load<Video>("building");
            player = new VideoPlayer();
            video2 = Content.Load<Video>("building2");
            player2 = new VideoPlayer();
            floatingText = new FloatingText(this, platform.transform, "");
            //Components.Add(floatingText);
            Components.Add(new SpawnEnemies(this));
          //  Components.Add(new MoveLocalEnemyToWarrior(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {

            soundEffect = Content.Load<SoundEffect>("jellybounce");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region commented
            //State right = new MoveRigth();
            //State left = new MoveLeft();
            //FinateStateMachine move =
            //    new FinateStateMachineBuilder()
            //    .AddState(right)
            //    .AddState(left)
            //    .AddTransion(left, right, new TrueAfter100Frames().ChangeState)
            //    .AddTransion(right, left, new TrueAfter100Frames().ChangeState)
            //    .Build();

            //aniamteZarlok = new FinateStateMachineBuilder()
            //    .AddState(new Animate("Take 001", 20, true))
            //    .Build();

            //GameObject jelly1 = new Jelly("Jelly", this, new Vector3(10f, 0f, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker", 0.6f);
            //{
            //    finateSatemachine = move
            //};

            //Texture2D jellyTexture = Content.Load<Texture2D>("jelly_texture"); //wczytanie nowej teksury z Content Manager'a
            //scene.AddSceneObject("galaretka_001", jelly1);
            //scene.AddSceneObject("galaretka_003", new Jelly("jumping", this, new Vector3(8f, 0, 8f), 0f, 0f, 0f, 0.01f, true, "worker") {finateSatemachine = aniamteJelly });

            //foreach (ModelMesh mesh in scene.SceneObjects["galaretka_005"].model.Meshes)
            //{
            //    foreach (Effect effect in mesh.Effects)
            //    {
            //        SkinnedEffect skinnedEffect = effect as SkinnedEffect;
            //        if (skinnedEffect != null)
            //            skinnedEffect.Texture = jellyTexture;
            //    }
            //} //Ustawiam teksture recznie wewnatrz efektu.


            //State JellyJumping = new Animate("1", 20, true);
            //State JellyMelting = new Animate("2", 20, true);
            //State JellyWaitng = new Animate("3", 20, true);
            //animateJelly = new FinateStateMachineBuilder()
            //    .AddState(JellyJumping)
            //    .AddState(JellyWaitng)
            //    .AddState(JellyMelting)
            //    .AddTransion(JellyWaitng, JellyJumping, new AnimationStateChanger().ChangeState)
            //    .AddTransion(JellyJumping, JellyWaitng, new AnimationStateChanger().ChangeState)
            //    .Build();

            //
            #endregion

            flr = new GameObject("mountainTerrain", this, new Vector3(50, 0, 50), -1.57f, -1.5f, 0, 1f, false, "floor",0.9f);
            scene.AddSceneObject("podloga",flr );
            scene.Floor = flr;
            scene.AddSceneObject("zarlok_001", new Enemy("zarlok", this, new Vector3(10f, 0, 10f), 0f, 3.14f, 0f, 0.02f, true, "enemy", 0.5f*0.9f));
            scene.AddSceneObject("galaretka_003", new Jelly("jelly_blue", this, new Vector3(6f, 0, 8f), 0f, 0f, 0f, 0.005f, true, "worker",0.6f));
            scene.AddSceneObject("galaretka_004", new Jelly("jelly_blue", this, new Vector3(10f, 0, 8f), 0f, 0f, 0f, 0.005f, true, "worker", 0.6f));
            scene.AddSceneObject("galaretka_005", new Jelly("jelly_blue", this, new Vector3(14f, 0, 8f), 0f, 0f, 0f, 0.005f, true, "worker", 0.6f));
            scene.AddSceneObject("galaretka_007", new Warrior("jelly_yellow", this, new Vector3(4f, 0, 8f), 0, 0f, 0f, 0.008f, true, "warrior", 0.9f));
            scene.AddSceneObject("baza_001", new Spawn("baza", this, new Vector3(15, 0, 15), -1.57f, 0, 0, 0.009f, false, "baza", 2.5f));
            scene.AddSceneObject("baza_003", new Spawn("baseEnemy", this, new Vector3(50, 0f, 50), -1.57f, 0, 0, 0.07f, false, "bazaenemy", 8.2f));
            spawn = new Spawn("spawn", this, new Vector3(95f, 0, 5f), -1.5f, -1.55f, 0f, 0.05f, true, "spawn", 1.6f);
            scene.AddSceneObject("yellowPlatform", platform);
            scene.AddSceneObject("spawn",spawn);

            scene.AddSceneObject("zarlok_stacjonarny_001", new Enemy("zarlok", this, new Vector3(91f, 0, 3f), 0f, 3.14f, 0f, 0.02f, true, "enemyLocal", 0.5f * 0.9f));
            scene.AddSceneObject("zarlok_stacjonarny_002", new Enemy("zarlok", this, new Vector3(91f, 0, 5f), 0f, 3.14f, 0f, 0.02f, true, "enemyLocal", 0.5f * 0.9f));
            scene.AddSceneObject("zarlok_stacjonarny_003", new Enemy("zarlok", this, new Vector3(91f, 0, 7f), 0f, 3.14f, 0f, 0.02f, true, "enemyLocal", 0.5f * 0.9f));
            scene.AddSceneObject("zarlok_stacjonarny_004", new Enemy("zarlok", this, new Vector3(93f, 0, 3f), 0f, 3.14f, 0f, 0.02f, true, "enemyLocal", 0.5f * 0.9f));
            scene.AddSceneObject("zarlok_stacjonarny_005", new Enemy("zarlok", this, new Vector3(93f, 0, 7f), 0f, 3.14f, 0f, 0.02f, true, "enemyLocal", 0.5f * 0.9f));

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

            if (!switcher)
            {
                temporaryRot = camera.Rotation;
                temporaryPos = camera.Position;
            }
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Tab) && lastKeyboardState.IsKeyUp(Keys.Tab))
            {
                switcher = !switcher;
                if (switcher)
                {
                    camera.Rotation = new Vector3(1.5f, 0, 0);
                    camera.Position = new Vector3(50, 130, 50);
                }
                if (!switcher)
                {
                    camera.Position = temporaryPos;
                    camera.Rotation = temporaryRot;
                }
               
            }
            lastKeyboardState = currentState;
            // TODO: Add your update logic here
            //Debug.WriteLine(1000.0f/gameTime.ElapsedGameTime.TotalMilliseconds);  FPS COUNTER
            foreach (GameObject gameObject in scene.SceneObjects.Values)
            {
                gameObject.update((float)gameTime.ElapsedGameTime.TotalMilliseconds, soundEffect);
                    //flr.transform.XRotation += 0.0005f;
                   // Debug.WriteLine(gameObject.transform.XRotation);
                gameObject.Update(gameTime);
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(28, 44, 68));
            base.Draw(gameTime);
            if (switcher)
            {
                
                if (player.State == MediaState.Stopped)
                {
                    player.Play(video);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    player.Play(video2);
                }

                Texture2D videoTexture = null;

                if (player.State != MediaState.Stopped)
                {
                    videoTexture = player.GetTexture();
                }

                if (videoTexture != null )
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(videoTexture, new Rectangle(50, 200, 400, 250), Color.White);
                    spriteBatch.End();
                }
            }
            else
            {
                player.Stop();
                player2.Stop();
            }
            
        }
    }

}
