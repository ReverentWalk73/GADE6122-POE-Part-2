using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    namespace GADE6122_POE_Part_1
    {
        abstract class CharacterTiles : Tile
        {
            protected int hitpoints;
            protected int maxHitPoints;
            protected int attackPower;
            protected Tile[] vision = new Tile[4];
            public CharacterTiles(Position position, int hitpoints, int attackPower) : base(position)
            {
                this.hitpoints = hitpoints;
                this.maxHitPoints = hitpoints;
                this.attackPower = attackPower;
            }

            public virtual void UpdateVision(Level level)
            {
                vision[0] = level.GetTile(X, Y - 1); // up
                vision[1] = level.GetTile(X + 1, Y); // right
                vision[2] = level.GetTile(X, Y + 1); // down
                vision[3] = level.GetTile(X - 1, Y); // left
            }
            //properties
            public int HitPoints => hitpoints;
            public int MaxHitPoints => maxHitPoints;
            public int AttackPower => attackPower;
            public Tile[] Vision => vision;
            public bool IsDead => hitpoints <= 0;
            public void TakeDamage(int damage)
            {
                hitpoints -= damage;
                if (hitpoints < 0) hitpoints = 0;
            }
            public void MoveTo(Tile destination)
            {
                if (destination is null || !(destination is EmptyTile))
                    return;
            }
            public void Attack(CharacterTiles target)
            {
                target.TakeDamage(attackPower);
            }
        }
    }
}

