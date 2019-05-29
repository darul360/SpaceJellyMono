using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.GameObjectComponents
{
    class Jelly : GameObject
    {
        float hp;
        float dmg;
        public Jelly(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag, float hp) : base(path, game1, translation, rotationAngleX,rotationAngleY, rotationAngleZ, scale, isMovable, tag)
        {
            this.hp = hp;
        }

        override public float GetDmg()
        {
            return dmg;
        }
    }
}
