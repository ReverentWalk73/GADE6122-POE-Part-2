using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6122_POE_Part_1
{
    internal class Level
    {
        private int _width;
        private int _height;
        private Tile[,] _tiles;

        internal enum TileType
        {
            Wall,
            Empty
        }

        public Level(int width, int height)
        {
            _width = width;
            _height = height;
            _tiles = new Tile[width, height];

            InitialiseTiles();
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
    }
}
