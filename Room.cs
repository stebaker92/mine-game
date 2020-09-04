using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;

namespace MineGame
{
    public class Room
    {
        private ContentManager content;

        public Room(ContentManager content)
        {
            this.content = content;
        }

        public Tile[,] Tiles { get; private set; }

        public Vector2 StartPosition;

        public void LoadLevelOne()
        {
            var lines = File.ReadAllLines("map.txt");

            Tiles = new Tile[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    Tiles[x, y] = new Tile(lines[y][x]);

                    if (Tiles[x, y].TileType == TileType.Start) StartPosition = new Vector2(x, y);

                }
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int y = 0; y < Tiles.GetLength(1); y++)
            {
                for (int x = 0; x < Tiles.GetLength(0); x++)
                {
                    var tile = Tiles[x, y];

                    int width = 16;
                    int column = 0;
                    int height = 16;
                    int ssrow = 3;
                    Rectangle sourceRectangle = new Rectangle(width * column, 40, width, height);

                    if (tile.TileType == TileType.Wall)
                    {
#pragma warning disable CS0618 // Type or member is obsolete
                        sb.Draw(content.Load<Texture2D>(tile.GetTextureName()),
                            GetPositionAbsolute(x, y),
                            sourceRectangle: sourceRectangle,
                            color: Color.White,
                            scale: new Vector2(Constants.Zoom, Constants.Zoom));
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                    else if (tile.TileType == TileType.Floor
                        || tile.TileType == TileType.Start
                        || tile.TileType == TileType.Mine)
                    {
                        sourceRectangle = new Rectangle(width * column, 24, width, height);

#pragma warning disable CS0618 // Type or member is obsolete
                        sb.Draw(content.Load<Texture2D>(tile.GetTextureName()),
                            GetPositionAbsolute(x, y),
                            sourceRectangle: sourceRectangle,
                            color: Color.Gray,
                            scale: new Vector2(Constants.Zoom, Constants.Zoom));
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                    else if (tile.TileType == TileType.MineExploded)
                    {
                        column = 2;
                        sourceRectangle = new Rectangle(width * column, 40, width, height);

#pragma warning disable CS0618 // Type or member is obsolete
                        sb.Draw(content.Load<Texture2D>(tile.GetTextureName()),
                            GetPositionAbsolute(x, y),
                            sourceRectangle: sourceRectangle,
                            color: Color.Gray,
                            scale: new Vector2(Constants.Zoom, Constants.Zoom));
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                    else if (tile.TileType == TileType.Exit)
                    {
                        sourceRectangle = new Rectangle(width * (column + 1), 40, width, height);

#pragma warning disable CS0618 // Type or member is obsolete
                        sb.Draw(content.Load<Texture2D>(tile.GetTextureName()),
                            GetPositionAbsolute(x, y),
                            sourceRectangle: sourceRectangle,
                            color: Color.White,
                            scale: new Vector2(Constants.Zoom, Constants.Zoom));
#pragma warning restore CS0618 // Type or member is obsolete
                    }
                }

            }
        }

        internal bool CanMoveToTile(int x, int y)
        {
            return Tiles[x, y].TileType != TileType.Wall;
        }

        private Vector2 GetPositionAbsolute(int x, int y)
        {
            const int spriteSize = 16;
            return new Vector2(x * spriteSize * Constants.Zoom, y * spriteSize * Constants.Zoom);
        }
    }
}
