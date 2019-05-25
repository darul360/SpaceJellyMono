using Microsoft.Xna.Framework;
using SpaceJellyMONO.GameObjectComponents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.Collision
{
    class CirclePath : Collider
    {
        float radius;
        Vector3 position;
        public CylinderPrimitive cylinder;
        public Camera camera;

        public CirclePath(GameObject parent, float radius, Vector3 position) : base(parent)
        {
            this.radius = radius;
            colliderTYPE = GameObjectComponents.Type.circlePath;
            this.position = position;
        }

        public override bool Intersect(Collider other)
        {
            switch (other.colliderTYPE)
            {
                case GameObjectComponents.Type.circle: return ((other.parent.transform.Translation - position).Length() < (((Circle)other).radius + radius)); break;
                default: return false; break;
            }
        }
        public void setCam(Camera camera)
        {
            this.camera = camera;
        }
        public Matrix world(Vector3 vec)
        {
            return Matrix.CreateScale(1.0f) * Matrix.CreateTranslation(vec);
        }

    }
}
