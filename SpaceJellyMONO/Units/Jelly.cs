﻿using Microsoft.Xna.Framework;
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
        Rectangle helathRectangle;
        SpriteBatch spriteBatch;
        Game1 game1;

        public Jelly(string path, Game1 game1, Vector3 translation, float rotationAngleX, float rotationAngleY, float rotationAngleZ, float scale, bool isMovable, string tag) : base(path, game1, translation, rotationAngleX,rotationAngleY, rotationAngleZ, scale, isMovable, tag)
        {
            healthTexture = game1.exportContentManager().Load<Texture2D>("Red");
            spriteBatch = new SpriteBatch(game1.GraphicsDevice);
            Vector3 tmp = game1.GraphicsDevice.Viewport.Project(Vector3.Zero, game1.camera.Projection, game1.camera.View, Matrix.CreateTranslation(translation));
            //Debug.WriteLine(tmp);
            helathRectangle = new Rectangle((int)tmp.X, (int)tmp.Y , (int)hp, 25);
            this.game1 = game1;
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
            base.Draw(gameTime);
            Vector3 tmp = game1.GraphicsDevice.Viewport.Project(Vector3.Zero, game1.camera.Projection, game1.camera.View, Matrix.CreateTranslation(transform.translation));
            helathRectangle.X = (int)tmp.X;
            helathRectangle.Y = (int)tmp.Y;
            helathRectangle.Width = (int)hp;
            spriteBatch.Begin();
            spriteBatch.Draw(healthTexture, helathRectangle, Color.White);
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            skinnedAnimationPlayer?.Update(gameTime.ElapsedGameTime, WorldTransform);
            finateSatemachine?.Update(gameTime, this);
            base.Update(gameTime);
        }
    }
}