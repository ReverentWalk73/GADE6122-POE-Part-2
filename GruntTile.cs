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
        // Implementing a GruntTile class with parameter position, while being passed to base class.
        public GruntTile(Position position) : base(position, 10, 1) 
        {

        }
        public override char Display
        {
            get
            {
                return IsDead ? 'X' : 'G';
            }
        }
        // Overriding boolean GetMove
        public override bool GetMove(out Tile tile)
        {
            var emptyTiles = Vision.OfType<CharacterTiles>().ToList();
            if (emptyTiles.Count > 0)
            {
                // Random empty tile
                var random = new Random();
                tile = emptyTiles[random.Next(emptyTiles.Count)];
                return true;
            }
            tile = null;
            return false;
        }
        // overriding the CharacterTiles[]
        public override CharacterTiles[] GetTargets()
        {
            var hero = Vision.OfType<CharacterTiles>().FirstOrDefault(t => t.GetType().Name == "HeroTile");
            if (hero != null)
            {
                return new[] { hero };
            }
            return new CharacterTiles[0];
        }
    }
}
