using System;

namespace HeavyBomber
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HeavyBomberGame game = new HeavyBomberGame())
            {
                game.Run();
            }
        }
    }
#endif
}

