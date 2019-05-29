using Microsoft.Xna.Framework;
namespace SpaceJellyMONO.GameObjectComponents
{
    public enum Type {circle, plane, rectangle, circlePath};
    public class Collider
    {
        public GameObject parent;

        public Type colliderTYPE;

        public Collider(GameObject parent)
        {
            this.parent = parent;
        }

        public virtual bool Intersect(Collider other) { return false; }
        public virtual void DrawCollider() {
        }
    }
}