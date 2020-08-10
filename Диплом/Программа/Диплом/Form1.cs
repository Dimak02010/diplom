using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Диплом
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            Menu M = new Menu();
            if (System.IO.File.Exists("enter.sqlite"))
            {
                
                SQLiteConnection sql = new SQLiteConnection(@"DataSource=enter.sqlite");
                SQLiteCommand sc = new SQLiteCommand();
                sc.Connection = sql;
                sql.Open();
                sc.CommandText = "select login from admin where id = 1";
                int i = 0;
                if (textBox1.Text == Convert.ToString(sc.ExecuteScalar())) { i++; }
                sc.CommandText = "select password from admin where id = 1";
                if (textBox2.Text == Convert.ToString(sc.ExecuteScalar())) { i++; }
                if (i < 2) { MessageBox.Show("Неправильный логин или пароль"); }
                if (i == 2) {  M.Show(); this.Hide(); }
                else { };
                sql.Close();
            }
            else { MessageBox.Show("Ошибка файла входа"); }
            
        }
    }
}
