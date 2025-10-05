using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
     public abstract class Tile
    {
        public Position _position {  get; set; }
        public int X => _position.x;
        public int Y => _position.y;
        public Tile(Position position)
        {
            _position = position;
        }
        public abstract char Display { get; }
    }
}
