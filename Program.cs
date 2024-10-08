﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Breakout
{
    class Program
    {
        public const int ScreenW = 500;
        public const int ScreenH = 700;

        static void Main(string[] args)
        {
            using (var window = new RenderWindow(
                new VideoMode(ScreenW, ScreenH), "Breakout"))
            {
                window.Closed += (o, e) => window.Close();

                Clock clock = new Clock();
                Ball ball = new Ball();
                Paddle paddle = new Paddle();
                Tiles tiles = new Tiles();
                PowerUps powerUps = new PowerUps();

                while (window.IsOpen)
                {
                    float deltaTime = clock.Restart().AsSeconds();
                    window.DispatchEvents();

                    // Resets the game if the player loses
                    if (ball.Health <= 0)
                    {
                        ball = new Ball();
                        paddle = new Paddle();
                        tiles = new Tiles();
                        powerUps = new PowerUps();
                    }

                    // Resets the tiles if all tiles are destroyed
                    if (tiles.Positions.Count == 0)
                    {
                        tiles = new Tiles();
                    }

                    // Updates
                    ball.Update(deltaTime);
                    paddle.Update(ball, powerUps, deltaTime);
                    tiles.Update(ball, deltaTime, powerUps);
                    powerUps.Update(deltaTime);
                    window.Clear(new Color(131, 197, 235));

                    // Drawing
                    ball.Draw(window);
                    paddle.Draw(window);
                    tiles.Draw(window);
                    powerUps.Draw(window);
                    window.Display();
                }
            }
        }
    }
}