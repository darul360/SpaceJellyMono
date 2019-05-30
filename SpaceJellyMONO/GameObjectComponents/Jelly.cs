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
        float hp=20;
        float dmg=5;
        public Jelly(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag) : base(path, game1, translation, rotationAngleX,rotationAngleY, rotationAngleZ, scale, isMovable, tag)
        {
        }

        override public float GetDmg()
        {
            return dmg;
        }
        override public void TakeDmg(float dmg)
        {
            hp -= dmg;
        }
        override public float GetHp()
        {
            return hp;
        }
    }
}
