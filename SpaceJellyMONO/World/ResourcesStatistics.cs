using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.World
{
    public class ResourcesStatistics
    {
        private Game1 game1;
        public int waterStats;
        public int bluePowderStats;
        public int yellowPowderStats;
        public int redPowderStats;
        public int blackPowderStats;
        public int workers, warriors, enemies;
        public int selectedWorkers, selectedWarriors, selectedEnemies;
        public void Refresh()
        {
            int tempworkerscount = 0;
            int tempwarriorscount = 0;
            int tempenemiescount = 0;
            int tempworkersselectedcount = 0;
            int tempwarriorsselectedcount = 0;
            int tempenemiesselectedcount = 0;

            foreach (GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "worker")
                {
                    tempworkerscount++;
                }
                if (go.GameTag == "warrior")
                {
                    tempwarriorscount++;
                }
                if (go.GameTag == "enemy")
                {
                    tempenemiescount++;
                }

            }

            foreach (GameObject go in game1.selectedObjectsRepository.getRepo())
            {
                if (go.GameTag == "worker")
                {
                    tempworkersselectedcount++;
                }
                if (go.GameTag == "warrior")
                {
                    tempwarriorsselectedcount++;
                }
                if (go.GameTag == "enemy")
                {
                    tempenemiesselectedcount++;
                }

            }
            workers = tempworkerscount;
            warriors = tempwarriorscount;
            enemies = tempenemiescount;
            selectedWorkers = tempworkersselectedcount;
            selectedWarriors = tempwarriorsselectedcount;
            selectedEnemies = tempenemiesselectedcount;
        }
        public ResourcesStatistics(Game1 game1)
        {
            this.waterStats = 20;
            this.bluePowderStats = 20;
            this.yellowPowderStats = 20;
            this.redPowderStats = 0;
            this.blackPowderStats = 0;
            this.game1 = game1;
        }

    }
}
