using GADE6122_POE_Part_1.GADE6122_POE_Part_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class HealthPickupTiles : PickupTile
    {
        public HealthPickupTiles(Position position) : base(position)
        {
        }

        public override void ApplyEffect(CharacterTiles target)
        {
            target.Heal(10);
        }

        public override char Display
        {
            get { return '+'; } // You can use another symbol if preferred
        }
    }
}

