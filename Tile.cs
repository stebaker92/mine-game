using System;

namespace MineGame
{
    public enum TileType
    {
        Wall,
        Floor,
        Mine,
        Exit,
        Start,
        MineExploded,
    }

    public class Tile
    {
        public string TextureName => "minesweeper";

        public TileType TileType { get; private set; }

        public Tile(char c)
        {
            switch (c)
            {
                case '+':
                    TileType = TileType.Wall;
                    break;

                case '#':
                    TileType = TileType.Floor;
                    break;

                case '^':
                    TileType = TileType.Mine;
                    break;

                case 'E':
                    TileType = TileType.Exit;
                    break;

                case 'R':
                    TileType = TileType.Start;
                    break;

                default:
                    throw new NotSupportedException("Unsupported map character: " + c);
            }
        }

        internal void Explode()
        {
            TileType = TileType.MineExploded;
        }
    }
}
