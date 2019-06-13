using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    public class Scene
    {
        private Transform rootTransform;

        public Transform RootTransform { get { return rootTransform; } }

        public BasicFloorGenerate Floor { get; set; }

        private Dictionary<string, GameObject> sceneObjects;
        public Dictionary<string, GameObject> SceneObjects { get { return sceneObjects; }}
        public void AddSceneObject(string objectId, GameObject objectToAdd)
        {
            objectToAdd.ParentTransform = rootTransform;
            sceneObjects.Add(objectId, objectToAdd);
        }
        public void AddSceneObject(string objectId, string parentId, GameObject objectToAdd)
        {
            objectToAdd.ParentTransform = SceneObjects[parentId].transform;
            sceneObjects.Add(objectId, objectToAdd);
        }

        public Scene(Transform rootTransform)
        {
            this.rootTransform = rootTransform;
            sceneObjects = new Dictionary<string, GameObject>();
        }
    }
}
