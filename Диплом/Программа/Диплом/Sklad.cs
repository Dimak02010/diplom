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
    public partial class Sklad : Form
    {
        public Sklad()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand("select * from склад1",sql);
            sql.Open();
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
            Skladadd f1 = new Skladadd();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Skladizm bd = new Skladizm();

            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "select Артикул from склад1 where Артикул = " + id;
            bd.label8.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Артикул from склад1 where Артикул = " + id;
            bd.textBox1.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Название from склад1 where Артикул = " + id;
            bd.textBox2.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Размер from склад1 where Артикул = " + id;
            bd.textBox3.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Вид_сырья from склад1 where Артикул = " + id;
            bd.textBox4.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Сырьё from склад1 where Артикул = " + id;
            bd.textBox5.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Количество_сырья from склад1 where Артикул = " + id;
            bd.textBox6.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Количество_на_складе from склад1 where Артикул = " + id;
            bd.textBox7.Text = cs.ExecuteScalar().ToString();
            cs.CommandText = "select Количество_стадий from склад1 where Артикул = " + id;
            bd.textBox8.Text = cs.ExecuteScalar().ToString();
            sql.Close();
            bd.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteCommand sc = new SQLiteCommand("delete from склад1 where Артикул = " + id, sql);
            sql.Open();
            sc.ExecuteNonQuery();
            sql.Close();
        }

        private void Sklad_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "")
            {
                SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
                SQLiteCommand cs = new SQLiteCommand("select * from склад1 ", sql);
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
                SQLiteCommand cs = new SQLiteCommand("select * from склад1 order by " + comboBox1.SelectedItem.ToString(), sql);
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
