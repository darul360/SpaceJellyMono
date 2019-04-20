using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceJellyMONO
{
    public class GameObjectsRepository
    {
        List<ModelLoader> repository = new List<ModelLoader>();

        public GameObjectsRepository() { }
        public void AddToRepo(ModelLoader modelLoader) { repository.Add(modelLoader); }
        public void RemoveFromRepo(ModelLoader modelLoader) { repository.Remove(modelLoader); }
        public List<ModelLoader> getRepo() { return repository; }
        
    }
}
