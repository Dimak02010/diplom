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
    public partial class Skladizm : Form
    {
        public Skladizm()
        {
            InitializeComponent();
        }

        private void Skladizm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sklad bd = new Sklad();
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
                cs.CommandText = "update склад1 set Артикул = '" + textBox1.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Название = '" + textBox2.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Размер = '" + textBox3.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Вид_сырья = '" + textBox4.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Сырьё = '" + textBox5.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Количество_сырья = '" + textBox6.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Количество_на_складе = '" + textBox7.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
                cs.CommandText = "update склад1 set Количество_стадий = '" + textBox8.Text + "' where Артикул = '" + label8.Text + "'";
                cs.ExecuteScalar();
            sql.Close();
            label8.Text = textBox1.Text;
            this.Close();
        }
    
    }
}
