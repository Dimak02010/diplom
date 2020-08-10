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
    public partial class Zakazizm : Form
    {
        public Zakazizm()
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
            cs.CommandText = "update Заказ set Номер_заказа = '" + textBox1.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Артикул = '" + comboBox1.SelectedItem.ToString() + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Название = '" + label4.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Сырьё = '" + label6.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Табельный_номер = '" + comboBox2.SelectedIndex.ToString() + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set ФИО = '" + label9.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Вид_работы = '" + label11.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Количество = '" + textBox8.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Дата = '" + label14.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            cs.CommandText = "update Заказ set Стадия = '" + label16.Text + "' where Номер_заказа = '" + label17.Text + "'";
            cs.ExecuteScalar();
            sql.Close();
            label17.Text = textBox1.Text;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sklad bd = new Sklad();
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "insert into История (Номер_заказа, ФИО, Артикул, Название, Количество, Дата) values ('"+textBox1.Text+"','"+label9.Text+ "','"+comboBox1.SelectedItem.ToString()+"', '" + label4.Text+"','" + textBox8.Text + "', '" + label14.Text + "')";
            cs.ExecuteNonQuery();
            comboBox2.SelectedItem = "";
            label14.Text = DateTime.Now.ToString();
            cs.CommandText = "select Количество_стадий from склад1 where Артикул ="+  comboBox1.SelectedItem.ToString();
            int q = Convert.ToInt32(cs.ExecuteScalar());
            int i = Convert.ToInt32(label16.Text);
            if (Convert.ToInt32(label16.Text) <= q) { MessageBox.Show("Введите информацию для следующей стадии"); i++; label16.Text = i.ToString(); } 
            else 
            { 
                MessageBox.Show("Артикул выполнен");
                cs.CommandText = "Select Количество_на_складе from склад1 where Артикул =" + comboBox1.SelectedItem.ToString();
                int w = Convert.ToInt32(cs.ExecuteScalar());
                int r = Convert.ToInt32(textBox8.Text);
                int sum = w + r;
                cs.CommandText = "update Количество_на_складе from склад1 where Артикул = " + comboBox1.SelectedItem.ToString();
                this.Close();
                cs.CommandText = "delete from Заказ where Номер_заказа =" +label17.Text;
                
            }
            

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

        private void Zakazizm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
