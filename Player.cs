using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ParticleEngine2D;
using System.Collections.Generic;

namespace MineGame
{
    public class Player
    {
        private readonly ContentManager content;
        private readonly RoomScene room;
        private KeyboardState lastState;
        private ParticleEngine particleEmitter;
        private string textureName = "robot_3Dred";

        /// <summary>
        /// Tile X
        /// </summary>
        private int X;
        /// <summary>
        /// Tile Y
        /// </summary>
        private int Y;
        private double spriteRotatation;

        public Player(ContentManager content, RoomScene room)
        {
            this.content = content;
            this.room = room;
            X = (int)room.StartPosition.X;
            Y = (int)room.StartPosition.Y;
        }

        public bool HasExploded { get; private set; }
        public bool HasReachedGoal { get; private set; }

        public void Update()
        {
            particleEmitter?.Update();

            if (HasExploded || HasReachedGoal)
            {
                // Don't allow robot to move if game is over
                return;
            }

            KeyboardState state = Keyboard.GetState();
            if (IsNewKeyPress(state, Keys.Left) && room.TileIsWalkable(X - 1, Y))
            {
                X--;
                spriteRotatation = 180.ConvertToRadians();
                InteractWithTile(X, Y);
            }
            if (IsNewKeyPress(state, Keys.Right) && room.TileIsWalkable(X + 1, Y))
            {
                X++;
                spriteRotatation = 0.ConvertToRadians();
                InteractWithTile(X, Y);
            }
            if (IsNewKeyPress(state, Keys.Up) && room.TileIsWalkable(X, Y - 1))
            {
                Y--;
                spriteRotatation = 270.ConvertToRadians();
                InteractWithTile(X, Y);
            }
            if (IsNewKeyPress(state, Keys.Down) && room.TileIsWalkable(X, Y + 1))
            {
                Y++;
                spriteRotatation = 90.ConvertToRadians();
                InteractWithTile(X, Y);
            }


            lastState = state;
        }

        internal void InteractWithTile(int x, int y)
        {
            var tile = room.Tiles[x, y];
            if (tile.TileType == TileType.Exit)
            {
                ReachGoal();
            }
            else if (tile.TileType == TileType.Mine)
            {
                tile.Explode();
                Kill();
            }
        }

        private bool IsNewKeyPress(KeyboardState state, Keys key)
        {
            return state.IsKeyDown(key) && !lastState.IsKeyDown(key);
        }

        public void ReachGoal()
        {
            HasReachedGoal = true;

            var textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("star"));

            particleEmitter = new ParticleEngine(textures, new Vector2(600, 340), particleCount: 30, ttl: 240);

            particleEmitter.EmitterLocation = new Vector2(X * Constants.TileSize * Constants.Zoom + 16, Y * Constants.TileSize * Constants.Zoom + 16);
        }

        public void Kill()
        {
            HasExploded = true;

            var textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("star"));

            particleEmitter = new ParticleEngine(textures, new Vector2(400, 240), color: Color.Red);

            particleEmitter.EmitterLocation = new Vector2(X * Constants.TileSize * Constants.Zoom + 16, Y * Constants.TileSize * Constants.Zoom + 16);
        }

        public void Draw(SpriteBatch sb)
        {
            particleEmitter?.Draw(sb);

            if (HasExploded)
            {
                // TODO - fade out on death
                return;
            }

            var spriteSize = Constants.TileSize;
            var texture = content.Load<Texture2D>(textureName);
            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            sb.Draw(texture, new Vector2(X * spriteSize * Constants.Zoom + 16, Y * spriteSize * Constants.Zoom + 16), color: Color.White, scale: new Vector2(0.15f, 0.15f), rotation: (float)spriteRotatation, origin: origin);


        }
    }
}
