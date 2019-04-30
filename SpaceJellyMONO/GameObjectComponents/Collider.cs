using Microsoft.Xna.Framework;
namespace SpaceJellyMONO.GameObjectComponents
{

    public class Collider
    {
        public BoundingBox box;
        private GameObject modelLoader;
        private DrawBoxCollider drawBoxCollider;
        private Vector3 translation;
        private Vector3[] veticies = new Vector3[8];
        private float size;


        public Collider(GameObject modelLoader,float size)
        {
            this.modelLoader = modelLoader;
            this.size = size;
            this.drawBoxCollider = new DrawBoxCollider(modelLoader.mainClass.GraphicsDevice, modelLoader.mainClass);
        }

        public void DrawBoxCollider()
        {
            this.translation = this.modelLoader.transform.Translation;
            this.box = new BoundingBox(new Vector3(translation.X - size / 2, translation.Y, translation.Z - size / 2), new Vector3(translation.X + size / 2, translation.Y + size, translation.Z + size / 2));
            this.veticies = this.box.GetCorners();
            this.drawBoxCollider.Draw(modelLoader.camera, box.GetCorners());
        }

    }
}
