using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public interface ISystem : IDisposable
    {
         void Initialize(World world);
    }
}
