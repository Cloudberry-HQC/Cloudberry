namespace Poker
{
    using System;
    using System.Windows.Forms;

    public static class Launcher
    {
        private static   Form1 poker = new Form1();
        public static Form1 Poker
        { get { return poker; } }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
          //  Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(poker);
        }
    }
}
