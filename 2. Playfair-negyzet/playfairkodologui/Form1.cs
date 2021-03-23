using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Linq;

namespace playfairkodologui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Green;
            String[] m = richTextBox1.Text.Split(' ');
            foreach(var i in m)
            {
                if(i.Length != 2)
                {
                    label1.ForeColor = Color.Red;
                    return;
                }
            }

            foreach(var i in m)
            {
                if (i.First() < 'A' || i.First() > 'Z' ||i.Last() < 'A' || i.Last() > 'Z')
                    {
                    label1.ForeColor = Color.Blue;
                    return;
                }
            }

            foreach(var i in m)
            {
                if (i.First() == i.Last())
                {
                    label1.ForeColor = Color.Magenta;
                    return;
                }
            }
        }
    }
}
