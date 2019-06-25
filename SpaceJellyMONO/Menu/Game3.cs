using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SpaceJellyMONO
{
    public class Game3 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D exitButton;
        VideoPlayer player, player2;
        Video video, video2;
        public bool returnToMenu = false;
        Color color;
        Rectangle rectangle;

        public Game3()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1020;
            graphics.PreferredBackBufferWidth = 1920;
            IsMouseVisible = true;
            
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
            video = Content.Load<Video>("building");
            video2 = Content.Load<Video>("building2");
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
            GraphicsDevice.Clear(new Color(28, 44, 68));
            base.Draw(gameTime);
            if (player.State == MediaState.Stopped)
            {
                player.Play(video);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                player.Play(video2); // cos sypie 
            }

            Texture2D videoTexture = null;

            if (player.State != MediaState.Stopped)
            {
                videoTexture = player.GetTexture();
            }

            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, new Rectangle(50, 200, 400, 250), Color.White);
                spriteBatch.Draw(exitButton, rectangle, color);
                spriteBatch.End();
            }

        }
    }

}
