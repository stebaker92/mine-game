using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineGame.Components;
using System;

namespace MineGame
{
    class GameOverComponent
    {
        private readonly Game1 game;
        private readonly SpriteFont fontLarge;
        private const string gameOverText = "You Died";
        private Texture2D gradientTexture;
        private MenuComponent menu;
        private int screenHeight = Constants.ScreenHeight;
        private int backgroundAlpha = 0;

        public GameOverComponent(Game1 game)
        {
            this.game = game;

            fontLarge = game.Content.Load<SpriteFont>("PithazardLarge");
            gradientTexture = CreateRedGradientTexture(game.GraphicsDevice, 1000, 600, reverse: true);

            menu = new MenuComponent(game);

            menu.AddOption("Retry", () => { game.Restart(); });
            menu.AddOption("Exit", () => { game.Exit(); });
        }

        /// <summary>
        /// Creates a pretty cool gradient texture!
        /// Used for a background Texture!
        /// </summary>
        /// <param name="width">The width of the current viewport</param>
        /// <param name="height">The height of the current viewport</param>
        /// A Texture2D with a gradient applied.
        private Texture2D CreateRedGradientTexture(GraphicsDevice graphicsDevice, int width, int height, bool reverse, int gradientOffset = 12)
        {
            var texture = new Texture2D(graphicsDevice, width, height);
            Color[] bgc = new Color[height * width];
            int texColour;          // Defines the colour of the gradient.

            for (int i = 0; i < bgc.Length; i++)
            {
                texColour = (i / (screenHeight * gradientOffset));

                // Create a pixel that's slightly more Red then the previous one
                bgc[i] = new Color(texColour, 0, 0, 128);
            }

            if (reverse)
            {
                Array.Reverse(bgc);
            }

            texture.SetData(bgc);
            return texture;
        }

        internal void Update()
        {
            if (backgroundAlpha < 255)
            {
                backgroundAlpha += 3;
            }

            menu.Update();
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gradientTexture, new Vector2(0, 0), new Color(backgroundAlpha, backgroundAlpha, backgroundAlpha));

            DrawTitle(sb);

            menu.Draw(sb);
        }

        private void DrawTitle(SpriteBatch sb)
        {
            var stringLength = fontLarge.MeasureString(gameOverText);
            var screenCenter = Constants.ScreenWidth / 2;

            var textX = screenCenter - stringLength.X / 2;
            var textY = 100;

            sb.DrawString(fontLarge, gameOverText, new Vector2(textX, textY), Color.Red);
        }
    }
}
