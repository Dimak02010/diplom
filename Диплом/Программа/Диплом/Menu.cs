using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Диплом
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sklad f1 = new Sklad();
            f1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zakaz f1 = new Zakaz();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rabot f1 = new Rabot();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Otchet f1 = new Otchet();
            f1.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            
        }
    }
}
