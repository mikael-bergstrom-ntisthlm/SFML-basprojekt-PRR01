using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;


namespace SFML_basprojekt
{
    class Program
    {
        static void Main(string[] args)
        {

            // Skapa fönster
            VideoMode videoMode = new VideoMode(800, 600);
            RenderWindow window = new RenderWindow(videoMode, "Fönstertitel");


            // Lägg in event handlers
            window.Closed += OnClose;


            // Skapa boll-form
            CircleShape ball = new CircleShape(32);
            ball.FillColor = Color.Green;
            ball.Origin = new Vector2f(32, 32); // Sätt origo i mitten
            ball.Position = new Vector2f(64, 64);

            Vector2f ballVelocity = new Vector2f(100, 100);


            // Timer-klocka
            Clock clock = new Clock();
            

            // Spel-loop
            while (window.IsOpen)
            {
                // Kör alla events
                window.DispatchEvents();

                // Hämta tid sedan föregående frame
                Time deltaTime = clock.Restart();
                ballVelocity = BallLogic(window, ball, ballVelocity, deltaTime);

                // Rensa fönstret
                window.Clear(Color.Black);

                // Rita objekt
                window.Draw(ball);

                // Uppdatera fönstrets grafik
                window.Display();
            }
        }

        private static Vector2f BallLogic(RenderWindow window, CircleShape ball, Vector2f ballVelocity, Time deltaTime)
        {
            // Flytta bollen
            ball.Position += ballVelocity * deltaTime.AsSeconds();

            // Om bollen kommit för långt åt vänster eller höger, 
            // vänd vektorn horisontellt
            if (ball.Position.X > window.Size.X ||
                ball.Position.X < 0)
            {
                ballVelocity = new Vector2f(
                    -ballVelocity.X,
                    ballVelocity.Y
                    );
            }

            // Om bollen kommit för långt uppåt eller neråt, 
            // vänd vektorn vertikalt
            if (ball.Position.Y > window.Size.Y ||
                ball.Position.Y < 0)
            {
                ballVelocity = new Vector2f(
                    ballVelocity.X,
                    -ballVelocity.Y
                    );
            }

            // Returnera den nya boll-förflyttningsvektorn
            return ballVelocity;
        }

        static void OnClose(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }
    }
}
