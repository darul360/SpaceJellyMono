using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.Units;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.GameObjectComponents
{
    class Jelly : Unit
    {
        float hp=200;
        float dmg=5;
        //healthBar
        Texture2D healthTexture;
        Rectangle healthRectangle;

        public override float CurrentHealth { get { return hp; } set { hp = value; } }
        public override Texture2D HealthBarTexture { get { return healthTexture; } set { healthTexture = value; } }
        public override Rectangle HealthBar { get { return healthRectangle; } set { healthRectangle = value; } }

        public Jelly(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag, float colSize) : base(path, game1, translation, rotationAngleX,rotationAngleY, rotationAngleZ, scale, isMovable, tag,colSize)
        {
            healthTexture = game1.exportContentManager().Load<Texture2D>("diffuse3");
            healthRectangle = new Rectangle(0, 0, 0, 10);

            SelectionCircle = new SelectionCircle(Vector3.Zero, new Vector2(1f, 1f), game1);
            SelectionCircle.Color = Color.GreenYellow.ToVector4();
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

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);           
        }

        public override void Update(GameTime gameTime)
        {
            Vector3 position = GraphicsDevice.Viewport.Project(Vector3.Zero, camera.Projection, camera.View, WorldTransform);
            healthRectangle.X = (int)position.X - 50;
            healthRectangle.Y = (int)position.Y - 50;
            healthRectangle.Width = (int)(hp * 0.5f);

            skinnedAnimationPlayer?.Update(gameTime.ElapsedGameTime, WorldTransform);
            finateSatemachine?.Update(gameTime, this);
            base.Update(gameTime);
        }
    }
}