using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MineGame
{
    class GameOverWidget
    {
        private readonly Game1 game;
        private readonly SpriteFont fontLarge;
        private readonly Texture2D textureOption;
        private Texture2D gradientTexture;
        private float fadeLayerOpacity = 0.1f;
        private int screenHeight = Constants.ScreenHeight;
        private int selectedIndex;
        private KeyboardState lastState;
        private Texture2D textureOptionActive;
        private Texture2D textureOptionBorder;
        private const string gameOverText = "You Died";

        public GameOverWidget(Game1 game)
        {
            gradientTexture = new Texture2D(game.GraphicsDevice, 1, 1);
            gradientTexture.SetData(new[] { new Color(3, 0, 0, fadeLayerOpacity) });

            gradientTexture = CreateRedGradientTexture(game.GraphicsDevice, 1000, 600, reverse: true);
            this.game = game;
            fontLarge = game.Content.Load<SpriteFont>("PithazardLarge");

            // textures for menu options
            textureOption = new Texture2D(game.GraphicsDevice, 1, 1);
            textureOption.SetData(new Color[] { Color.Black });

            textureOptionActive = new Texture2D(game.GraphicsDevice, 1, 1);
            textureOptionActive.SetData(new Color[] { Color.LightGray });

            textureOptionBorder = new Texture2D(game.GraphicsDevice, 1, 1);
            textureOptionBorder.SetData(new Color[] { Color.White });
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
            // TODO - refactor this into a Menu component
            if (game.IsNewKeyPress(Keys.Up) && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (game.IsNewKeyPress(Keys.Down) && selectedIndex < 1)
            {
                selectedIndex++;
            }
            else if (game.IsNewKeyPress(Keys.Enter) && selectedIndex == 0)
            {
                game.Restart();
            }
            else if (game.IsNewKeyPress(Keys.Enter) && selectedIndex == 1)
            {
                game.Exit();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gradientTexture, new Vector2(0, 0));
            
            DrawTitle(sb);

            DrawOptions(sb);
        }

        private void DrawTitle(SpriteBatch sb)
        {
            var stringLength = fontLarge.MeasureString(gameOverText);
            var screenCenter = Constants.ScreenWidth / 2;

            var textX = screenCenter - stringLength.X / 2;
            var textY = 100;

            sb.DrawString(fontLarge, gameOverText, new Vector2(textX, textY), Color.Red);
        }

        private void DrawOptions(SpriteBatch sb)
        {
            DrawOption(sb, "Try Again", 0);
            DrawOption(sb, "Exit", 1);
        }

        private void DrawOption(SpriteBatch sb, string text, int index)
        {
            bool isSelected = index == selectedIndex;

            const int optionsTop = 200;

            const int boxWith = 220;

            var optionRectangle = new Rectangle(
                Constants.ScreenWidth / 2 - (boxWith / 2),
                optionsTop + (100 * (index + 1)),
                boxWith,
                100
            );

            DrawOptionBorder(sb, optionRectangle);

            // Draw option background
            sb.Draw(isSelected ? textureOptionActive : textureOption, optionRectangle, Color.White);
            
            DrawOptionText(sb, text, isSelected, optionRectangle);
        }

        private void DrawOptionText(SpriteBatch sb, string text, bool isSelected, Rectangle optionRectangle)
        {
            var centerOfBox = optionRectangle.Center;

            var font = game.Content.Load<SpriteFont>("ArialMedium");

            sb.DrawString(font, text,
                new Vector2(
                    centerOfBox.X - (font.MeasureString(text).X / 2),
                    centerOfBox.Y - (font.MeasureString(text).Y / 2)
                ),
                isSelected ? Color.Black : Color.White
            );
        }

        private void DrawOptionBorder(SpriteBatch sb, Rectangle boxRectangle)
        {
            const int border = 3;

            var container = boxRectangle;
            container.X -= border;
            container.Y -= border;
            container.Width += border * 2;
            container.Height += border * 2;
            sb.Draw(textureOptionBorder, container, Color.White);

        }
    }
}
