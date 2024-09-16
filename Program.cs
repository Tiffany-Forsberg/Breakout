using SFML.Graphics;
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
                while (window.IsOpen)
                {
                    float deltaTime = clock.Restart().AsSeconds();
                    window.DispatchEvents();

                    // Updates
                    ball.Update(deltaTime);
                    window.Clear(new Color(131, 197, 235));

                    // Drawing
                    ball.Draw(window);
                    window.Display();
                }
            }
        }
    }
}