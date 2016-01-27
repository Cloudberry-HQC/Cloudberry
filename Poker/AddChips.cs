namespace Poker
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Class for adding chips in the game.
    /// </summary>
    public partial class AddChips : Form
    {
        public AddChips()
        {
            FontFamily fontFamily = new FontFamily("Arial");
            this.InitializeComponent();
            this.ControlBox = false;
            this.label1.BorderStyle = BorderStyle.FixedSingle;
        }

        public int AmountOfChips { get; set; }

        /// <summary>
        /// Method is executed when the button for adding chips was clicked.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">Parameters of the event.</param>
        public void Button1_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (int.Parse(this.textBox1.Text) > 100000000)
            {
                MessageBox.Show("The maximium chips you can add is 100000000");
                return;
            }

            if (!int.TryParse(this.textBox1.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            else if (int.TryParse(this.textBox1.Text, out parsedValue) && int.Parse(this.textBox1.Text) <= 100000000)
            {
                this.AmountOfChips = int.Parse(this.textBox1.Text);
                this.Close();
            }
        }

        /// <summary>
        /// Method is executed when the button for exit from the game was clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(
            message,
            title,
            MessageBoxButtons.YesNo, 
            MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.No:
                    break;
                case DialogResult.Yes:
                    Application.Exit();
                    break;
            }
        }
    }
}
