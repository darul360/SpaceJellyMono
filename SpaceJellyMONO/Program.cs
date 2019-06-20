using System;

namespace SpaceJellyMONO
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        static bool play = false, tutorial = false, returnToMenu = false;
        [STAThread]
        static void Main()
        {
            using (var game = new Game2())
            {
                game.Run();
                if (game.play)
                {
                    play = true;
                }
                if (game.tutorial)
                {
                    tutorial = true;
                }
            }

            if(tutorial)
            using (var game = new Game3())
            {
                    game.Run();
                    if (game.returnToMenu)
                    {
                        returnToMenu = true;
                    }
            }
            if(returnToMenu)
            {
                using (var game = new Game2())
                {
                    game.Run();
                    if (game.play)
                    {
                        play = true;
                    }
                    if (game.tutorial)
                    {
                        tutorial = true;
                    }
                }
            }

            if (play)
               using (var game = new Game1())
                     game.Run();
        }
    }
#endif
}
