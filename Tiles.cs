using breakout;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace Breakout
{
    class Tiles
    {
        public Sprite Sprite;
        public Vector2f Size;
        public List<Vector2f> Positions;

        public Tiles()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/tileBlue.png");
            
            Vector2f tileTextureSize = (Vector2f) this.Sprite.Texture.Size;
            Sprite.Origin = 0.5f * tileTextureSize;
            Sprite.Scale = new Vector2f(51 * 1.2f / tileTextureSize.X, 24 * 1.2f / tileTextureSize.Y);
            
            this.Size = new Vector2f(Sprite.GetGlobalBounds().Width, Sprite.GetGlobalBounds().Height);

            Positions = new List<Vector2f>();
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    var pos = new Vector2f(Program.ScreenW * 0.5f + i * 96.0f, Program.ScreenH * 0.3f + j * 48.0f);
                    Positions.Add(pos);
                }
            }
        }

        public void Update(Ball ball, float deltaTime)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                var pos = Positions[i];

                if (Collision.CircleRectangle(
                        ball.Sprite.Position,
                        Ball.Radius,
                        pos,
                        Size,
                        out Vector2f hit
                    ))
                {
                    ball.Sprite.Position += hit;
                    ball.Reflect(hit.Normalized());
                    ball.Score += 100;
                    Positions.RemoveAt(i);
                    i = 0; // Check all again since ball was moved
                }
            }
        }

        public void Draw(RenderTarget target)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                if (Positions[i].Y == Program.ScreenH * 0.3f + -2 * 48 || Positions[i].Y == Program.ScreenH * 0.3f + 2 * 48 )
                {
                    Sprite.Texture = new Texture("assets/tilePink.png");
                } 
                else if (Positions[i].Y == Program.ScreenH * 0.3f + -1 * 48 || Positions[i].Y == Program.ScreenH * 0.3f + 1 * 48 )
                {
                    Sprite.Texture = new Texture("assets/tileBlue.png");
                }
                else
                {
                    Sprite.Texture = new Texture("assets/tileGreen.png");
                }
                Sprite.Position = Positions[i];
                target.Draw(Sprite);
            }
        }
    }
}