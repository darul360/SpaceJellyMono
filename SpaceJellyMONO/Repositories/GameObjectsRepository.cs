using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceJellyMONO
{
    public class GameObjectsRepository
    {
        List<GameObject> repository = new List<GameObject>();

        public GameObjectsRepository() { }
        public void AddToRepo(GameObject modelLoader) { repository.Add(modelLoader); }
        public void RemoveFromRepo(GameObject modelLoader) { repository.Remove(modelLoader); }
        public List<GameObject> getRepo() { return repository; }
        
    }
}