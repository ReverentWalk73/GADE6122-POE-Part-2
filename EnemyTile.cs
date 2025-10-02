using GADE6122_POE_Part_1.GADE6122_POE_Part_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal abstract class EnemyTile : CharacterTiles 
    {
        // Constructor 
        public EnemyTile(Position position, int hitPoints, int attackPower) : base(position, hitPoints, attackPower)
        { }

        /* Abstract method GetMove
         * returns bool with a out parameter type relative to tile type.
         */
        public abstract bool GetMove(out Tile tile);

        /*Abstract method GetTargets
         * returns CharacterTile array
         */
        public abstract CharacterTiles[] GetTargets();
    }
}
