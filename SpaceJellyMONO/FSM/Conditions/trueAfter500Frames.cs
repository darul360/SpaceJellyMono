using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.FSM.Trans
{
    public class TrueAfter100Frames
    {
        int frameCounter = 0;

        public bool ChangeState()
        {
            //Console.WriteLine(frameCounter);
            frameCounter++;
            if (frameCounter > 100)
            {
                frameCounter = 0;
                return true;
            }
            return false;
        }
    }
}
