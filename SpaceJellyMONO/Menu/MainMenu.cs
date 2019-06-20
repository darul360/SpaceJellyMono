using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceJellyMONO.Menu
{
    class Button
    {
        Texture2D texture2D;
        Rectangle rectangle;
        Vector2 position;
        Color color = new Color(255, 255, 255, 255);

        public Button(Texture2D texture2D,int xPos,int yPos)
        {
            this.texture2D = texture2D;
            position = new Vector2(xPos, yPos);
        }

        bool down;
        public bool isClicked;

        public void Update(MouseState mouseState)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 200, 100);
            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3;
                else color.A -= 3;
                if (mouseState.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if(color.A <255)
            {
                color.A += 3;
                isClicked = false;
            }
        }

        public void setPosition(Vector2 newPos)
        {
            position = newPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, rectangle, color);
        }
    }
}
