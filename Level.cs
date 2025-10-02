using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class Level
    {
        public int _width;
        public int _height;
        public Tile[,] _tiles;
        private EnemyTile[] _enemies;
        public EnemyTile[] Enemies => _enemies;
        internal enum TileType
        {
            Wall,
            Empty,
            Hero,
            ExitTile,
            Enemy 
        }
        private HeroTile hero;
        public HeroTile Hero => hero;
        private ExitTile _exitTile;
        private HeroTile oldHero;

        public ExitTile ExitTile { get { return _exitTile; } }

        public int CurrentLevelNumber { get; }

        public Level(int width, int height,int numEnemies, HeroTile hero = null)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            _enemies = new EnemyTile[0];
            InitialiseTiles();

            // Placing hero
            if (hero == null)
            {
                Position spawn = GetRandomEmptyPosition();
                this.hero = new HeroTile(spawn);
                _tiles[spawn.x, spawn.y] = this.hero;
            }
            else
            {
                Position spawn = GetRandomEmptyPosition();
                hero._position = spawn;
                this.hero = hero;
                _tiles[spawn.x, spawn.y] = this.hero;
            }
            // Placing Exit
            Position exitPos;
            do
            {
                exitPos = GetRandomEmptyPosition();
            }
            while (exitPos.x == this.hero.X && exitPos.y == this.hero.Y);

            _exitTile = new ExitTile(exitPos);
            _tiles[exitPos.x, exitPos.y] = _exitTile;

            // Initializing and placing enemies
            _enemies = new EnemyTile[numEnemies];
            for (int i = 0; i < numEnemies; i++)
            {
                Position enemyPos = GetRandomEmptyPosition();
                var enemy = (EnemyTile)CreateTile(TileType.Enemy, enemyPos);
                _enemies[i] = enemy;
            }
            UpdateVision();
        }

        public Level(int width, int height, HeroTile oldHero)
        {
            _width = width;
            _height = height;
            this.oldHero = oldHero;
        }

        public Level(int currentLevelNumber)
        {
            CurrentLevelNumber = currentLevelNumber;
        }

        private Tile CreateTile(TileType type, Position position)
        {
            Tile tile;
            switch (type)
            {
                case TileType.Wall:
                    tile = new WallTile(position);
                    break;
                case TileType.Empty:
                    tile = new EmptyTile(position);
                    break;
                case TileType.Hero:
                    tile = new HeroTile(position);
                    break;
                case TileType.ExitTile:
                    tile = new ExitTile(position);
                    break;
                case TileType.Enemy:
                    tile = new GruntTile(position);// Initiating the grunt tile
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(type), $" Unsupported TileType: {type}");
            }

            _tiles[position.x, position.y] = tile;
            return tile;
        }
        private void InitialiseTiles()
        {
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                {
                    bool isBoundry = x == 0 || y == 0 || x == _width - 1 || y == _height - 1;
                    CreateTile(isBoundry ? TileType.Wall : TileType.Empty, new Position(x, y));
                }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    sb.Append(_tiles[x, y].Display);
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
        private static readonly Random random = new Random();
        private Position GetRandomEmptyPosition()
        {
            Position position;
            do
            {
                int x = random.Next(1, _width - 1);
                int y = random.Next(1, _height - 1);
                position = new Position(x, y);
            } while (!(_tiles[position.x, position.y] is EmptyTile));
            return position;
        }
        public void SwopTiles(Tile a, Tile b)
        {
            Position temp = (Position)a._position;
            a._position = b._position;
            b._position = temp;

            _tiles[a.X, a.Y] = a;
            _tiles[b.X, b.Y] = b;

            if (a is HeroTile) hero = (HeroTile)a;
            if (b is HeroTile) hero = (HeroTile)b;
        }
            public void UpdateVision()
            {
                hero.Updatevision(this);
                foreach (var enemy in _enemies)
                {
                    enemy.Updatevision(this);
                }
            }
    }
}