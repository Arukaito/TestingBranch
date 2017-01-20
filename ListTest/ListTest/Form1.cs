using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ListTesting()
        {
            for (int i = 0; i < textBoxX1.Lines.Length; i++)
            {
                textBoxX2.Text += textBoxX1.Lines[i] + "\r\n";
            }

            textBoxX1.Text = textBoxX1.Text.Trim();
            textBoxX2.Text = textBoxX2.Text.Trim();

            labelX1.Text = textBoxX1.Lines.Length.ToString();
            labelX2.Text = textBoxX2.Lines.Length.ToString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            ListTesting();
        }
    }
}
