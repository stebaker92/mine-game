using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MineGame
{
    public enum GameState
    {
        Playing,
        GameOver,
        Won,
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private RoomScene room;
        private Player player;
        private GameState state;
        private GameOverWidget gameOverOverlay;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();

            room = new RoomScene(Content);
            room.LoadLevelOne();

            player = new Player(Content, room);

            state = GameState.Playing;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (state)
            {
                case GameState.Playing:
                    player.Update();

                    if (player.HasExploded)
                    {
                        state = GameState.GameOver;
                        gameOverOverlay = new GameOverWidget(graphics.GraphicsDevice, Content);
                    }
                    else if (player.HasReachedGoal)
                    {
                        state = GameState.Won;
                    }

                    break;
                case GameState.GameOver:
                    player.Update();

                    gameOverOverlay.Update();
                    break;
                case GameState.Won:
                    player.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (state)
            {
                case GameState.Playing:
                    room.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    break;
                case GameState.GameOver:
                    room.Draw(spriteBatch);
                    player.Draw(spriteBatch);
                    gameOverOverlay.Draw(spriteBatch);
                    break;
                case GameState.Won:
                    player.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
