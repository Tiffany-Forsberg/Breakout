using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    public class Paddle
    {
        public Sprite Sprite;
        public float Width = 128.0f;
        public float Height = 28.0f;

        public Paddle()
        {
            this.Sprite = new Sprite();
            this.Sprite.Texture = new Texture("assets/paddle.png");
            this.Sprite.Position = new Vector2f(Program.ScreenW / 2f, Program.ScreenH - 50f);
            
            Vector2f ballTextureSize = (Vector2f) this.Sprite.Texture.Size;
            Sprite.Origin = 0.5f * ballTextureSize;
            Sprite.Scale = new Vector2f(this.Width / ballTextureSize.X, this.Height / ballTextureSize.Y);
        }

        public void Update(float deltaTime)
        {
            
        }
        
        public void Draw(RenderTarget target)
        {
            target.Draw(this.Sprite);
        }
    }
}