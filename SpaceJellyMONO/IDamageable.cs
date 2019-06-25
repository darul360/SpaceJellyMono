using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public interface IDamageable
    {
        //Logika otrzymywania obrażeń
        float CurrentHealth { get; set; }
        void TakeDamage(float damage);
        void Die();

        //Rysowanie paska zdrowia
        Texture2D HealthBarTexture { get; set; }
        Rectangle HealthBar { get; set; }
    }
}
