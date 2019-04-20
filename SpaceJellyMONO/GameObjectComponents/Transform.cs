using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceJellyMONO
{
    public class Transform
    {
        private ModelLoader modelLoader;
        private float scale, yRotation,xRotation,zRotation;
        private Vector3 translation;

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

        public Transform(ModelLoader modelLoader, Vector3 translation, float xRotation,float yRotation,float zRotation, float scale)
        {
            this.modelLoader = modelLoader;
            this.Translation = translation;
            this.YRotation = yRotation;
            this.XRotation = xRotation;
            this.ZRotation = zRotation;
            this.Scale = scale;
        }

        public Matrix World()
        {
             return  Matrix.CreateScale(Scale) *Matrix.CreateRotationX(XRotation)*Matrix.CreateRotationY(YRotation)*Matrix.CreateRotationZ(ZRotation) * Matrix.CreateTranslation(Translation);
        }
    }
}
