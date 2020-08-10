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
    public partial class Skladadd : Form
    {
        public Skladadd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "select Артикул from склад1";
            if (textBox1.Text == Convert.ToString(cs.ExecuteScalar())) { MessageBox.Show("Такой Артикул уже существует"); }
            else
            {
                cs.CommandText = "insert into склад1 (Артикул, Название, Размер, Вид_сырья, Сырьё, Количество_сырья, Количество_на_складе, Количество_стадий) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','"+ textBox8.Text +"') ";
                 cs.ExecuteNonQuery();
            }
                sql.Close();

        }
    }
}
