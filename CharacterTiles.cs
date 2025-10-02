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
            protected Tile[] vision;
            public CharacterTiles(Position position, int hitpoints, int attackPower) : base(position)
            {
                this.hitpoints = hitpoints;
                this.maxHitPoints = hitpoints;
                this.attackPower = attackPower;
                this.vision = new Tile[4];
            }

            public void Updatevision(Level level)
            {
                var t = level._tiles;
                int w = level._width, h = level._height;
                vision[0] = (Y > 0) ? t[X, Y - 1] : null;//up
                vision[1] = (X < w - 1) ? t[X + 1, Y] : null;//right
                vision[2] = (Y < h - 1) ? t[X, Y + 1] : null;//down
                vision[3] = (X > 0) ? t[X - 1, Y] : null;//left
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
                if (hitpoints < 0)
                    hitpoints = 0;
            }
            public void Attack(CharacterTiles target)
            {
                target.TakeDamage(attackPower);
            }
            private ExitTile exitTile;
            public ExitTile ExitTile { get { return exitTile; } }
        }
    }
}

