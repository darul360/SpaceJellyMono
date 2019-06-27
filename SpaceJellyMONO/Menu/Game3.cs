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
        public Texture2D exitButton,rbutton,lbutton;
        VideoPlayer player, player2;
        Video video, video2,video3,video4,video5;
        public bool PLAYGAME = false;
        Color color,lcolor,rcolor;
        Rectangle rectangle,lrec,rrec;
        List<Video> lista;
        private MouseState lastMouseState = new MouseState();
        private MouseState lastMouseState2 = new MouseState();
        private MouseState lastMouseState3 = new MouseState();
        Texture2D videoTexture = null;

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
            lrec = new Rectangle(100, 400, 200, 400);
            rrec = new Rectangle(1620, 400, 200, 400);
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
            rbutton = Content.Load<Texture2D>("rbutton");
            lbutton = Content.Load<Texture2D>("lbutton");
            lista.Add(video);lista.Add(video2);lista.Add(video3);lista.Add(video4);lista.Add(video5);
            exitButton = Content.Load<Texture2D>("SKIP");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();



            MouseState mouseState = Mouse.GetState();
            if (mouseState.X > rectangle.X && mouseState.X < rectangle.X + rectangle.Width
            && mouseState.Y > rectangle.Y && mouseState.Y < rectangle.Y + rectangle.Height)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    PLAYGAME = true;
                    player.Stop();
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
            lastMouseState = mouseState;

            MouseState mouseState2 = Mouse.GetState();
            if (mouseState2.X > lrec.X && mouseState2.X < lrec.X + lrec.Width
            && mouseState2.Y > lrec.Y && mouseState2.Y < lrec.Y + lrec.Height)
            {
                if (mouseState2.LeftButton == ButtonState.Pressed && lastMouseState2.LeftButton == ButtonState.Released)
                {
                    i--;
                    if (i == -1) i = 4;
                }
                else
                {
                    lcolor = Color.Teal;
                }

            }
            else
            {
                lcolor = Color.White;
            }
            lastMouseState2 = mouseState2;

            MouseState mouseState3 = Mouse.GetState();
            if (mouseState.X > rrec.X && mouseState.X < rrec.X + rrec.Width
            && mouseState.Y > rrec.Y && mouseState.Y < rrec.Y + rrec.Height)
            {
                if (mouseState3.LeftButton == ButtonState.Pressed && lastMouseState3.LeftButton == ButtonState.Released)
                {
                   
                    i++;
                    if (i == 5) i = 0;
                }
                else
                {
                    rcolor = Color.Teal;
                }
            }
            else
            {
                rcolor = Color.White;
            }
            lastMouseState3 = mouseState3;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (player.State == MediaState.Stopped)
            {
                player.Play(lista[i]);
            }

            {
                player.Play(lista[i]);
            }



            if (player.State != MediaState.Stopped)
            {
                videoTexture = player.GetTexture();
            }

            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, new Rectangle(200, 200, 1520, 680), Color.White);
                spriteBatch.Draw(exitButton, rectangle, color);
                spriteBatch.Draw(lbutton, lrec , lcolor);
                spriteBatch.Draw(rbutton, rrec, rcolor);
                spriteBatch.End();
            }

            base.Draw(gameTime);

        }
    }

}
