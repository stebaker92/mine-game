using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MineGame.Components;
using ParticleEngine2D;
using System;
using System.Collections.Generic;

namespace MineGame
{
    class SuccessComponent
    {
        private readonly Game1 game;
        private readonly SpriteFont fontLarge;
        private readonly List<ParticleEngine> emitters;
        private readonly MenuComponent menu;
        private Texture2D gradientTexture;
        private int screenHeight = Constants.ScreenHeight;
        private const string titleText = "You Won";
        private int titleAlpha = 0;
        private int backgroundAlpha = 0;
        private Vector2 titlePosition;

        public SuccessComponent(Game1 game)
        {
            this.game = game;

            gradientTexture = new Texture2D(game.GraphicsDevice, 1, 1);

            gradientTexture = CreateGreenGradientTexture(game.GraphicsDevice, 1000, 600, reverse: true, 8);

            fontLarge = game.Content.Load<SpriteFont>("ArialLarge");

            var stringLength = fontLarge.MeasureString(titleText);
            var screenCenter = Constants.ScreenWidth / 2;

            var titleX = screenCenter - stringLength.X / 2;
            var titleY = 100;

            titlePosition = new Vector2(titleX, titleY);

            var textures = new List<Texture2D>
            {
                game.Content.Load<Texture2D>("star"),
                game.Content.Load<Texture2D>("diamond"),
            };


            ParticleEngine CreateEmitter(float x, float y)
            {
                return new ParticleEngine(textures, new Vector2(x, y), 240, null, 2);
            }

            emitters = new List<ParticleEngine>
            {
                CreateEmitter(titlePosition.X, titleY),
                CreateEmitter(titlePosition.X + fontLarge.MeasureString(titleText).X / 2, titleY),
                CreateEmitter(titlePosition.X + fontLarge.MeasureString(titleText).X, titleY),
            };

            menu = new MenuComponent(game);

            menu.AddOption("Play Game", () => { game.Restart(); });
            menu.AddOption("Exit", () => { game.Exit(); });
        }

        /// <summary>
        /// Creates a pretty cool gradient texture!
        /// Used for a background Texture!
        /// </summary>
        /// <param name="width">The width of the current viewport</param>
        /// <param name="height">The height of the current viewport</param>
        /// A Texture2D with a gradient applied.
        private Texture2D CreateGreenGradientTexture(GraphicsDevice graphicsDevice, int width, int height, bool reverse, int gradientOffset = 12)
        {
            var texture = new Texture2D(graphicsDevice, width, height);
            Color[] bgc = new Color[height * width];
            int texColour;          // Defines the colour of the gradient.

            for (int i = 0; i < bgc.Length; i++)
            {
                texColour = (i / (screenHeight * gradientOffset));

                // Create a pixel that's slightly more Green then the previous one
                bgc[i] = new Color(0, texColour, 0, 128);
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
            if (titleAlpha < 255)
            {
                titleAlpha++;
            }

            if (backgroundAlpha < 255)
            {
                backgroundAlpha += 3;
            }

            menu.Update();

            emitters.ForEach(e => e.Update());
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(gradientTexture, new Vector2(0, 0), new Color(backgroundAlpha, backgroundAlpha, backgroundAlpha, backgroundAlpha));

            emitters.ForEach(e => e.Draw(sb));

            DrawTitle(sb);

            menu.Draw(sb);
        }

        private void DrawTitle(SpriteBatch sb)
        {

            var color = new Color(titleAlpha, titleAlpha, titleAlpha, titleAlpha);

            sb.DrawString(fontLarge, titleText, titlePosition, color);
        }
    }
}
