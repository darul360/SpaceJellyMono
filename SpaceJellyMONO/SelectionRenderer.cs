using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceJellyMONO
{
    public class SelectionRenderer //rysowanie zaznaczenia, prototyp
    {
        private Effect selectionEffect;
        public Effect SelectionEffect { get { return selectionEffect; } }

        public SelectionRenderer(Game game)
        {
            selectionEffect = game.Content.Load<Effect>("custom_effects/Projection");
        }
        public Matrix WorldMatrix { set { selectionEffect.Parameters["worldMatrix"].SetValue(value); } }
        public Matrix ViewMatrix { set { selectionEffect.Parameters["viewMatrix"].SetValue(value); } }
        public Matrix ProjectionMatrix { set { selectionEffect.Parameters["projectionMatrix"].SetValue(value); } }

        public Matrix ViewMatrix2 { set { selectionEffect.Parameters["viewMatrix2"].SetValue(value); } }
        public Matrix ProjectionMatrix2 { set { selectionEffect.Parameters["projectionMatrix2"].SetValue(value); } }

        public Vector4 ProjectionColorMultiplier { set { selectionEffect.Parameters["projectionColorMultiplier"].SetValue(value); } }

        public void Draw(Scene scene)
        {
            foreach(GameObject gameObject in scene?.SceneObjects.Values)
            {
                if(gameObject.isObjectSelected)
                {
                    float posX;
                    float posY;
                    float posZ;
                    gameObject.WorldTransform.Translation.Deconstruct(out posX, out posY, out posZ);

                    ViewMatrix2 = Matrix.CreateLookAt(new Vector3(posX, posY + 3f, posZ), new Vector3(posX, 0f, posZ), Vector3.Up);

                    scene.Floor.Draw(selectionEffect);
                }
            }
        }
    }
}
