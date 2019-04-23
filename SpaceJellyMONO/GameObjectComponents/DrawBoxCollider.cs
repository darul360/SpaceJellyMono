using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class DrawBoxCollider : GameComponent
    {
        private VertexPositionTexture[] verticies;
        private BasicEffect effect;
        private GraphicsDevice graphicsDevice;
        private Vector3[] boundingBoxVerticies;
        private Texture2D checkerboardTexture;


        public DrawBoxCollider(GraphicsDevice graphicsDevice,Game1 game) :base(game)
        {           
            this.graphicsDevice = graphicsDevice;
            checkerboardTexture = game.exportContentManager().Load<Texture2D>("diffuse3");
        }
        
        public void Initialize(Vector3[] boundingBoxVerticies)
        {
            verticies = new VertexPositionTexture[36];
            #region TOP
            verticies[0].Position = new Vector3(boundingBoxVerticies[0].X, boundingBoxVerticies[0].Y, boundingBoxVerticies[0].Z); //A
            verticies[1].Position = new Vector3(boundingBoxVerticies[4].X, boundingBoxVerticies[4].Y, boundingBoxVerticies[4].Z); //E
            verticies[2].Position = new Vector3(boundingBoxVerticies[1].X, boundingBoxVerticies[1].Y, boundingBoxVerticies[1].Z); //B

            verticies[3].Position = new Vector3(boundingBoxVerticies[4].X, boundingBoxVerticies[4].Y, boundingBoxVerticies[4].Z); //E
            verticies[4].Position = new Vector3(boundingBoxVerticies[5].X, boundingBoxVerticies[5].Y, boundingBoxVerticies[5].Z); //F
            verticies[5].Position = new Vector3(boundingBoxVerticies[1].X, boundingBoxVerticies[1].Y, boundingBoxVerticies[1].Z); //B
            #endregion

            #region BOTTOM
            verticies[6].Position = new Vector3(boundingBoxVerticies[2].X, boundingBoxVerticies[2].Y, boundingBoxVerticies[2].Z); //C
            verticies[7].Position = new Vector3(boundingBoxVerticies[6].X, boundingBoxVerticies[6].Y, boundingBoxVerticies[6].Z); //G
            verticies[8].Position = new Vector3(boundingBoxVerticies[3].X, boundingBoxVerticies[3].Y, boundingBoxVerticies[3].Z); //D

            verticies[9].Position = new Vector3(boundingBoxVerticies[6].X, boundingBoxVerticies[6].Y, boundingBoxVerticies[6].Z); //G
            verticies[10].Position = new Vector3(boundingBoxVerticies[7].X, boundingBoxVerticies[7].Y, boundingBoxVerticies[7].Z); //H
            verticies[11].Position = new Vector3(boundingBoxVerticies[3].X, boundingBoxVerticies[3].Y, boundingBoxVerticies[3].Z); //D
            #endregion

            #region FRONT
            verticies[12].Position = new Vector3(boundingBoxVerticies[0].X, boundingBoxVerticies[0].Y, boundingBoxVerticies[0].Z); //A
            verticies[13].Position = new Vector3(boundingBoxVerticies[1].X, boundingBoxVerticies[1].Y, boundingBoxVerticies[1].Z); //B
            verticies[14].Position = new Vector3(boundingBoxVerticies[3].X, boundingBoxVerticies[3].Y, boundingBoxVerticies[3].Z); //D

            verticies[15].Position = new Vector3(boundingBoxVerticies[0].X, boundingBoxVerticies[0].Y, boundingBoxVerticies[0].Z); //B
            verticies[16].Position = new Vector3(boundingBoxVerticies[1].X, boundingBoxVerticies[1].Y, boundingBoxVerticies[1].Z); //C
            verticies[17].Position = new Vector3(boundingBoxVerticies[2].X, boundingBoxVerticies[2].Y, boundingBoxVerticies[2].Z); //D
            #endregion

            #region BACK
            verticies[18].Position = new Vector3(boundingBoxVerticies[5].X, boundingBoxVerticies[5].Y, boundingBoxVerticies[5].Z); //F
            verticies[19].Position = new Vector3(boundingBoxVerticies[4].X, boundingBoxVerticies[4].Y, boundingBoxVerticies[4].Z); //E
            verticies[20].Position = new Vector3(boundingBoxVerticies[6].X, boundingBoxVerticies[6].Y, boundingBoxVerticies[6].Z); //G

            verticies[21].Position = new Vector3(boundingBoxVerticies[4].X, boundingBoxVerticies[4].Y, boundingBoxVerticies[4].Z); //E
            verticies[22].Position = new Vector3(boundingBoxVerticies[7].X, boundingBoxVerticies[7].Y, boundingBoxVerticies[7].Z); //H
            verticies[23].Position = new Vector3(boundingBoxVerticies[6].X, boundingBoxVerticies[6].Y, boundingBoxVerticies[6].Z); //G
            #endregion

            #region LEFT
            verticies[24].Position = new Vector3(boundingBoxVerticies[7].X, boundingBoxVerticies[7].Y, boundingBoxVerticies[7].Z); //H
            verticies[25].Position = new Vector3(boundingBoxVerticies[4].X, boundingBoxVerticies[4].Y, boundingBoxVerticies[4].Z); //E
            verticies[26].Position = new Vector3(boundingBoxVerticies[3].X, boundingBoxVerticies[3].Y, boundingBoxVerticies[3].Z); //D

            verticies[27].Position = new Vector3(boundingBoxVerticies[4].X, boundingBoxVerticies[4].Y, boundingBoxVerticies[4].Z); //E
            verticies[28].Position = new Vector3(boundingBoxVerticies[0].X, boundingBoxVerticies[0].Y, boundingBoxVerticies[0].Z); //A
            verticies[29].Position = new Vector3(boundingBoxVerticies[3].X, boundingBoxVerticies[3].Y, boundingBoxVerticies[3].Z); //D
            #endregion

            #region RIGHT
            verticies[30].Position = new Vector3(boundingBoxVerticies[2].X, boundingBoxVerticies[2].Y, boundingBoxVerticies[2].Z); //C
            verticies[31].Position = new Vector3(boundingBoxVerticies[1].X, boundingBoxVerticies[1].Y, boundingBoxVerticies[1].Z); //B
            verticies[32].Position = new Vector3(boundingBoxVerticies[6].X, boundingBoxVerticies[6].Y, boundingBoxVerticies[6].Z); //G

            verticies[33].Position = new Vector3(boundingBoxVerticies[1].X, boundingBoxVerticies[1].Y, boundingBoxVerticies[1].Z); //B
            verticies[34].Position = new Vector3(boundingBoxVerticies[5].X, boundingBoxVerticies[5].Y, boundingBoxVerticies[5].Z); //F
            verticies[35].Position = new Vector3(boundingBoxVerticies[6].X, boundingBoxVerticies[6].Y, boundingBoxVerticies[6].Z); //G
            #endregion

        }
        public void Draw(Camera camera, Vector3[] boundingBoxVerticies)
        {
            Initialize(boundingBoxVerticies);
            effect = new BasicEffect(graphicsDevice);
            graphicsDevice.BlendState = BlendState.AlphaBlend;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.TextureEnabled = true;
            effect.Texture = checkerboardTexture;
            effect.Alpha = 0.5f;

            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList,verticies,0,12);

            }
        }
    }
}
