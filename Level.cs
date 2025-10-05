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
        private HeroTile hero;
        private HeroTile oldHero;
        public HeroTile Hero => hero;
        private ExitTile _exitTile;
        private static readonly Random random = new Random();
        public EnemyTile[] Enemies => _enemies;
        public ExitTile ExitTile { get { return _exitTile; } }
        public int CurrentLevelNumber { get; }

        internal enum TileType
        {
            Wall,
            Empty,
            Hero,
            ExitTile,
            Enemy 
        }
        public Level(int width, int height,int numEnemies, HeroTile hero = null)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            _enemies = new EnemyTile[0];
            InitializeTiles();

            // Placing hero
            placeHero(hero);
            // Placing exit
            placeExit();
            // Placing enemies
            placeEnemies(numEnemies);
            // Updating vision
            UpdateVision();
        }

        public Level(int width, int height, EnemyTile[] oldEnemies, HeroTile oldHero)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            InitializeTiles();

            // place hero
            Position heroPos = GetRandomEmptyPosition();
            oldHero._position = heroPos;
            hero = oldHero;
            _tiles[heroPos.x, heroPos.y] = hero;

            Position exitPos;
            do
            {
                exitPos = GetRandomEmptyPosition();
            } while (exitPos.x == hero.X && exitPos.y == hero.Y);
            _exitTile = new ExitTile(exitPos);
            _tiles[exitPos.x, exitPos.y] = _exitTile;

            _enemies = new EnemyTile[oldEnemies.Length];
            for (int i = 0; i < oldEnemies.Length; i++)
            {
                Position enemyPos = GetRandomEmptyPosition();
                oldEnemies[i]._position = enemyPos;
                _enemies[i] = oldEnemies[i];
                _tiles[enemyPos.x, enemyPos.y] = oldEnemies[i];
            }
            UpdateVision();
        }

        private void InitializeTiles()
        {
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                {
                    bool isBoundary = x == 0 || y == 0 || x == _width - 1 || y == _height - 1;
                    CreateTile(isBoundary ? TileType.Wall : TileType.Empty, new Position(x, y));
                }
        }

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

        // Hero placement 
        private void placeHero(HeroTile existingHero = null)
        {
            Position spawn = GetRandomEmptyPosition();
            if (existingHero == null)
            {
                hero = new HeroTile(spawn);
            }
            else
            {
                existingHero._position = spawn;
                hero = existingHero;
            }
            _tiles[spawn.x, spawn.y] = hero;
        }

        // Exit placement
        private void placeExit()
        {
            Position exitPos;
            do
            {
                exitPos = GetRandomEmptyPosition();
            }
            while (exitPos.x == hero.X && exitPos.y == hero.Y);

            _exitTile = new ExitTile(exitPos);
            _tiles[exitPos.x, exitPos.y] = _exitTile;
        }

        // Enemy placement
        private void placeEnemies(int numEnemies)
        {
            _enemies = new EnemyTile[numEnemies];
            for (int i = 0; i < numEnemies; i++)
            {
                Position enemyPos = GetRandomEmptyPosition();
                var enemy = new GruntTile(enemyPos);
                _enemies[i] = enemy;
                _tiles[enemyPos.x, enemyPos.y] = enemy;
            }
        }

        public void SwopTiles(Tile a, Tile b)
        {
            var tempPos = a._position;
            a._position = b._position;
            b._position = tempPos;

            _tiles[a.X, a.Y] = a;
            _tiles[b.X, b.Y] = b;

            if (a is HeroTile) hero = (HeroTile)a;
            if (b is HeroTile) hero = (HeroTile)b;
        }

        // Level set up
        public Level(int currentLevelNumber)
        {
            CurrentLevelNumber = currentLevelNumber;
            _width = 10; 
            _height = 10; 
            _tiles = new Tile[_width, _height];
            InitializeTiles();
        }
       
        public void UpdateVision()
        {
            hero.UpdateVision(this);
            foreach (var enemy in _enemies)
            {
                enemy.UpdateVision(this);
            }
        }

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && x < _width && y >= 0 && y < _height)
                return _tiles[x, y];
            return null;
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

        // Creating and placing tiles
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
    }
}