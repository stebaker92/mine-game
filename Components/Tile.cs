using Microsoft.Xna.Framework;
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
        public Color Color { get; private set; }
        public Rectangle SourceRectangle { get; }

        public Tile(char c)
        {
            Color = Color.White;

            switch (c)
            {
                case '+':
                    TileType = TileType.Wall;
                    break;

                case '#':
                    TileType = TileType.Floor;
                    Color = Color.Gray;
                    break;

                case '^':
                    TileType = TileType.Mine;
                    Color = Color.Gray;
                    break;

                case 'E':
                    TileType = TileType.Exit;
                    break;

                case 'R':
                    TileType = TileType.Start;
                    Color = Color.Gray;
                    break;

                default:
                    throw new NotSupportedException("Unsupported map character: " + c);
            }
        }

        internal void Explode()
        {
            TileType = TileType.MineExploded;
            Color = Color.Red;
        }

        public Rectangle GetSourceRectangle()
        {
            int spriteAtlasTileWidth = 16;
            int spriteAtlasTileHeight = 16;
            
            int spriteAtlasRow;
            int spriteAtlasColumn;
            
            switch (TileType)
            {
                case TileType.Wall:
                    spriteAtlasColumn = 0;
                    spriteAtlasRow = 1;
                    break;
                case TileType.Start:
                case TileType.Floor:
                case TileType.Mine:
                default:
                    spriteAtlasRow = 0;
                    spriteAtlasColumn = 0;
                    break;
                case TileType.Exit:
                    spriteAtlasRow = 1;
                    spriteAtlasColumn = 1;
                    break;
                case TileType.MineExploded:
                    spriteAtlasRow = 1;
                    spriteAtlasColumn = 2;
                    break;
            }

            return new Rectangle(spriteAtlasTileWidth * spriteAtlasColumn, spriteAtlasRow * spriteAtlasTileHeight, spriteAtlasTileWidth, spriteAtlasTileHeight);
        }
    }
}
