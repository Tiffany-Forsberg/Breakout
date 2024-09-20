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
        private List<Texture> TileTextures;

        public Tiles()
        {
            Sprite = new Sprite();
            Sprite.Texture = new Texture("assets/tileBlue.png");
            
            // Preload textures
            TileTextures = new List<Texture>
            {
                new Texture("assets/tileBlue.png"),
                new Texture("assets/tilePink.png"),
                new Texture("assets/tileGreen.png"),
            };
            
            // Uses sprite size to set origin to center
            Vector2f tileTextureSize = (Vector2f) this.Sprite.Texture.Size;
            Sprite.Origin = 0.5f * tileTextureSize;
            
            // Resizes sprite
            Sprite.Scale = new Vector2f(51 * 1.2f / tileTextureSize.X, 24 * 1.2f / tileTextureSize.Y);
            
            // Sets size to be used for collision
            Size = new Vector2f(Sprite.GetGlobalBounds().Width, Sprite.GetGlobalBounds().Height);
            
            // Initiates list of tile positions
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

        public void Update(Ball ball, float deltaTime, PowerUps powerUps)
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

                    if (new Random().Next(10) == 0)
                    {
                        powerUps.Positions.Add(pos);
                    }

                    Positions.RemoveAt(i);
                    i = 0; // Check all again since ball was moved
                }
            }
        }

        public void Draw(RenderTarget target)
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                // Sets sprite depending on Y position
                if (
                    Positions[i].Y > Program.ScreenH * 0.3f + -1 * 48 &&
                    Positions[i].Y < Program.ScreenH * 0.3f + 1 * 48
                )
                {
                    Sprite.Texture = TileTextures[2];
                } 
                else if (
                    Positions[i].Y > Program.ScreenH * 0.3f + -2 * 48 &&
                    Positions[i].Y < Program.ScreenH * 0.3f + 2 * 48
                )
                {
                    Sprite.Texture = TileTextures[1];
                }
                else
                {
                    Sprite.Texture = TileTextures[0];
                }
                
                Sprite.Position = Positions[i];
                target.Draw(Sprite);
            }
        }
    }
}