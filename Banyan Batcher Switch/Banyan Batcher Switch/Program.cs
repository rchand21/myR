using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Banyan_Batcher_Network
{
    public class Packet
    {
        public int outputport;
        public string binaryValue;
        public int inputport;

    }

    public class Node
    {
        public bool Input0 = false;
        public bool Input1 = false;
        public List<string> Value = new List<string>();
        public List<int> intnum = new List<int>(2);
        public List<int> table = new List<int>();


    }
    class NumericTextBox : TextBox
    {
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (this.Text != "")
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(this.Text, "[^0-9]"))
                {

                    MessageBox.Show(" Please enter numbers only");
                    this.Text = "";
                }
                else if (Convert.ToInt16(this.Text) > 7)
                {
                    MessageBox.Show("Please enter numbers between 0 and 7 only");
                    this.Text = "";
                }
            }
            base.OnTextChanged(e);

            // InputModeEditor.SetInputMode(this, InputMode.Numeric);
        }
    }
        static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
