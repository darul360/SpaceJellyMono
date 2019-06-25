using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class Game3 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D exitButton;
        VideoPlayer player, player2;
        Video video, video2,video3,video4,video5;
        public bool returnToMenu = false;
        Color color;
        Rectangle rectangle;
        List<Video> lista;
        //Texture2D videoTexture = null;

        int i = 0;

        public Game3()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1020;
            graphics.PreferredBackBufferWidth = 1920;
            IsMouseVisible = true;
            lista = new List<Video>();
            
        }

        protected override void Initialize()
        {
            rectangle = new Rectangle(1720, 880, 200, 100);
            color = Color.White;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new VideoPlayer();
            player2 = new VideoPlayer();
            video = Content.Load<Video>("t5");
            video2 = Content.Load<Video>("t3");
            video3 = Content.Load<Video>("t2");
            video4 = Content.Load<Video>("t4");
            video5 = Content.Load<Video>("t1");
            lista.Add(video);lista.Add(video2);lista.Add(video3);lista.Add(video4);lista.Add(video5);
            exitButton = Content.Load<Texture2D>("exitButton");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            MouseState mouseState = Mouse.GetState();
            if (mouseState.X > rectangle.X && mouseState.X < rectangle.X + rectangle.Width
            && mouseState.Y > rectangle.Y && mouseState.Y < rectangle.Y + rectangle.Height)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    returnToMenu = true;
                    Exit();
                }
                else
                {
                    color = Color.Teal;
                }
            }
            else
            {
                color = Color.White;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Debug.WriteLine(i);
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            if (player.State == MediaState.Stopped)
            {
                player.Play(video4);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (i <= 4)
                {
                    player.Play(lista[i]);
                    i++;
                    
                }
                else
                {
                    player.Play(lista[0]);
                    i = 0;
                }
            }


            Texture2D videoTexture = null;

            if (player.State != MediaState.Stopped)
            {
                videoTexture = player.GetTexture();
            }

            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, new Rectangle(500, 400, 1000, 480), Color.White);
                spriteBatch.Draw(exitButton, rectangle, color);
                spriteBatch.End();
            }

        }
    }

}
