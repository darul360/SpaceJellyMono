using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.GameObjectComponents
{
    public class FloatingText
    {
        private Vector3 position;
        private Matrix sourceWorldTransform;

        private string content;
        private Color textColor;

        private TimeSpan duration = TimeSpan.FromSeconds(2);
        private TimeSpan currentTime = TimeSpan.Zero;
        private float velocity = 0.0000001f; //text velocity per tick
        private float screenOffsetX = 0f;
        private float screenOffsetY = 0f;

        public FloatingText(Vector3 position, Matrix sourceWorldTransform, string content, Color textColor)
        {
            this.position = position;
            this.sourceWorldTransform = sourceWorldTransform;

            this.content = content;
            this.textColor = textColor;

            OutOfTime = false;
        }

        public Vector3 Position { get => position;}
        public Matrix SourceWorldTransform { get => sourceWorldTransform;}
        public string Content { get => content;}
        public Color TextColor { get => textColor;}
        public float ScreenOffsetX { get => screenOffsetX;}
        public float ScreenOffsetY { get => screenOffsetY;}
        public bool OutOfTime { get; private set; }

        public void UpdateTime(TimeSpan elapsedGameTime)
        {
            currentTime += elapsedGameTime;
            if (currentTime >= duration)
                OutOfTime = true;
            else
            {
                float offset = velocity * currentTime.Ticks;
                screenOffsetY -= offset;
            }
        }
    }
}
