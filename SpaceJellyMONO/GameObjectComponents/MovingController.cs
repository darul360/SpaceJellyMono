using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceJellyMONO.GameObjectComponents
{
    public class MovingController :GameComponent
    {
        Game1 game1;
        MouseState lastMouseState = new MouseState();
        List<Vector2> tempNodes;
        public Vector3 clickPos;
        public MovingController(Game1 game1) : base(game1)
        {
            this.game1 = game1;
            tempNodes = new List<Vector2>();
        }

        public void spiralSpread(int i, int j, int howManySelected)
        {
            tempNodes.Add(new Vector2(i, j));
            // (di, dj) is a vector - direction in which we move right now
            int di = 1;
            int dj = 0;
            // length of current segment
            int segment_length = 1;

            // current position (i, j) and how much of current segment we passed
            //int i = 0;
            //int j = 0;
            int segment_passed = 0;
            for (int k = 0; k < howManySelected; ++k)
            {
                // make a step, add 'direction' vector (di, dj) to current position (i, j)
                i += di;
                j += dj;
                ++segment_passed;
                tempNodes.Add(new Vector2(i, j));

                if (segment_passed == segment_length)
                {
                    // done with current segment
                    segment_passed = 0;

                    // 'rotate' directions
                    int buffer = di;
                    di = -dj;
                    dj = buffer;

                    // increase segment length if necessary
                    if (dj == 0)
                    {
                        ++segment_length;
                    }
                }
            }
        }



        public override void Update(GameTime gameTime)
        {
            if (game1.selectedObjectsRepository.getRepo().Count != 0)
            {
                MouseState currentState = Mouse.GetState();
                if(currentState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Released)
                {
                    clickPos = game1.clickCooridantes.FindWhereClicked();
                    if (clickPos.X > 0 && clickPos.X < 100 && clickPos.Z > 0 && clickPos.Z<100)
                    if (!game1.findPath.checkIfPositionIsBlocked((int)Math.Round(clickPos.X), (int)Math.Round(clickPos.Z)))
                    {
                            Debug.WriteLine(game1.selectedObjectsRepository.getRepo().Count);
                        spiralSpread((int)Math.Round(clickPos.X), (int)Math.Round(clickPos.Z), game1.selectedObjectsRepository.getRepo().Count);
                        int i = 0;
                        foreach (GameObject go in game1.selectedObjectsRepository.getRepo())
                        {
                            
                                go.targetX = (int)tempNodes[i].X;
                                go.targetY = (int)tempNodes[i].Y;
                                go.isMoving = true;
                            i++;
                        }
                    }
                }
                lastMouseState = currentState;
                tempNodes.Clear();
            }
            base.Update(gameTime);
        }

    }
}
