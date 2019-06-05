using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.World
{
    public class ResourcesStatistics
    {
        public int waterStats;
        public int bluePowderStats;
        public int yellowPowderStats;
        public int redPowderStats;
        public int blackPowderStats;

        public ResourcesStatistics()
        {
            this.waterStats = 10;
            this.bluePowderStats = 10;
            this.yellowPowderStats = 10;
            this.redPowderStats = 0;
            this.blackPowderStats = 0;
        }

    }
}
