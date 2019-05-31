using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.Repositories
{
    public class PowderSourcesRepository
    {
        List<GameObject> repository = new List<GameObject>();

        public PowderSourcesRepository() { }
        public void AddToRepo(GameObject modelLoader) { repository.Add(modelLoader); }
        public void RemoveFromRepo(GameObject modelLoader) { repository.Remove(modelLoader); }
        public List<GameObject> getRepo() { return repository; }
    }
}
