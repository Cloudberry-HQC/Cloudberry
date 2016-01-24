namespace Poker
{
    using System;
    using System.Windows.Forms;
    using Forms;

    public static class Launcher
    {
        private static readonly Game poker = new Game();
        public static Game Poker
        { get { return poker; } }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(poker);
        }
    }
}
