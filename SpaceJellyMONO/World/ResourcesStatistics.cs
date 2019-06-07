﻿using System;
using System.Collections.Generic;
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

        public void Refresh()
        {
            int tempworkerscount = 0;
            int tempwarriorscount = 0;
            int tempenemiescount = 0;

            foreach(GameObject go in game1.gameObjectsRepository.getRepo())
            {
                if (go.GameTag == "worker") tempworkerscount++;
                if (go.GameTag == "warrior") tempwarriorscount++;
                if (go.GameTag == "enemy") tempenemiescount++;
            }
            workers = tempworkerscount;
            warriors = tempwarriorscount;
            enemies = tempenemiescount;
        }
        public ResourcesStatistics(Game1 game1)
        {
            this.waterStats = 10;
            this.bluePowderStats = 10;
            this.yellowPowderStats = 10;
            this.redPowderStats = 0;
            this.blackPowderStats = 0;
            this.game1 = game1;
        }

    }
}
