using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    public class Scene
    {
        private Camera camera;
        public Camera Camera { get { return camera; } set { camera = value; } }

        private Transform rootTransform;

        private Dictionary<int, GameObject> sceneObjects;
        public Dictionary<int, GameObject> SceneObjects { get { return SceneObjects; }}
        public void AddSceneObject(int objectId, GameObject objectToAdd)
        {
            objectToAdd.parentTransform = rootTransform;
            sceneObjects.Add(objectId, objectToAdd);
        }

        public Scene(Camera camera, Transform rootTransform)
        {
            this.camera = camera;
            this.rootTransform = rootTransform;
        }
    }
}
