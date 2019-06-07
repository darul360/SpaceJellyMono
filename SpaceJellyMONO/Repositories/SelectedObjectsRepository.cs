using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.Repositories
{
    public class SelectedObjectsRepository
    {
        List<GameObject> repository = new List<GameObject>();

        public SelectedObjectsRepository() { }
        public void AddToRepo(GameObject modelLoader) { if(!ifExists(modelLoader))repository.Add(modelLoader); }
        bool ifExists(GameObject go)
        {
            if (repository.Contains(go)) return true;
            return false;
        }
        public void ClearAll() { repository.Clear(); }
        public void RemoveFromRepo(GameObject modelLoader) { repository.Remove(modelLoader); }
        public List<GameObject> getRepo() { return repository; }
    }
}