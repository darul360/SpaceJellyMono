using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using SpaceJellyMONO.Units;

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
        private MouseState lastMouseState = new MouseState();

        public Selector(Game1 game) : base(game)
        {
            this.game = game;
            checkerboardTexture = game.exportContentManager().Load<Texture2D>("diffuse3");
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private void ChangeSelectedObjectsState(Vector3 startPoint, Vector3 stopPoint)
        {
            foreach (GameObject model in game.gameObjectsRepository.getRepo())
            {
                
                if(model is Unit)
                {
                    Unit selectableUnit = model as Unit;
                    if (startPoint.X > stopPoint.X)
                    {
                        if (startPoint.Z > stopPoint.Z)
						{   if ((selectableUnit.transform.Translation.X >= stopPoint.X && selectableUnit.transform.Translation.X <= startPoint.X) && (selectableUnit.transform.Translation.Z >= stopPoint.Z && selectableUnit.transform.Translation.Z <= startPoint.Z))
                            {
                                if (model.GameTag == "worker" || model.GameTag == "warrior")
                                {
                                    //selectableUnit.IsSelected = true;
                                    game.selectedObjectsRepository.AddToRepo(selectableUnit);
                                    //setPrimary(selectableUnit);
                                }
                            }
                        }
                        if (startPoint.Z < stopPoint.Z)
                        {

                            if ((selectableUnit.transform.Translation.X >= stopPoint.X && selectableUnit.transform.Translation.X <= startPoint.X) && (selectableUnit.transform.Translation.Z >= startPoint.Z && selectableUnit.transform.Translation.Z <= stopPoint.Z))

                            {
                                if ((model.GameTag == "worker" || model.GameTag == "warrior"))
                                {
                                    //selectableUnit.IsSelected = true;
                                    game.selectedObjectsRepository.AddToRepo(selectableUnit);
                                    //setPrimary(selectableUnit);
                                }
                            }
                        }
                    }


                    if (startPoint.X < stopPoint.X)
                    {
                        if (startPoint.Z > stopPoint.Z)
                        {

                            if ((selectableUnit.transform.Translation.X >= startPoint.X && selectableUnit.transform.Translation.X <= stopPoint.X) && (selectableUnit.transform.Translation.Z >= stopPoint.Z && selectableUnit.transform.Translation.Z <= startPoint.Z))

                            {
                                if (model.GameTag == "worker" || model.GameTag == "warrior")
                                {
                                    //selectableUnit.IsSelected = true;
                                    game.selectedObjectsRepository.AddToRepo(selectableUnit);
                                    //setPrimary(selectableUnit);
                                }
                            }
                        }
                        if (startPoint.Z < stopPoint.Z)
                        {
                            if ((selectableUnit.transform.Translation.X >= startPoint.X && selectableUnit.transform.Translation.X <= stopPoint.X) && (selectableUnit.transform.Translation.Z >= startPoint.Z && selectableUnit.transform.Translation.Z <= stopPoint.Z))
                            {
                                if (model.GameTag == "worker" || model.GameTag == "warrior")
                                {
                                    //selectableUnit.IsSelected = true;
                                    game.selectedObjectsRepository.AddToRepo(selectableUnit);
                                    //setPrimary(selectableUnit);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void DeselectAll()
        {
            game.selectedObjectsRepository.ClearAll();
            
            foreach (GameObject model in game.selectedObjectsRepository.getRepo())
            {
                if (!model.isMoving)
                {
                        ((Unit)model).IsSelected = false;
                        model.mainClass.selectedObjectsRepository.getRepo().Remove(model);
                }
            }
            
        }


        private void setPrimary(GameObject go2)
        {
            //int counter = 0;
            //foreach(GameObject go in game.gameObjectsRepository.getRepo())
            //{
            //    if (go.isPrimary == true)
            //        counter++;
            //}

            //if (counter == 0) go2.isPrimary = true;

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
            BasicEffect effect = new BasicEffect(Game.GraphicsDevice);
            effect.View = game.camera.View;
            effect.Projection = game.camera.Projection;
            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;
            effect.Alpha = 0.3f;

            if ((start.X > end.X && start.Z < end.Z) || (start.X < end.X && start.Z > end.Z))
            {
                verticies[0].Position = new Vector3(start.X, 0.5f, start.Z);
                verticies[1].Position = new Vector3(start.X, 0.5f, end.Z);
                verticies[2].Position = new Vector3(end.X, 0.5f, start.Z);
                verticies[3].Position = new Vector3(start.X, 0.5f, end.Z);
                verticies[4].Position = new Vector3(end.X, 0.5f, end.Z);
                verticies[5].Position = new Vector3(end.X, 0.5f, start.Z);
            }

            else
            {
                verticies = new VertexPositionTexture[6];
                verticies[0].Position = new Vector3(start.X, 0.5f, start.Z);
                verticies[1].Position = new Vector3(end.X, 0.5f, start.Z);
                verticies[2].Position = new Vector3(start.X, 0.5f, end.Z);
                verticies[3].Position = new Vector3(end.X, 0.5f, start.Z);
                verticies[4].Position = new Vector3(end.X, 0.5f, end.Z);
                verticies[5].Position = new Vector3(start.X, 0.5f, end.Z);
            }


            game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, verticies, 0, 2);
            }
        }

        private void drawRect()
        {
            Game.GraphicsDevice.BlendState = BlendState.Additive;
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && isFirstPointSelected == false)
            {
                startPoint = FindWhereClicked();
                DeselectAll();
                isFirstPointSelected = true;
            }
            if (mouseState.LeftButton == ButtonState.Pressed && isFirstPointSelected == true)
            {
                endPoint = FindWhereClicked();
                DrawRect(startPoint, endPoint);
                ChangeSelectedObjectsState(startPoint, endPoint);
            }
            if (mouseState.LeftButton == ButtonState.Released)
            {
                isFirstPointSelected = false;
                isLastPointSelected = false;
            }
            Game.GraphicsDevice.BlendState = BlendState.Opaque;

        }
        public void singleClickSelect()
        {
            foreach(GameObject go in game.gameObjectsRepository.getRepo())
            {
                if (Vector3.Distance(FindWhereClicked(), go.transform.translation) < 0.5f)
                {
                    MouseState currentState = Mouse.GetState();

                    if (currentState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                    {
                        if (go.GameTag == "worker" || go.GameTag == "warrior")
                        {
                            if (go is Unit)
                            {
                                Unit selectableUnit = go as Unit;
                                go.isObjectSelected = true;
                                game.selectedObjectsRepository.AddToRepo(selectableUnit);
                            }
                        }
                    }
                    lastMouseState = currentState;
                }
            }
        }


        public override void Draw(GameTime gameTime)
        {
            
            drawRect();
            singleClickSelect();
            base.Draw(gameTime);
        }
    }
}