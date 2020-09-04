using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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
        public TileType TileType { get; set; }

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

        public string GetTextureName()
        {
            return "minesweeper" ?? TileType.ToString();
        }


    }
}
