using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class Selector : DrawableGameComponent
    {
        Game1 game;
        Texture2D checkerboardTexture;
        private VertexPositionTexture[] verticies;
        private bool isFirstPointSelected = false;
        private bool isLastPointSelected = false;
        private bool reclicked = false;
        Vector3 startPoint = new Vector3(0, 0, 0);
        Vector3 endPoint = new Vector3(0, 0, 0);

        public Selector(Game1 game) : base(game)
        {
            this.game = game;
            checkerboardTexture = game.exportContentManager().Load<Texture2D>("diffuse3");
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private void ChangeSelectedObjectsState(bool state,Vector3 startPoint,Vector3 stopPoint)
        {
            foreach(ModelLoader model in game.gameObjectsRepository.getRepo())
            {
                if (startPoint.X > stopPoint.X)
                {
                    if(startPoint.Z > stopPoint.Z)
                    {
                        if ((model.transform.Translation.X >= startPoint.X && model.transform.Translation.X <= stopPoint.X) && (model.transform.Translation.Z >= startPoint.Z && model.transform.Translation.Z <= stopPoint.Z))
                        {
                            /*TODO*/
                        }
                    }
                    else
                    {

                    }
                }

                if(startPoint.X < stopPoint.X)
                {

                }
            }
        }

        private Vector3 FindWhereClicked()
        {
            GraphicsDevice graphicsDevice = game.GraphicsDevice;
            MouseState mouseState = Mouse.GetState();

            Vector3 nearSource = new Vector3((float)mouseState.X, (float)mouseState.Y, 0f);
            Vector3 farSource = new Vector3((float)mouseState.X, (float)mouseState.Y, 1f);
            Vector3 nearPoint = graphicsDevice.Viewport.Unproject(nearSource, game.camera.Projection, game.camera.View, Matrix.Identity);
            Vector3 farPoint = graphicsDevice.Viewport.Unproject(farSource, game.camera.Projection, game.camera.View, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            Ray ray = new Ray(nearPoint, direction);

            Vector3 n = new Vector3(0f, 1f, 0f);
            Plane p = new Plane(n, 0f);

            float denominator = Vector3.Dot(p.Normal, ray.Direction);
            float numerator = Vector3.Dot(p.Normal, ray.Position) + p.D;
            float t = -(numerator / denominator);

            Vector3 pickedPosition = nearPoint + direction * t;

            return pickedPosition;
        }

        public void DrawRect(Vector3 start, Vector3 end)
        {
            BasicEffect effect = new BasicEffect(GraphicsDevice);
            effect.View = game.camera.View;
            effect.Projection = game.camera.Projection;
            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;

            if ((start.X > end.X && start.Z < end.Z) || (start.X < end.X && start.Z > end.Z))
            {

                verticies[0].Position = new Vector3(start.X, 0.001f, start.Z);
                verticies[1].Position = new Vector3(start.X, 0.001f, end.Z);
                verticies[2].Position = new Vector3(end.X, 0.001f, start.Z);
                verticies[3].Position = new Vector3(start.X, 0.001f, end.Z);
                verticies[4].Position = new Vector3(end.X, 0.001f, end.Z);
                verticies[5].Position = new Vector3(end.X, 0.001f, start.Z);
            }

            else
            {
                verticies = new VertexPositionTexture[6];
                verticies[0].Position = new Vector3(start.X, 0.001f, start.Z);
                verticies[1].Position = new Vector3(end.X, 0.001f, start.Z);
                verticies[2].Position = new Vector3(start.X, 0.001f, end.Z);
                verticies[3].Position = new Vector3(end.X, 0.001f, start.Z);
                verticies[4].Position = new Vector3(end.X, 0.001f, end.Z);
                verticies[5].Position = new Vector3(start.X, 0.001f, end.Z);
            }



            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, verticies, 0, 2);

            }
        }

        private void drawRect()
        {
            
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && isFirstPointSelected == false)
            {              
                startPoint = FindWhereClicked();
                isFirstPointSelected = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && isFirstPointSelected == true)
            {
                endPoint = FindWhereClicked();
                DrawRect(startPoint, endPoint);
                Debug.WriteLine(startPoint.X + " " + startPoint.Z);
                Debug.WriteLine(endPoint.X + " " + endPoint.Z);

            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                isFirstPointSelected = false;
                isLastPointSelected = false;
            }

        }


        public override void Draw(GameTime gameTime) {
            drawRect();
        }
    }
}
