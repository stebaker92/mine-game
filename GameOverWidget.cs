using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MineGame
{
    class GameOverWidget
    {
        private GraphicsDevice graphicsDevice;
        private readonly ContentManager content;
        private Texture2D fadeTexture;
        private float fadeLayerOpacity = 0.1f;
        private int screenHeight = 600;

        private const string gameOverText = "You Died";

        public GameOverWidget(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            fadeTexture = new Texture2D(graphicsDevice, 1, 1);
            fadeTexture.SetData(new[] { new Color(3, 0, 0, fadeLayerOpacity) });

            fadeTexture = CreateRedGradientTexture(graphicsDevice, 1000, 600, reverse: true);
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
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(fadeTexture, new Vector2(0, 0));

            var font = content.Load<SpriteFont>("PithazardLarge");

            var stringLength = font.MeasureString(gameOverText);
            var screenCenter = Constants.ScreenWidth / 2;

            var textX = screenCenter - stringLength.X / 2;
            var textY = 100;

            sb.DrawString(font, "You Died", new Vector2(textX, textY), Color.Red);
        }
    }
}
