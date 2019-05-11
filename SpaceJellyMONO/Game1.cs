using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SkinnedModel;
using System;

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
            this.IsMouseVisible = true;
            this.gameObjectsRepository = new GameObjectsRepository();
        }

            protected override void Initialize()
            {
            //TargetElapsedTime = new TimeSpan(TargetElapsedTime.Ticks / 2);
            //IsFixedTimeStep = false;
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 3f, 5f), new Vector3(0.8f,0,0), 5f,graphics);
            Components.Add(camera);
            basicFloor = new BasicFloorGenerate(GraphicsDevice, 20, 20);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            scene = new Scene(camera, new Transform(new Vector3(0f, 0f, 0f), 0f, 0f, 0f, 1f));

            Components.Add(new Selector(this));
            Components.Add(new RenderEngine(this));

            base.Initialize();
            }

            protected override void LoadContent()
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);

                scene.AddSceneObject("zarlok_001", new GameObject("zarlok_poprawiony", camera, this, new Vector3(10f, 0, 10f), 0f, 3.14f, 0f, 0.05f, false));
                scene.AddSceneObject("galaretka_001", new GameObject("Jelly", camera, this, new Vector3(10f, 0f, 8f), 0f, 0f, 0f, 0.5f, true));
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
            foreach(GameObject gameObject in scene.SceneObjects.Values)
            {
                gameObject.update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
                gameObject.Update(gameTime);
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                basicFloor.Draw(camera, effect);

            base.Draw(gameTime);
            
            }
        }

    }
