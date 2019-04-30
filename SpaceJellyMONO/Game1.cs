using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            GameObject modelLoader,modelLoader2;
            Model jelly;
            BasicAnimation jumpAnimation;
            public GameObjectsRepository gameObjectsRepository;

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
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 3f, 5f), new Vector3(0.8f,0,0), 5f,graphics);
            Components.Add(camera);
            basicFloor = new BasicFloorGenerate(GraphicsDevice, 20, 20);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            modelLoader = new GameObject("Jelly", camera, this, new Vector3(10, 0, 8),0f,0f,0, 1.0f,true);
            modelLoader2 = new GameObject("Jelly", camera, this, new Vector3(13, 0, 8), 0f, 0f, 0, 1.0f, false);
            Components.Add(new BasicAnimation(this, Matrix.CreateScale(0.2f) * Matrix.CreateTranslation(new Vector3(10f, 0f, 8f)), Matrix.CreateScale(0.8f, 1.4f, 0.8f), Matrix.Identity, Matrix.CreateTranslation(new Vector3(0f, 2f, 0f))));
            Components.Add(new Selector(this));

            foreach (GameObject obj in gameObjectsRepository.getRepo())
            {
                Components.Add(obj);
            }
            base.Initialize();
            }

            protected override void LoadContent()
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                jelly = Content.Load<Model>("Jelly");

            foreach (ModelMesh mesh in jelly.Meshes)
                foreach (BasicEffect effect in mesh.Effects)
                    effect.EnableDefaultLighting();
                        
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
            modelLoader.update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                basicFloor.Draw(camera, effect);
                
                //jelly.Draw(jumpAnimation.position, camera.View, camera.Projection);


            base.Draw(gameTime);
            
            }
        }

    }
