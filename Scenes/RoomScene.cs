using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace MineGame
{
    public class RoomScene
    {
        private ContentManager content;
        public RoomScene(ContentManager content)
        {
            this.content = content;
        }

        public Tile[,] Tiles { get; private set; }
        public Vector2 StartPosition;

        public void LoadMap()
        {
            var lines = File.ReadAllLines("map.txt");

            Tiles = new Tile[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    Tiles[x, y] = new Tile(lines[y][x]);

                    if (Tiles[x, y].TileType == TileType.Start)
                    {
                        StartPosition = new Vector2(x, y);
                    }
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

#pragma warning disable CS0618 // Type or member is obsolete

                    var position = GetPositionAbsolute(x, y);

                    sb.Draw(content.Load<Texture2D>(tile.TextureName),
                            position,
                            sourceRectangle: tile.GetSourceRectangle(),
                            color: tile.Color,
                            scale: new Vector2(Constants.Zoom, Constants.Zoom));

#pragma warning restore CS0618 // Type or member is obsolete
                }
            }
        }

        internal bool TileIsWalkable(int x, int y)
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
