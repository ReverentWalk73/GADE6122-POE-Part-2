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
        protected Level currentLevel;
        // Constructor 
        public EnemyTile(Position position, int hitPoints, int attackPower) : base(position, hitPoints, attackPower)
        { }

        // Abstract method GetMove
        public abstract bool GetMove(out Tile tile);
        
        // Abstract method GetTargets
        public abstract CharacterTiles[] GetTargets();

        public void SetLevel(Level level)
        {
            currentLevel = level;
        }
    }
}
