using System;

namespace PhantomDevelopmentKit
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Forms.MainForm mainForm = new Forms.MainForm();
            mainForm.Show();

            using (DevKit game = new DevKit(mainForm))
            {
                game.Run();
            }
        }
    }
#endif
}

