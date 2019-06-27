using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.Units
{
    public class Unit : GameObject, ISelectable, ITargetable, IDamageable
    {
        public virtual Texture2D HealthBarTexture { get; set; }
        public virtual Rectangle HealthBar { get; set; }
        public virtual float CurrentHealth { get; set; }

        private bool isSelected = false;
        public bool IsSelected { get { return isSelected; } set { isSelected = value;} }
        public virtual SelectionCircle SelectionCircle { get { return null; } }

        private bool isTargeted = false;
        public bool IsTargeted { get { return isTargeted; } set { isTargeted = value; } }
        public virtual SelectionCircle TargetCircle { get { return null; } }

        private Effect selectedEffect;

        public Unit(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag, float colSize) : base(path, game1, translation, rotationAngleX, rotationAngleY, rotationAngleZ, scale, isMovable, tag,colSize)
        {
            selectedEffect = game1.Content.Load<Effect>("custom_effects/SelectionCircle");
            selectedEffect.Parameters["circleTexture"].SetValue(game1.Content.Load<Texture2D>("SelectionCircleAlpha"));
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
            if (IsSelected)
                if (SelectionCircle != null)
                {
                    selectedEffect.Parameters["worldMatrix"].SetValue(Matrix.CreateTranslation(WorldTransform.Translation));
                    selectedEffect.Parameters["viewMatrix"].SetValue(camera.View);
                    selectedEffect.Parameters["projectionMatrix"].SetValue(camera.Projection);
                    selectedEffect.Parameters["tintColor"].SetValue(SelectionCircle.TintColor);

                    SelectionCircle.Draw(selectedEffect);
                }
            if (IsTargeted)
                if (TargetCircle != null)
                {
                    selectedEffect.Parameters["worldMatrix"].SetValue(Matrix.CreateTranslation(WorldTransform.Translation));
                    selectedEffect.Parameters["viewMatrix"].SetValue(camera.View);
                    selectedEffect.Parameters["projectionMatrix"].SetValue(camera.Projection);
                    selectedEffect.Parameters["tintColor"].SetValue(TargetCircle.TintColor);

                    TargetCircle.Draw(selectedEffect);
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
