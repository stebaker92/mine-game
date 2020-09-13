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
        private GameState gameState;
        private GameOverComponent gameOverOverlay;
        private SuccessComponent successOverlay;
        private KeyboardState keyboardState;
        private KeyboardState lastKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        }

        public void Restart()
        {
            Initialize();
        }

        public bool IsNewKeyPress(Keys key)
        {
            return keyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key);
        }

        protected override void Initialize()
        {
            base.Initialize();

            room = new RoomScene(Content);
            room.LoadMap();

            player = new Player(Content, room, this);

            gameState = GameState.Playing;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            switch (gameState)
            {
                case GameState.Playing:
                    player.Update();

                    if (player.HasExploded)
                    {
                        gameState = GameState.GameOver;
                        gameOverOverlay = new GameOverComponent(this);
                    }
                    else if (player.HasReachedGoal)
                    {
                        gameState = GameState.Won;
                        successOverlay = new SuccessComponent(this);
                    }

                    break;
                case GameState.GameOver:
                    player.Update();
                    gameOverOverlay.Update();
                    break;
                case GameState.Won:
                    player.Update();
                    successOverlay.Update();
                    break;
            }

            lastKeyboardState = keyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            switch (gameState)
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
                    successOverlay.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
