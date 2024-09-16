using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    public class Ball
    {
        public Sprite Sprite;
        public const float Diameter = 20.0f;
        public const float Radius = Diameter * 0.5f;
        public Vector2f Direction = new Vector2f(1, 1) / MathF.Sqrt(2.0f);
        
        public Ball()
        {
            this.Sprite = new Sprite();
            this.Sprite.Texture = new Texture("assets/ball.png");
            this.Sprite.Position = new Vector2f(250, 300);
            Vector2f ballTextureSize = (Vector2f) Sprite.Texture.Size;
            Sprite.Origin = 0.5f * ballTextureSize;
            Sprite.Scale = new Vector2f(Diameter / ballTextureSize.X, Diameter / ballTextureSize.Y);
        }

        public void Update(float deltaTime)
        {
            var newPos = this.Sprite.Position;
            newPos += this.Direction * deltaTime * 100.0f;
            
            // Collision right side of screen
            if (newPos.X > Program.ScreenW - Radius)
            {
                newPos.X = Program.ScreenW - Radius;
                Reflect(new Vector2f(-1, 0));
            }
            
            // Collision left side of screen
            if (newPos.X < Radius)
            {
                newPos.X = Radius;
                Reflect(new Vector2f(1, 0));
            }
            
            // Collision bottom of screen
            if (newPos.Y > Program.ScreenH - Radius)
            {
                newPos.Y = Program.ScreenH - Radius;
                Reflect(new Vector2f(0, -1));
            }
            
            // Collision top of screen
            if (newPos.Y < Radius)
            {
                newPos.Y = Radius;
                Reflect(new Vector2f(0, 1));
            }
            
            this.Sprite.Position = newPos;
        }

        public void Reflect(Vector2f normal)
        {
            this.Direction -= normal * (2 * (this.Direction.X * normal.X + this.Direction.Y * normal.Y));
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(this.Sprite);
        }
    }
}