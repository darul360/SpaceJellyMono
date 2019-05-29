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

        private Dictionary<string, GameObject> sceneObjects;
        public Dictionary<string, GameObject> SceneObjects { get { return sceneObjects; }}
        public void AddSceneObject(string objectId, GameObject objectToAdd)
        {
            objectToAdd.parentTransform = rootTransform;
            sceneObjects.Add(objectId, objectToAdd);
        }

        public void DeleteSceneObject(String key)
        {
            sceneObjects.Remove(key);
        }

        public string FindKeyOfObject(GameObject go)
        {
            string myKey = SceneObjects.FirstOrDefault(x => x.Value == go).Key;
            return myKey;
        }

        public Scene(Camera camera, Transform rootTransform)
        {
            this.camera = camera;
            this.rootTransform = rootTransform;
            sceneObjects = new Dictionary<string, GameObject>();
        }
    }
}
