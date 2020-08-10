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
using System.Data.OleDb;

namespace Диплом
{
    public partial class Zakazadd : Form
    {
        public Zakazadd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "select Количество_стадий from склад1 where Артикул =" + comboBox1.SelectedItem.ToString();
            int i = Convert.ToInt32(cs.ExecuteScalar());
            
            cs.CommandText = "select Номер_заказа from Заказ";
            if (textBox1.Text == Convert.ToString(cs.ExecuteScalar())) { MessageBox.Show("Такой Номер уже существует"); }
            else
            {
                cs.CommandText = "insert into Заказ (Номер_заказа, Артикул, Название, Сырьё, Табельный_номер, ФИО, Вид_работы,  Количество, Дата, Количество_стадий, Стадия) values ('" + textBox1.Text + "', '" + comboBox1.SelectedItem.ToString() + "','" + label4.Text + "','" + label6.Text + "','" + comboBox2.SelectedItem.ToString() +"', '" + label9.Text + "','" + label11.Text + "', '" + textBox8.Text + "', '" + label14.Text + "','" + i + "','"+label16.Text+"')";
                    
            }
            cs.ExecuteNonQuery();
            sql.Close();
        }    

        private void Zakazadd_Load(object sender, EventArgs e)
        {
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
                comboBox1.Items.Add(m1["Артикул"]);
            }
            cs1.CommandText = "select Табельный_номер from Рабочие";
            var m2 = cs1.ExecuteReader();
           
            while (m2.Read())
            {
                comboBox2.Items.Add(m2["Табельный_номер"]);
            }
            sql.Close();
            label14.Text = DateTime.Now.ToString();
            label16.Text = "1";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            
            if (comboBox1.SelectedItem.ToString() != "")
            {
                cs.CommandText = "select Название from склад1 where Артикул =" + comboBox1.SelectedItem.ToString();
                label4.Text = cs.ExecuteScalar().ToString();
            }

            else { label4.Text = ""; label6.Text = ""; }



            if (comboBox1.SelectedItem.ToString() != "")
            {
                cs.CommandText = "select Сырьё from склад1 where Артикул =" + comboBox1.SelectedItem.ToString();
                label6.Text = cs.ExecuteScalar().ToString();
            }
            else { label4.Text = ""; label6.Text = ""; }

            sql.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            if (comboBox2.SelectedItem.ToString() != "")
            {
                cs.CommandText = "select ФИО from Рабочие where Табельный_номер =" + comboBox2.SelectedItem.ToString();
                label9.Text = cs.ExecuteScalar().ToString();
            }
            else { label9.Text = ""; label11.Text = ""; }
            if (comboBox2.SelectedItem.ToString() != "")
            {
                cs.CommandText = "select Вид_работы from Рабочие where Табельный_номер =" + comboBox2.SelectedItem.ToString();
                label11.Text = cs.ExecuteScalar().ToString();
            }
            else { label9.Text = ""; label11.Text = ""; }
            sql.Close();
        }
    }
}
