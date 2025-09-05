using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    public class Position
    {
        // Coordinates
        private int _x;
        private int _y;

        // Constructor 
        public Position(int x, int y)
        {
            _x = x;
            _y = y;
        }
        // Properties
        public int x
        {
            get => _x;
            set => _x = value;
        }

        public int y
        {
            get => _y;
            set => _y = value;
        }
    }
}
