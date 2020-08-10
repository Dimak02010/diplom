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
    public partial class Zakaz : Form
    {
        public Zakaz()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand("select * from Заказ", sql);
            sql.Open();
            //cs.CommandText = "drop table История";
            //cs.ExecuteNonQuery();
            //cs.CommandText = "CREATE TABLE Заказ (Номер_заказа int primary key," +
            //    " Артикул int," +
            //    "Название text," +
            //    "Сырьё text," +
            //    "Табельный_номер int," +
            //    "ФИО text," +
            //    "Вид_работы text," +
            //    "Дата text," +
            //    "Количество int," +
            //    "Количество_стадий int," +
            //    "Стадия int," +
            //    "foreign key (Артикул) references Склад1(Артикул)  " +
            //    "foreign key (Название) references Склад1(Название) " +
            //    "foreign key (Сырьё) references Склад1(Сырьё) " +
            //    "foreign key (Табельный_номер) references Рабочие(Табельный_номер) " +
            //    "foreign key (ФИО) references Рабочие(ФИО) " +
            //    "foreign key (Вид_работы) references Рабочие(Вид_работы) " +
            //    "foreign key (Количество_стадий) references Склад1(Количество_стадий)) ";
            //cs.ExecuteNonQuery();
            //cs.CommandText = "CREATE TABLE История (Номер integer primary key autoincrement," +
            //    "Номер_заказа int," +

            //    "ФИО text," +

            //    "Артикул int," +

            //    "Название text," +

            //    "Количество int," +

            //    "Дата text," +
            //    "foreign key (Номер_заказа) references Заказ(Номер_заказа)  " +
            //    "foreign key (ФИО) references Заказ(ФИО) " +
            //     "foreign key (Артикул) references Заказ(Артикул) " +
            //     "foreign key (Название) references Заказ(Название) " +
            //     "foreign key (Количество) references Заказ(Количество) " +
            //    "foreign key (Дата) references Заказ(Дата))";
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
            Zakazadd f1 = new Zakazadd();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Zakazizm bd = new Zakazizm();

            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            SQLiteCommand cs1 = new SQLiteCommand();
            cs.Connection = sql;
            cs1.Connection = sql;
            sql.Open();

            cs.CommandText = "select Артикул from склад1 ";
            var m1 = cs.ExecuteReader();

            while (m1.Read())
            {
                bd.comboBox1.Items.Add(m1["Артикул"]);
            }
            cs1.CommandText = "select Табельный_номер from Рабочие";
            var m2 = cs1.ExecuteReader();

            while (m2.Read())
            {
                bd.comboBox2.Items.Add(m2["Табельный_номер"]);
            }
            m1.Close();
            m2.Close();

            cs.CommandText = "select Номер_заказа from Заказ where Номер_заказа = " + id;
            bd.label17.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Номер_заказа from Заказ where Номер_заказа = " + id;
            bd.textBox1.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Артикул from Заказ where Номер_заказа = " + id;
            m1 = cs.ExecuteReader();
            m1.Read();
            bd.comboBox1.SelectedItem = m1["Артикул"];
            m1.Close();
            cs.CommandText = "select Название from Заказ where Номер_заказа = " + id;
            bd.label4.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Сырьё from Заказ where Номер_заказа = " + id;
            bd.label6.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Табельный_номер from Заказ where Номер_заказа = " + id;
            m1 = cs.ExecuteReader();
            m1.Read();
            bd.comboBox2.SelectedItem = m1["Табельный_номер"];
            m1.Close();
            cs.CommandText = "select ФИО from Заказ where Номер_заказа = " + id;
            bd.label9.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Вид_работы from Заказ where Номер_заказа = " + id;
            bd.label11.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Количество from Заказ where Номер_заказа = " + id;
            bd.textBox8.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Дата from Заказ where Номер_заказа = " + id;
            bd.label14.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Стадия from Заказ where Номер_заказа = " + id;
            bd.label16.Text = cs.ExecuteScalar().ToString();
            sql.Close();
            bd.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteCommand sc = new SQLiteCommand("delete from Заказ where Номер_заказа = " + id, sql);
            sql.Open();
            sc.ExecuteNonQuery();
            sql.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "")
            {
                SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
                SQLiteCommand cs = new SQLiteCommand("select * from Заказ ", sql);
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
                SQLiteCommand cs = new SQLiteCommand("select * from Заказ order by " + comboBox1.SelectedItem.ToString(), sql);
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
