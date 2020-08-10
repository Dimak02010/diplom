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
    public partial class Rabotadd : Form
    {
        public Rabotadd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "select Табельный_номер from Рабочие";
            if (textBox1.Text == Convert.ToString(cs.ExecuteScalar())) { MessageBox.Show("Такой Табельный номер уже существует"); }
            else
            {
                cs.CommandText = "insert into Рабочие (Табельный_номер, ФИО, Вид_работы, Цех, Телефон, Статус, Зарплата) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox2.SelectedItem.ToString() + "','" + textBox5.Text + "') ";
                cs.ExecuteNonQuery();
            }
            sql.Close();
        }

        private void Rabotadd_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "", "Заготовка", "Токарная", "Фрезерная", "Термообработка", "Заточка предварительно", "Шлифовка", "Заточка окончательно" });
            comboBox2.Items.AddRange(new string[] { "", "Работает", "Уволен", "Выходной", "Отпуск" });
        }
    }
}
