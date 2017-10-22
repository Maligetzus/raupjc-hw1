using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    public class Paddle : Sprite
    {
        /// <summary >
        /// Current paddle speed in time
        /// </ summary >
        public float Speed { get; set; }
        public Paddle(int width, int height, float initialSpeed) : base(width,
            height)
        {
            Speed = initialSpeed;
        }
        /// <summary >
        /// Overriding draw method . Masking paddle texture with black color .
        /// </ summary >
        public override void DrawSpriteOnScreen(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Vector2(X, Y), new Rectangle(0, 0,
                Width, Height), Color.GhostWhite);
        }

        public static Vector2 operator *(Vector2 prvi, Paddle drugi)
        {
            Vector2 vektor;
            vektor.X = prvi.X * drugi.X;
            vektor.Y = prvi.Y * drugi.Y;
            return vektor;
        }
    }
}
