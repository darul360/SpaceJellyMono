using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
        /// <summary>
        /// This is the main type for your game.
        /// </summary>
        public class Game1 : Game
        {
            GraphicsDeviceManager graphics;
            SpriteBatch spriteBatch;
            Camera camera;
            BasicFloorGenerate basicFloor;
            BasicEffect effect;
            ModelLoader modelLoader;
        

            public Game1()
            {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            this.IsMouseVisible = true;
            }

            protected override void Initialize()
            {
            /*-----KAMERA-----*/
            camera = new Camera(this, new Vector3(10f, 3f, 5f), new Vector3(0.8f,0,0), 5f,graphics);
            Components.Add(camera);
            basicFloor = new BasicFloorGenerate(GraphicsDevice, 20, 20);
            effect = new BasicEffect(GraphicsDevice);

            /*-----MODELE-----*/
            modelLoader = new ModelLoader("Floor", camera, this, 0.2f, 0.01f);

            base.Initialize();
            }

            protected override void LoadContent()
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
            }

            public ContentManager exportContentManager()
            {
                return base.Content;
            }
            
            protected override void UnloadContent()
            {
            
            }

            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                basicFloor.Draw(camera, effect);

            modelLoader.draw();
            
            base.Draw(gameTime);
            }
        }

    }
