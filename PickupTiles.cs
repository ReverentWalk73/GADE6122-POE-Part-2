using GADE6122_POE_Part_1.GADE6122_POE_Part_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
   
        abstract class PickupTile : Tile
        {
            public PickupTile(Position position) : base(position)
            {
            }
            public abstract void ApplyEffect(CharacterTiles target);
        }
    }


