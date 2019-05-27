using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpaceJellyMONO
{
    public class Transform
    {
        private GameObject modelLoader;
        private float scale, yRotation,xRotation,zRotation;
        public Vector3 translation;

        /*===================================*/

        public float Scale{
            get { return scale; }
            set { scale = value; }
        }
        public float ZRotation
        {
            get { return zRotation; }
            set { zRotation = value; }
        }
        public float YRotation
        {
            get { return yRotation; }
            set { yRotation = value; }
        }
        public float XRotation
        {
            get { return xRotation; }
            set { xRotation = value; }
        }
        public Vector3 Translation
        {
            get { return translation; }
            set { translation = value; }
        }

        /*===================================*/

        public Transform(GameObject modelLoader, Vector3 translation, float xRotation,float yRotation,float zRotation, float scale)
        {
            this.Translation = translation;
            this.YRotation = yRotation;
            this.XRotation = xRotation;
            this.ZRotation = zRotation;
            this.Scale = scale;
        }
        public Transform(Vector3 translation, float xRotation, float yRotation, float zRotation, float scale)
        {
            this.Translation = translation;
            this.YRotation = yRotation;
            this.XRotation = xRotation;
            this.ZRotation = zRotation;
            this.Scale = scale;
        }
        public Matrix World()
        {
            Matrix rotation = Matrix.CreateRotationX(XRotation) * Matrix.CreateRotationY(YRotation) * Matrix.CreateRotationZ(ZRotation);
            return Matrix.CreateScale(Scale) * rotation * Matrix.CreateTranslation(Translation);
            
        }
    }
}
