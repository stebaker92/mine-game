using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleEngine2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineGame
{
    public class Player
    {
        private readonly ContentManager content;
        private readonly Room room;
        private KeyboardState lastState;
        private ParticleEngine particleEmitter;
        private string textureName = "robot_3Dblue";

        public Player(ContentManager content, Room room)
        {
            this.content = content;
            this.room = room;
            X = (int)room.StartPosition.X;
            Y = (int)room.StartPosition.Y;
        }

        public bool IsExploded { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public double Rotate { get; set; }

        public void Update()
        {
            particleEmitter?.Update();

            if (IsExploded)
            {
                return;
            }

            KeyboardState state = Keyboard.GetState();
            if (IsNewKeyPress(state, Keys.Left) && room.CanMoveToTile(X - 1, Y))
            {
                X--;
                Rotate = ConvertToRadians(180);
                InteractWithTile(X, Y);
            }
            if (IsNewKeyPress(state, Keys.Right) && room.CanMoveToTile(X + 1, Y))
            {
                X++;
                Rotate = ConvertToRadians(0);
                InteractWithTile(X, Y);
            }
            if (IsNewKeyPress(state, Keys.Up) && room.CanMoveToTile(X, Y - 1))
            {
                Y--;
                Rotate = ConvertToRadians(270);
                InteractWithTile(X, Y);
            }
            if (IsNewKeyPress(state, Keys.Down) && room.CanMoveToTile(X, Y + 1))
            {
                Y++;
                Rotate = ConvertToRadians(90);
                InteractWithTile(X, Y);
            }


            lastState = state;
        }

        internal void InteractWithTile(int x, int y)
        {
            var tile = room.Tiles[x, y];
            if (tile.TileType == TileType.Exit)
            {
            }
            else if (tile.TileType == TileType.Mine)
            {
                tile.TileType = TileType.MineExploded;
                GameOver();
            }
        }

        private bool IsNewKeyPress(KeyboardState state, Keys key)
        {
            return state.IsKeyDown(key) && !lastState.IsKeyDown(key);
        }
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        public void LevelSuccess()
        {
            var textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("star"));

            particleEmitter = new ParticleEngine(textures, new Vector2(600, 340), color: Color.Green, particleCount: 20);

            particleEmitter.EmitterLocation = new Vector2(X * Constants.TileSize * Constants.Zoom + 16, Y * Constants.TileSize * Constants.Zoom + 16);
        }

        public void GameOver()
        {
            IsExploded = true;

            var textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("star"));

            particleEmitter = new ParticleEngine(textures, new Vector2(400, 240), color: Color.Red);

            particleEmitter.EmitterLocation = new Vector2(X * Constants.TileSize * Constants.Zoom + 16, Y * Constants.TileSize * Constants.Zoom + 16);
        }

        public void Draw(SpriteBatch sb)
        {
            particleEmitter?.Draw(sb);

            if (IsExploded)
            {
                // TODO - fade out on death
                return;
            }

            var spriteSize = Constants.TileSize;
            var texture = content.Load<Texture2D>(textureName);
            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            sb.Draw(texture, new Vector2(X * spriteSize * Constants.Zoom + 16, Y * spriteSize * Constants.Zoom + 16), color: Color.White, scale: new Vector2(0.15f, 0.15f), rotation: (float)Rotate, origin: origin);


        }
    }
}
