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
        public Vector2f BaseSize;
        public Vector2f BaseScale;
        public float Width = 128.0f;
        public float Height = 28.0f;
        private Clock Timer;
        public int PowerUpDuration;

        public Paddle()
        {
            this.Sprite = new Sprite();
            this.Sprite.Texture = new Texture("assets/paddle.png");
            this.Sprite.Position = new Vector2f(Program.ScreenW / 2f, Program.ScreenH - 50f);
            
            Vector2f paddleTextureSize = (Vector2f) this.Sprite.Texture.Size;
            Sprite.Origin = 0.5f * paddleTextureSize;
            Sprite.Scale = new Vector2f(this.Width / paddleTextureSize.X, this.Height / paddleTextureSize.Y);
            BaseScale = Sprite.Scale;

            this.Size = new Vector2f(this.Sprite.GetGlobalBounds().Width, this.Sprite.GetGlobalBounds().Height);
            BaseSize = Size;

            PowerUpDuration = 0;
            Timer = new Clock();
        }

        public void Update(Ball ball, PowerUps powerUps, float deltaTime)
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

            foreach (Vector2f position in powerUps.Positions)
            {
                if (Collision.CircleRectangle(
                    position,
                    PowerUps.Radius,
                    Sprite.Position,
                    Size,
                    out Vector2f hitPowerUp
                ))
                {
                    if (PowerUpDuration == 0)
                    {
                        Timer.Restart();
                    }

                    PowerUpDuration += 4;
                    powerUps.Positions.Remove(position);
                    break;
                }
            }

            if (Timer.ElapsedTime.AsSeconds() < PowerUpDuration && PowerUpDuration > 0)
            {
                Sprite.Scale = new Vector2f(BaseScale.X * 1.5f, BaseScale.Y);
                Size = new Vector2f(BaseSize.X * 1.5f, BaseSize.Y);
            }
            else
            {
                PowerUpDuration = 0;
                Sprite.Scale = BaseScale;
                Size = BaseSize;
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