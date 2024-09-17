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
        private float Speed = 200.0f;
        public int Health = 3;
        public int Score;
        public Text Gui;
        public bool Active;
        
        public Ball()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/ball.png");
            Vector2f ballTextureSize = (Vector2f) Sprite.Texture.Size;
            Active = false;
            Sprite.Origin = 0.5f * ballTextureSize;
            Sprite.Scale = new Vector2f(Diameter / ballTextureSize.X, Diameter / ballTextureSize.Y);
            Gui = new Text();
            Gui.CharacterSize = 24;
            Gui.Font = new Font("assets/future.ttf");
        }

        public void Update(float deltaTime)
        {
            var newPos = this.Sprite.Position;
            newPos += this.Direction * deltaTime * this.Speed;
            
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
                if (Health > 0)
                {
                    Health -= 1;
                    Active = false;
                    float newXDirection;
                    if (new Random().Next() % 2 == 0)
                    {
                        newXDirection = -1;
                    }
                    else
                    {
                        newXDirection = 1;
                    }
                    Direction = new Vector2f(newXDirection, -1) / MathF.Sqrt(2.0f);
                }
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

            Gui.DisplayedString = $"Health: {Health}";
            Gui.Position = new Vector2f(12, 8);
            target.Draw(Gui);

            Gui.DisplayedString = $"Score: {Score}";
            Gui.Position = new Vector2f(Program.ScreenW - Gui.GetGlobalBounds().Width - 12, 8);
            target.Draw(Gui);
        }
    }
}