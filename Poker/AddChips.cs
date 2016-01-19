namespace Poker
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class AddChips : Form
    {
        public int a;
        public AddChips()
        {
            FontFamily fontFamily = new FontFamily("Arial");
            InitializeComponent();
            this.ControlBox = false;
            this.label1.BorderStyle = BorderStyle.FixedSingle;
        }

        public void button1_Click(object sender, EventArgs e)
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
                this.a = int.Parse(this.textBox1.Text);
                Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var message = "Are you sure?";
            var title = "Quit";
            var result = MessageBox.Show(
            message,title,
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
