using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    public interface ITargetable
    {
        bool IsTargeted { get; set; }
        SelectionCircle TargetCircle { get;}
    }
}
