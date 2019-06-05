using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceJellyMONO.FSM;
using SpaceJellyMONO.FSM.States;
using SpaceJellyMONO.FSM.Trans;

namespace SpaceJellyMONO
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public Camera camera;
        BasicFloorGenerate basicFloor;
        BasicEffect effect;
        public GameObjectsRepository gameObjectsRepository;
        public Scene scene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1020;
            graphics.PreferredBackBufferWidth = 1920;
            IsMouseVisible = true;
            gameObjectsRepository = new GameObjectsRepository();
        }

        protected override void Initialize()
        {
            //TargetElapsedTime = new TimeSpan(TargetElapsedTime.Ticks / 2);
            //IsFixedTimeStep = false;
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 3f, 5f), new Vector3(0.8f, 0, 0), 5f, graphics);
            Components.Add(camera);
            Effect shadowedEffect = Content.Load<Effect>("custom_effects/Shadowed");
            Texture2D floorTexture = Content.Load<Texture2D>("diffuse3");
            basicFloor = new BasicFloorGenerate(GraphicsDevice, 20, 20, shadowedEffect, floorTexture);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            scene = new Scene(new Transform(new Vector3(0f, 0f, 0f), 0f, 0f, 0f, 1f));

            Components.Add(new Selector(this));
            Effect shadowMapEffect = Content.Load<Effect>("custom_effects/ShadowMap");
            Components.Add(new RenderEngine(this, camera, basicFloor, 1920, 1080, shadowMapEffect));

            base.Initialize();
        }

        protected override void LoadContent()
        {
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

            scene.AddSceneObject("zarlok_001", new GameObject("zarlok_poprawiony", camera, this, new Vector3(10f, 0, 10f), 0f, 3.14f, 0f, 0.05f, false));

            GameObject jelly1 = new GameObject("Jelly", camera, this, new Vector3(10f, 0f, 8f), 0f, 0f, 0f, 0.5f, true)
            {
                finateSatemachine = move
            };
            scene.AddSceneObject("galaretka_001", jelly1);
            scene.AddSceneObject("galaretka_002", new GameObject("Jelly", camera, this, new Vector3(9f, 0, 8f), 0f, 0f, 0f, 0.1f, true));

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
            // Debug.WriteLine(1000.0f/gameTime.ElapsedGameTime.TotalMilliseconds); //fps counter ultra dupa mnnbhgugnd
            foreach (GameObject gameObject in scene.SceneObjects.Values)
            {
                gameObject.update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
                gameObject.Update(gameTime);
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //basicFloor.Draw(camera, effect);
            base.Draw(gameTime);

        }
    }

}
