using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.GameObjectComponents
{
    class Enemy : GameObject
    {
        float hp;
        public Enemy(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag, float hp) : base(path, game1, translation, rotationAngleX, rotationAngleY, rotationAngleZ, scale, isMovable, tag)
        {
            this.hp = hp;
        }

        override public void TakeDmg(float dmg)
        {
            hp -= dmg;
        }

        override public void update(float deltatime, SoundEffect effect)
        {
            moveObject.Move(deltatime, effect);
        }
    }
}
