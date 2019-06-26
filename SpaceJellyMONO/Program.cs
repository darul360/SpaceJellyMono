using System;

namespace SpaceJellyMONO
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        static bool play = false, tutorial = false;
        [STAThread]
        static void Main()
        {
            using (var game = new Game2()) // main menu
            {
                game.Run();
                if (game.tutorial)
                {
                    tutorial = true;
                }
            }

            if(tutorial)
            using (var game = new Game3())
            {
                    game.Run();
                    if (game.PLAYGAME)
                    {
                        play = true;
                        tutorial = false;
                    }
            }
            if (play)
               using (var game = new Game1())
                     game.Run();
        }
    }
#endif
}
