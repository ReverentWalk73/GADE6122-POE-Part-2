using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class Level
    {
        public int _width;
        public int _height;
        public Tile[,] _tiles;



        internal enum TileType
        {
            Wall,
            Empty,
            hero,
            exitTile
        }

        public Level(int width, int height, HeroTile hero = null)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];
            InitialiseTiles();

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
            this.hero.Updatevision(this);

            Position exitPos;
            do
            {
                exitPos = GetRandomEmptyPosition();
            }
            while (exitPos.x == this.hero.X && exitPos.y == this.hero.Y);

            _exitTile = new ExitTile(exitPos);
            _tiles[exitPos.x, exitPos.y] = _exitTile;
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
                case TileType.hero:
                    tile = new HeroTile(position);
                    break;
                case TileType.exitTile:
                    tile = new ExitTile(position);
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(type), $" Unsupported TileType: {type}");
            }

            _tiles[position.x, position.y] = tile;
            return tile;
        }
        private ExitTile _exitTile;
        public ExitTile ExitTile { get { return _exitTile; } }

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
        private HeroTile hero;
        public HeroTile Hero => hero;

        private Position GetRandomEmptyPosition()
        {

            Random random = new Random();
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
    }
}
