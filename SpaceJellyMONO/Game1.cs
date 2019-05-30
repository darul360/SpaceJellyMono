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
using SpaceJellyMONO.World;
using System.Diagnostics;
using System.Linq;

namespace SpaceJellyMONO
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Camera camera;
        BasicEffect effect;
        public GameObjectsRepository gameObjectsRepository;
        public Scene scene;
        public FindPath findPath;
        public ClickCooridantes clickCooridantes;
        public int gridW, gridH;
        public BasicFloorGenerate basicFloorGenerate;


        //sound
        SoundEffect soundEffect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1020;
            graphics.PreferredBackBufferWidth = 1920;
            IsMouseVisible = true;
            gameObjectsRepository = new GameObjectsRepository();
            gridW = 100;
            gridH = 100;
            findPath = new FindPath(gridW, gridH);

        }

        protected override void Initialize()
        {
            //TargetElapsedTime = new TimeSpan(TargetElapsedTime.Ticks / 2);
            //IsFixedTimeStep = false;
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 8f, 5f), new Vector3(0.8f, 0, 0), 10f, graphics);
            Components.Add(camera);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            scene = new Scene(camera, new Transform(new Vector3(0f, 0f, 0f), 0f, 0f, 0f, 1f));
            basicFloorGenerate = new BasicFloorGenerate(GraphicsDevice, gridW, gridH, spriteBatch, this);
            Components.Add(basicFloorGenerate);
            Components.Add(new Selector(this));
            Components.Add(new RenderEngine(this));
            clickCooridantes = new ClickCooridantes(this);
            Components.Add(clickCooridantes);
            Components.Add(new BaseBuildingBuilder(this));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            soundEffect = Content.Load<SoundEffect>("jellybounce");

            State right = new MoveRigth();
            State left = new MoveLeft();
            FinateStateMachine move =
                new FinateStateMachineBuilder()
                .AddState(right)
                .AddState(left)
                .AddTransion(left, right, new TrueAfter100Frames().ChangeState)
                .AddTransion(right, left, new TrueAfter100Frames().ChangeState)
                .Build();

            scene.AddSceneObject("zarlok_001", new GameObject("zarlok_poprawiony", this, new Vector3(10f, 0, 10f), 0f, 3.14f, 0f, 0.05f, false,"enemy"));

            GameObject jelly1 = new Jelly("Jelly", this, new Vector3(10f, 0f, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker")
            {
                finateSatemachine = move
            };
            scene.AddSceneObject("galaretka_001", jelly1);
            scene.AddSceneObject("galaretka_002", new Jelly("Jelly", this, new Vector3(8f, 0, 8f), -1.57f, 0f, 0f, 0.5f, true,"worker"));
            scene.AddSceneObject("galaretka_003", new Jelly("Jelly", this, new Vector3(6f, 0, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker"));
            scene.AddSceneObject("galaretka_004", new Jelly("Jelly", this, new Vector3(4f, 0, 8f), -1.57f, 0f, 0f, 0.5f, true, "worker"));
            scene.SceneObjects["zarlok_001"].StartAnimationClip("Take 001", 20, true);
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
