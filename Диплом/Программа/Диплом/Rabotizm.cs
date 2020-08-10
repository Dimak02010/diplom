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
    public partial class Rabotizm : Form
    {
        public Rabotizm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sklad bd = new Sklad();
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "select Табельный_номер from Рабочие where Табельный_номер =" + label8.Text;
                cs.CommandText = "update Рабочие set Табельный_номер = '" + textBox1.Text + "' where Табельный_номер = '" + label8.Text + "'";
                cs.ExecuteScalar();
            cs.CommandText = "update Рабочие set ФИО = '" + textBox2.Text + "' where Табельный_номер = '" + label8.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Рабочие set Вид_работы = '" + comboBox1.SelectedItem.ToString() + "' where Табельный_номер = '" + label8.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Рабочие set Цех = '" + textBox3.Text + "' where Табельный_номер = '" + label8.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Рабочие set Телефон = '" + textBox4.Text + "' where Табельный_номер = '" + label8.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Рабочие set Статус = '" + comboBox2.SelectedItem.ToString() + "' where Табельный_номер = '" + label8.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Рабочие set Зарплата = '" + textBox5.Text + "' where Табельный_номер = '" + label8.Text + "'";
            cs.ExecuteScalar();
            sql.Close();
            label8.Text = textBox1.Text;
            this.Close();
        }
    }
}
