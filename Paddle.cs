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
            
            Vector2f paddleTextureSize = (Vector2f) this.Sprite.Texture.Size;
            Sprite.Origin = 0.5f * paddleTextureSize;
            Sprite.Scale = new Vector2f(this.Width / paddleTextureSize.X, this.Height / paddleTextureSize.Y);

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

            if (!ball.Active && Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                ball.Active = true;
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

            if (ball.Active == false)
            {
                ball.Sprite.Position = newPos - new Vector2f(0, 30.0f);
            }

            this.Sprite.Position = newPos;
        }
        
        public void Draw(RenderTarget target)
        {
            target.Draw(this.Sprite);
        }
    }
}