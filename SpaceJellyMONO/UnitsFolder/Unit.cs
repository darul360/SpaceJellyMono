using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.Units
{
    public class Unit : GameObject, ISelectable, IDamageable
    {
        public virtual Texture2D HealthBarTexture { get; set; }
        public virtual Rectangle HealthBar { get; set; }
        public virtual float CurrentHealth { get; set; }

        private bool isSelected = false;
        public bool IsSelected { get { return isSelected; } set { isSelected = value;} }
        public SelectionCircle SelectionCircle { get; set; }

        public Unit(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag, float colSize) : base(path, game1, translation, rotationAngleX, rotationAngleY, rotationAngleZ, scale, isMovable, tag,colSize)
        {
        }

        override public void TakeDmg(float dmg) { }
        override public float GetDmg() { return 0; }
        override public float GetHp() { return 0; }

        //public void Update()
        //{
        //    rectangle = new Rectangle((int)transform.translation.X, (int)transform.translation.Y, texture.Width, texture.Height);
        //}

        public override  void Draw(GameTime gameTime)
        {
            if(SelectionCircle != null)
                if (IsSelected)
                {
                    SelectionCircle.WorldMatrix = Matrix.CreateTranslation(WorldTransform.Translation);
                    SelectionCircle.ViewMatrix = camera.View;
                    SelectionCircle.ProjectionMatrix = camera.Projection;

                    SelectionCircle.Draw();
                }
                

            base.Draw(gameTime);
        }
        public virtual void TakeDamage(float damage)
        {

        }
        public virtual void Die()
        {

        }
    }
}
