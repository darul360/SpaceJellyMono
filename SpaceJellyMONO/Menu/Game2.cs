using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SpaceJellyMONO
{
    public class Game2 : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D playButton,tutorialButton,exitButton;
        Rectangle playRect,tutRect,exitRect;
        Color color, color2, color3;
        public bool play = false,tutorial = false;

        public Game2()
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
            playRect = new Rectangle(910, 415, 100, 50);
            tutRect = new Rectangle(910, 485, 100, 50);
            exitRect = new Rectangle(910, 555, 100, 50);
            color = Color.White;
            color2 = Color.White;
            color3 = Color.White;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playButton = Content.Load<Texture2D>("playButton");
            tutorialButton = Content.Load<Texture2D>("tutorialButton");
            exitButton = Content.Load<Texture2D>("exitButton");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState mouseState = Mouse.GetState();
            #region play
            if (mouseState.X > playRect.X && mouseState.X < playRect.X + playRect.Width
                && mouseState.Y > playRect.Y && mouseState.Y < playRect.Y + playRect.Height)
            {
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    play = true;
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
            #endregion
            #region exit
            if (mouseState.X > exitRect.X && mouseState.X < exitRect.X + exitRect.Width
            && mouseState.Y > exitRect.Y && mouseState.Y < exitRect.Y + exitRect.Height)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Exit();
                }
                else
                {
                    color2 = Color.Teal;
                }
            }
            else
            {
                color2 = Color.White;
            }
            #endregion

            #region tutorial
            if (mouseState.X > tutRect.X && mouseState.X < tutRect.X + tutRect.Width
            && mouseState.Y > tutRect.Y && mouseState.Y < tutRect.Y + tutRect.Height)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    tutorial = true;
                    Exit();
                }
                else
                {
                    color3 = Color.Teal;
                }
            }
            else
            {
                color3 = Color.White;
            }
            #endregion
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(28, 44, 68));
            spriteBatch.Begin();
            spriteBatch.Draw(playButton,playRect, color);
            spriteBatch.Draw(exitButton, exitRect, color2);
            spriteBatch.Draw(tutorialButton, tutRect, color3);
            spriteBatch.End();
            base.Draw(gameTime);
         
        }
    }

}
