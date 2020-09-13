using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MineGame.Components
{
    class MenuComponent
    {
        private readonly Game1 game;
        private readonly Texture2D textureOption;
        private Texture2D textureOptionActive;
        private Texture2D textureOptionBorder;
        private int selectedIndex;
        private readonly List<(string text, Action action)> options;

        public MenuComponent(Game1 game)
        {
            this.game = game;

            options = new List<(string text, Action action)>();

            textureOption = new Texture2D(game.GraphicsDevice, 1, 1);
            textureOption.SetData(new Color[] { Color.Black });

            textureOptionActive = new Texture2D(game.GraphicsDevice, 1, 1);
            textureOptionActive.SetData(new Color[] { Color.LightGray });

            textureOptionBorder = new Texture2D(game.GraphicsDevice, 1, 1);
            textureOptionBorder.SetData(new Color[] { Color.White });
        }

        internal void AddOption(string text, Action action)
        {
            options.Add((text, action));
        }

        internal void Update()
        {
            if (game.IsNewKeyPress(Keys.Up) && selectedIndex > 0)
            {
                selectedIndex--;
            }
            else if (game.IsNewKeyPress(Keys.Down) && selectedIndex < options.Count - 1)
            {
                selectedIndex++;
            }
            else if (game.IsNewKeyPress(Keys.Enter))
            {
                options[selectedIndex].Item2.Invoke();
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < options.Count; i++)
            {
                DrawOption(sb, options[i].text, i);
            }
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
