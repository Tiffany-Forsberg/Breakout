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
            
            // Uses sprite size to set origin to center
            Vector2f paddleTextureSize = (Vector2f) this.Sprite.Texture.Size;
            Sprite.Origin = 0.5f * paddleTextureSize;
            
            // Resizes sprite
            Sprite.Scale = new Vector2f(128.0f / paddleTextureSize.X, 28.0f / paddleTextureSize.Y);
            BaseScale = Sprite.Scale;

            // Sets size to be used for collision
            this.Size = new Vector2f(this.Sprite.GetGlobalBounds().Width, this.Sprite.GetGlobalBounds().Height);
            
            // Base size for power up reset
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
            
            if (ball.Active == false)
            {
                ball.Sprite.Position = newPos - new Vector2f(0, 30.0f);
            }

            if (newPos.X > Program.ScreenW - Sprite.GetGlobalBounds().Width / 2)
            {
                newPos.X = Program.ScreenW - Sprite.GetGlobalBounds().Width / 2;
            }
            
            if (newPos.X < Sprite.GetGlobalBounds().Width / 2)
            {
                newPos.X = Sprite.GetGlobalBounds().Width / 2;
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

            foreach (Vector2f powerUpPosition in powerUps.Positions)
            {
                if (Collision.CircleRectangle(
                    powerUpPosition,
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
                    powerUps.Positions.Remove(powerUpPosition);
                    break;
                }
            }

            // Handles active power ups
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
            
            this.Sprite.Position = newPos;
        }
        
        public void Draw(RenderTarget target)
        {
            target.Draw(this.Sprite);
        }
    }
}