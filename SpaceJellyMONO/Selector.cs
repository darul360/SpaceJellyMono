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
        Vector3 startPoint = new Vector3(0, 0, 0);
        Vector3 endPoint = new Vector3(0, 0, 0);
        private MouseState lastMouseState = new MouseState();

        //Mouse position calculations
        private Vector3 nearSourceVector = new Vector3(0f, 0f, 0f);
        private Vector3 farSourceVector = new Vector3(0f, 0f, 1f);
        private Ray ray = new Ray();
        private Plane p = new Plane(Vector3.Up, 0f);
        private Vector3 currentTarget = new Vector3();
        private bool drawRectangle = false;

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
            //game.selectedObjectsRepository.ClearAll();
            
            foreach (Unit selectableUnit in game.selectedObjectsRepository.getRepo())
            {
                if (!selectableUnit.isMoving)
                {
                        selectableUnit.IsSelected = false;
                        game.selectedObjectsRepository.RemoveFromRepo(selectableUnit);
                }
            }
            
        }
        private Vector3 FindWhereClicked()
        {
            GraphicsDevice graphicsDevice = game.GraphicsDevice;
            MouseState mouseState = Mouse.GetState();

            nearSourceVector.X = (float)mouseState.X;
            nearSourceVector.Y = (float)mouseState.Y;

            farSourceVector.X = (float)mouseState.X;
            farSourceVector.Y = (float)mouseState.Y;
            Vector3 nearPoint = graphicsDevice.Viewport.Unproject(nearSourceVector, game.camera.Projection, game.camera.View, Matrix.Identity);
            Vector3 farPoint = graphicsDevice.Viewport.Unproject(farSourceVector, game.camera.Projection, game.camera.View, Matrix.Identity);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();

            ray.Position = nearPoint;
            ray.Direction = direction;

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
        public void singleClickSelect(Vector3 targetPosition)
        {
            foreach(GameObject go in game.gameObjectsRepository.getRepo())
            {
                if (go is Unit)
                {
                    Unit selectableUnit = go as Unit;

                    if (Vector3.Distance(targetPosition, selectableUnit.transform.translation) < 0.8f)
                        if (go.GameTag == "worker" || go.GameTag == "warrior")
                            game.selectedObjectsRepository.AddToRepo(selectableUnit);
                 }
            }

        }
        private void MouseOver(Vector3 targetPosition)
        {
            foreach (GameObject go in game.gameObjectsRepository.getRepo())
            {
                if (go is Unit)
                {
                    Unit targetableUnit = go as Unit;

                    if (Vector3.Distance(targetPosition, targetableUnit.transform.translation) < 0.8f)
                    {
                        targetableUnit.IsTargeted = true;
                    }
                    else
                        targetableUnit.IsTargeted = false;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            currentTarget = FindWhereClicked();

            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (lastMouseState.LeftButton == ButtonState.Released)
                {
                    DeselectAll();
                    singleClickSelect(currentTarget);
                }
                else
                {
                    if (!isFirstPointSelected)
                    {
                        startPoint = currentTarget;
                        isFirstPointSelected = true;
                    }
                    else
                    {
                        endPoint = currentTarget;
                        ChangeSelectedObjectsState(startPoint, endPoint);
                        drawRectangle = true;
                    }
                }
            }
            else
            {
                drawRectangle = false;
                isFirstPointSelected = false;
                MouseOver(currentTarget);
            }
            lastMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            game.GraphicsDevice.BlendState = BlendState.Additive;
            if(drawRectangle)
                DrawRect(startPoint, endPoint);
            game.GraphicsDevice.BlendState = BlendState.Opaque;
        }
    }
}