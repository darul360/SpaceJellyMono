﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceJellyMONO.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.UnitsFolder
{
    class Warrior : Unit
    {
        float hp = 200;
        float dmg = 50;
        //healthBar
        Texture2D healthTexture;
        Rectangle helathRectangle;
        SpriteBatch spriteBatch;
        Game1 game1;

        public Warrior(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag, float colSize) : base(path, game1, translation, rotationAngleX, rotationAngleY, rotationAngleZ, scale, isMovable, tag,colSize)
        {
            healthTexture = game1.exportContentManager().Load<Texture2D>("Red");
            spriteBatch = new SpriteBatch(game1.GraphicsDevice);
            Vector3 tmp = game1.GraphicsDevice.Viewport.Project(Vector3.Zero, game1.camera.Projection, game1.camera.View, Matrix.CreateTranslation(translation));
            //Debug.WriteLine(tmp);
            helathRectangle = new Rectangle((int)tmp.X, (int)tmp.Y, (int)hp, 8);
            this.game1 = game1;

            SelectionCircle = new SelectionCircle(Vector3.Zero, new Vector2(2f, 2f), game1);
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

            ////if (hp > 0)
            ////  spriteBatch.Draw(texture, rectangle, Color.White);

            //Debug.WriteLine("dsdsdsdsds");
            
            Vector3 tmp = game1.GraphicsDevice.Viewport.Project(Vector3.Zero, game1.camera.Projection, game1.camera.View, Matrix.CreateTranslation(transform.translation));
            helathRectangle.X = (int)tmp.X - 50;
            helathRectangle.Y = (int)tmp.Y - 50;
            helathRectangle.Width = (int)hp/2;
            spriteBatch.Begin();
            spriteBatch.Draw(healthTexture, helathRectangle, Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);

        }

        public override void Update(GameTime gameTime)
        {
            skinnedAnimationPlayer?.Update(gameTime.ElapsedGameTime, WorldTransform);
            finateSatemachine?.Update(gameTime, this);
            base.Update(gameTime);
        }
    }
}
