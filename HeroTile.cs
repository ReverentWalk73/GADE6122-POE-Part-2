using GADE6122_POE_Part_1.GADE6122_POE_Part_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    class HeroTile : CharacterTiles
    {
        public HeroTile(Position position) : base(position, 40, 5) { }
        public override char Display => IsDead ? 'X' : '▼';
    }
}
