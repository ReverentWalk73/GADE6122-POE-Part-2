using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal abstract class Tile
    {
        private Position _position;
        public Tile(Position position)
        {
            _position = position;
        }
        public int X => _position.x;
        public int Y => _position.y;

        public abstract char Display { get; }
    }
}
