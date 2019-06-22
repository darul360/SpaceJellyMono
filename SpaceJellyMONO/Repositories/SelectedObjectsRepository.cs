using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceJellyMONO.Units;

namespace SpaceJellyMONO.Repositories
{
    public class SelectedObjectsRepository
    {
        List<Unit> repository = new List<Unit>();

        public SelectedObjectsRepository() { }
        public void AddToRepo(Unit modelLoader)
        {
            if(!repository.Contains(modelLoader))
            {
                modelLoader.IsSelected = true;
                repository.Add(modelLoader);
            }
        }
        public void ClearAll()
        {
            foreach (Unit element in repository)
            {
                element.IsSelected = false;
            }
            repository.Clear();
        }
        public void RemoveFromRepo(Unit modelLoader)
        {
            if(repository.Contains(modelLoader))
            {
                modelLoader.IsSelected = true;
                repository.Remove(modelLoader);
            }
        }
        public List<GameObject> getRepo()
        {
            return new List<GameObject>(repository);
        }
    }
}