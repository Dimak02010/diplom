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
using System.IO;

namespace Диплом
{
    public partial class Rabot : Form
    {
        public Rabot()
        {
            InitializeComponent();
        }

        private void Rabot_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand("select * from Рабочие", sql);
            sql.Open();
            //cs.CommandText = "drop table Рабочие";
            //cs.ExecuteNonQuery();
            //cs.CommandText = "CREATE TABLE Рабочие (Табельный_номер int primary key, ФИО text, Вид_работы text, Цех text, Телефон text ,Статус text, Зарплата int)";
            //cs.ExecuteNonQuery();
            SQLiteDataReader sdr = cs.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            dataGridView1.DataSource = dt;
            sdr.Close();
            sql.Close();
            int i = 0;
            comboBox1.Items.Add("");
            while (i < dataGridView1.Columns.Count)
            {
                comboBox1.Items.Add(Convert.ToString(dataGridView1.Columns[i].Name));
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Rabotadd f1 = new Rabotadd();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rabotizm bd = new Rabotizm();

            bd.comboBox1.Items.AddRange(new string[] { "", "Заготовка", "Токарная", "Фрезерная", "Термообработка", "Заточка предварительно", "Шлифовка", "Заточка окончательно" });
            bd.comboBox2.Items.AddRange(new string[] { "", "Работает", "Уволен", "Выходной", "Отпуск" });

            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "select Табельный_номер from Рабочие where Табельный_номер = " + id;
            bd.label8.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Табельный_номер from Рабочие where Табельный_номер = " + id;
            bd.textBox1.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select ФИО from Рабочие where Табельный_номер = " + id;
            bd.textBox2.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Вид_работы from Рабочие where Табельный_номер = " + id;
            bd.comboBox1.SelectedItem = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Цех from Рабочие where Табельный_номер = " + id;
            bd.textBox3.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Телефон from Рабочие where Табельный_номер = " + id;
            bd.textBox4.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Статус from Рабочие where Табельный_номер = " + id;
            bd.comboBox2.SelectedItem = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Зарплата from Рабочие where Табельный_номер = " + id;
            bd.textBox5.Text = cs.ExecuteScalar().ToString();
            sql.Close();
            bd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteCommand sc = new SQLiteCommand("delete from Рабочие where Табельный_номер = " + id, sql);
            sql.Open();
            sc.ExecuteNonQuery();
            sql.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "")
            {
                SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
                SQLiteCommand cs = new SQLiteCommand("select * from Рабочие ", sql);
                sql.Open();
                SQLiteDataReader sdr = cs.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sdr);
                dataGridView1.DataSource = dt;
                sdr.Close();
                sql.Close();
            }
            else
            {
                SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
                SQLiteCommand cs = new SQLiteCommand("select * from Рабочие order by " + comboBox1.SelectedItem.ToString(), sql);
                sql.Open();
                SQLiteDataReader sdr = cs.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sdr);
                dataGridView1.DataSource = dt;
                sdr.Close();
                sql.Close();
            }
        }
    }
}
