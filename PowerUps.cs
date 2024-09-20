using System.Numerics;
using breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    public class PowerUps
    {
        public Sprite Sprite;
        public const float Diameter = 25.0f;
        public const float Radius = Diameter * 0.5f;
        private float Speed = 150.0f;
        public Text Gui;
        public List<Vector2f> Positions;

        public PowerUps()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/ball.png");
            Sprite.Color = new Color(255, 69, 0);

            // Uses sprite size to set origin to center
            Vector2f powerUpTextureSize = (Vector2f)Sprite.Texture.Size;
            Sprite.Origin = 0.5f * powerUpTextureSize;
            
            // Resizes sprite
            Sprite.Scale = new Vector2f(Diameter / powerUpTextureSize.X, Diameter / powerUpTextureSize.Y);
            
            // Initiates list of power up positions
            Positions = new List<Vector2f>();

            Gui = new Text();
            Gui.CharacterSize = 24;
            Gui.Origin = new Vector2f(0.5f * Gui.CharacterSize - 5, 0.5f * Gui.CharacterSize + 4);
            Gui.Font = new Font("assets/future.ttf");
        }

        public void Update(float deltaTime)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                Positions[i] += new Vector2f(0.0f, 1.0f) * deltaTime * Speed;
                if (Positions[i].Y > Program.ScreenH)
                {
                    Positions.RemoveAt(i);
                }
            }
        }

        public void Draw(RenderTarget target)
        {
            foreach (Vector2f powerUpPosition in Positions)
            {
                Sprite.Position = powerUpPosition;
                target.Draw(Sprite);
                
                Gui.DisplayedString = "P";
                Gui.Position = powerUpPosition;
                target.Draw(Gui);
            }
        }
    }
}