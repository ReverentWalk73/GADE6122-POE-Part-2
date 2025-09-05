using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class ExitTile : Tile
    {
        public ExitTile(Position position) : base(position)
        {
        }
        public override char Display => '\u25a6';//
    }
}
    

