using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinder
{
    /// <summary>
    /// A snap of a single Cell on the Map.  May upgrade to a QuadTree variant later.
    /// </summary>
    class Node
    {
        public int X;
        public int Y;
        public bool isValid;
        public Node prev;

        public int Fscore = 0;
        public int Gscore = 0;
        public int Hscore = 0;

        public Node(int x, int y, bool valid)
        {
            this.X = x;
            this.Y = y;
            this.isValid = valid;
        }
    }
}
