using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpaceJellyMONO
{
        /// <summary>
        /// This is the main type for your game.
        /// </summary>
        public class Game1 : Game
        {
            GraphicsDeviceManager graphics;
            SpriteBatch spriteBatch;
            Vector3 cameraTarget;
            Vector3 cameraPostion;
            /*---SECTION OF CONVERTING 3D OBJECT TO 2D */
            Matrix projection;      /*--RZUTOWANIE 3D NA 2D--*/
            Matrix view;            /*--POSITION OF VIRTUAL CAMERA--*/
            Matrix worldMatrix;     /*--POSITION OF OBJECT--*/
            bool orbit;
            Model model;

            public Game1()
            {
                graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
            }

            /// <summary>
            /// Allows the game to perform any initialization it needs to before starting to run.
            /// This is where it can query for any required services and load any non-graphic
            /// related content.  Calling base.Initialize will enumerate through any components
            /// and initialize them as well.
            /// </summary>
            protected override void Initialize()
            {
                base.Initialize();
                cameraTarget = new Vector3(0.0f, 0.0f, 143.0f);
                cameraPostion = new Vector3(0.0f, 100.0f,0.0f);
                /*--Tworzenie jakby trójkąta kamery, trójkąta widoku--*/
                //Obrazek       Szerokość matrycy
                projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
                view = Matrix.CreateLookAt(cameraPostion, cameraTarget, Vector3.Up);
                worldMatrix = Matrix.CreateWorld(cameraTarget, Vector3.Forward, Vector3.Up);
            }

            /// <summary>
            /// LoadContent will be called once per game and is the place to load
            /// all of your content.
            /// </summary>
            protected override void LoadContent()
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);
                model = Content.Load<Model>("Floor");

            }

            /// <summary>
            /// UnloadContent will be called once per game and is the place to unload
            /// game-specific content.
            /// </summary>
            protected override void UnloadContent()
            {
                // TODO: Unload any non ContentManager content here
            }

            /// <summary>
            /// Allows the game to run logic such as updating the world,
            /// checking for collisions, gathering input, and playing audio.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Update(GameTime gameTime)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    cameraPostion.X += 1f;
                    cameraTarget.X += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    cameraPostion.X -= 1f;
                    cameraTarget.X -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    cameraPostion.Y -= 1f;
                    cameraTarget.Y -= 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    cameraPostion.Y += 1f;
                    cameraTarget.Y += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    cameraPostion.Z += 1f;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    cameraPostion.Z -= 1f;
                }
                view = Matrix.CreateLookAt(cameraPostion, cameraTarget, Vector3.Down);
                base.Update(gameTime);
            }

            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.Black);
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.View = view;
                        effect.Projection = projection;
                        effect.World = worldMatrix;
                        mesh.Draw();

                    }
                }
                base.Draw(gameTime);
            }
        }

    }
