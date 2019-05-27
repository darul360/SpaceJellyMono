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

        public FindPath(int width,int height)
        {
            grid = new Grid(width, height, 1.0f);
            setBlockCell(5,5);
        }

        public void setBlockCell(int x, int y)
        {
            grid.BlockCell(new Position(x, y));

        }
        
        public void setCellCost()
        {
            grid.SetCellCost(new Position(5, 5), 300.0f);
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
