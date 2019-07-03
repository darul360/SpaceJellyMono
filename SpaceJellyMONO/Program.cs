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
           
               using (var game = new Game1())
                     game.Run();
        }
    }
#endif
}
