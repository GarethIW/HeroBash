using System;

namespace HeroBash
{
#if WINDOWS || LINUX || XBOX 
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (HeroBash game = new HeroBash())
            {
                game.Run();
            }
        }
    }
#endif
#if WINRT
    //public static class Program
    //{
    //    /// <summary>
    //    /// The main entry point for the application.
    //    /// </summary>
    //    static void Main()
    //    {
    //        var factory = new MonoGame.Framework.GameFrameworkViewSource<HeroBash>();
    //        Windows.ApplicationModel.Core.CoreApplication.Run(factory);
    //    }
    //}
#endif
}

