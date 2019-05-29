using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace SpaceJellyMONO.GameObjectComponents
{
    class Circle : Collider
    {
        private GraphicsDevice deviceManager;
        public CylinderPrimitive cylinder;
        public float radius;
        public Camera cam;
        public Circle(GameObject parent, float radius) : base(parent)
        {
            this.radius = radius;
            colliderTYPE = Type.circle;
            deviceManager = parent.mainClass.GraphicsDevice;
            cylinder = new CylinderPrimitive(deviceManager, 0.5f , radius*2, 20);
            cam = parent.camera;
        }

        public override bool Intersect(Collider other)
        {
            switch(other.colliderTYPE)
            {
                case Type.circle : return ((other.parent.transform.Translation - parent.transform.Translation).Length() < (((Circle)other).radius + radius)); break;
                default : return false; break;
            }
        }

        public override void DrawCollider()
        {
            Matrix rotation = Matrix.CreateRotationX(0) * Matrix.CreateRotationY(0) * Matrix.CreateRotationZ(0);
            Matrix temp = rotation*Matrix.CreateTranslation(parent.transform.World().Translation);
            if (parent.isObjectSelected)
                cylinder.Draw(temp, cam.View, cam.Projection, new Color(255, 0, 0));
            else
                cylinder.Draw(temp, cam.View, cam.Projection, new Color(0, 0, 255));
        }
    }
}
