using Microsoft.Xna.Framework.Graphics; // for Texture2D 
using Microsoft.Xna.Framework; // for Vector2 

namespace Spiff_Platformer
{
    class playerSprite
    {
        public Texture2D texture { get; set; } // sprite texture, read-only property 
        public Vector2 position { get; set; } // sprite position on screen 
        public Vector2 size { get; set; } // sprite size in pixels 
        public Vector2 velocity { get; set; } // sprite velocity 
        private Vector2 screenSize { get; set; } // screen size 

        public Vector2 center { get { return position + (size / 2); } } // sprite center 
        public float radius { get { return size.X / 2; } } // sprite radius 

        public int scorePlayer;

        public playerSprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth,
        int ScreenHeight)
        {
            texture = newTexture;
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);
            scorePlayer = 0;
        }

        public bool Collides1(playerSprite otherSprite)
        {
            // check if two sprites intersect 
            if (this.position.X + this.size.X > otherSprite.position.X &&
            this.position.X < otherSprite.position.X + otherSprite.size.X &&
            this.position.Y + this.size.Y > otherSprite.position.Y &&
            this.position.Y < otherSprite.position.Y + otherSprite.size.Y)
                return true;
            else
                return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
