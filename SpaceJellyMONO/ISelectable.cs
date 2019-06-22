using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO
{
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        SelectionCircle SelectionCircle { get; set; }
    }
}
