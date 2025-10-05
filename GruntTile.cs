using GADE6122_POE_Part_1.GADE6122_POE_Part_1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class GruntTile : EnemyTile
    {
        // private Tile[] Vision { get; } 

        // Implementing a GruntTile class with parameter position, while being passed to base class.
        private static Random random = new Random();
        public GruntTile(Position position) : base(position, 10, 1)
        {

        }

        public override char Display
        {
            get
            {
                return IsDead ? 'X' : '\u0394';
            }
        }

        // Overriding boolean GetMove
        public override bool GetMove(out Tile tile)
        {
            var emptyTiles = Vision.Where(t => t is EmptyTile).ToList();
            if (emptyTiles.Count > 0)
            {
                // Random empty tile
                tile = emptyTiles[random.Next(emptyTiles.Count)];
                return true;
            }
            tile = null;
            return false;
        }

        // overriding the CharacterTiles[]
        public override CharacterTiles[] GetTargets()
        {
            var hero = Vision.OfType<CharacterTiles>().FirstOrDefault(t => t.GetType().Name is "HeroTile");
            if (hero != null)
            {
                return new CharacterTiles[] { hero };
            }
            return new CharacterTiles[0];
        }

        public override void UpdateVision(Level level)
        {
            SetLevel(level);
            base.UpdateVision(level);
        }
    }
}
    

