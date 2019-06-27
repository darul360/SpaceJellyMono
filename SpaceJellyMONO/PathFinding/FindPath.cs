using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using RoyT.AStar;

namespace SpaceJellyMONO.PathFinding
{
    public class FindPath
    {
        Grid grid;
        int widht;
        int height;
        Game1 game1;

        public FindPath(int width,int height)
        {
            this.widht = width;
            this.height = height;
            grid = new Grid(width, height, 1.0f);
        }

        public void setBlockCell(int x, int y)
        {
           // grid.BlockCell(new Position(x, y));

        }
        
        public void unblockCell(int x,int y)
        {
            //grid.UnblockCell(new Position(x, y));
        }

        public float getCellCost(int x, int y)
        {
            return grid.GetCellCost(new Position(x, y));
        }

        public bool checkIfPositionIsBlocked(int i,int j)
        {
            if (grid.GetCellCost(new Position(i, j)) == float.PositiveInfinity)
            {
                return true;
            }
            return false;

        }

    
        public List<Vector2> findPath(int startX,int startZ,int stopX,int stopZ)
        {
            Position[] path = grid.GetPath(new Position(startX, startZ), new Position(stopX, stopZ));
            List<Vector2> localList = new List<Vector2>();
            foreach (Position p in path)
                localList.Add(new Vector2(p.X, p.Y));
            return localList;

        }

    
    }
}
