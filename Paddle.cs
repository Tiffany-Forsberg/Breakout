using breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    public class Paddle
    {
        public Sprite Sprite;
        public Vector2f Size;
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

            this.Size = new Vector2f(this.Sprite.GetGlobalBounds().Width, this.Sprite.GetGlobalBounds().Height);
        }

        public void Update(Ball ball, float deltaTime)
        {
            var newPos = this.Sprite.Position;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                newPos.X += deltaTime * 300.0f;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                newPos.X -= deltaTime * 300.0f;
            }

            if (newPos.X > Program.ScreenW - this.Width / 2)
            {
                newPos.X = Program.ScreenW - this.Width / 2;
            }
            
            if (newPos.X < this.Width / 2)
            {
                newPos.X = this.Width / 2;
            }

            if (Collision.CircleRectangle(
                    ball.Sprite.Position, 
                    Ball.Radius, 
                    this.Sprite.Position, 
                    this.Size,
                    out Vector2f hit
                ))
            {
                ball.Sprite.Position += hit;
                ball.Reflect(hit.Normalized());
            }

            this.Sprite.Position = newPos;
        }
        
        public void Draw(RenderTarget target)
        {
            target.Draw(this.Sprite);
        }
    }
}