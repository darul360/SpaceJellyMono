using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SpaceJellyMONO.GameObjectComponents
{
    class Circle : Collider
    {
        public float radius;
        public Circle(GameObject parent, float radius) : base(parent)
        {
            this.radius = radius;
            colliderTYPE = Type.circle;
        }
        public override bool Intersect(Collider other)
        {
            switch(other.colliderTYPE)
            {
                case Type.circle : return ((other.parent.transform.Translation - parent.transform.Translation).Length() < (((Circle)other).radius - radius)); break;
                default : return false;
            }
        }

        public override void DrawCollider()
        {
            cylinder = new CylinderPrimitive(GraphicsDevice);
        }
    }
}
