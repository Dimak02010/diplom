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
using System.Drawing.Drawing2D;

namespace Диплом
{
    public partial class Analit : Form
    {
        public Analit()
        {
            InitializeComponent();
        }

        private void Predpros_Load(object sender, EventArgs e)
        {

            comboBox1.Items.AddRange(new string[] { "", "Приход деталей", "Выполненная работа" });
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            if (comboBox1.SelectedItem.ToString() == "Приход деталей")
            {
                cs.CommandText = "select Артикул from склад1";
                var m1 = cs.ExecuteReader();
                while (m1.Read())
                {
                    comboBox2.Items.Add(m1["Артикул"]);
                }
            }
            else
            {
                if (comboBox1.SelectedItem.ToString() == "Выполненная работа")
                {
                    cs.CommandText = "select ФИО from Рабочие";
                    var m1 = cs.ExecuteReader();
                    while (m1.Read())
                    {
                        comboBox2.Items.Add(m1["ФИО"]);
                    }
                }
                else 
                {
                    comboBox2.Items.Clear();
                }
            }
             
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {


            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics gr = Graphics.FromImage(bm);
            Random r = new Random();

            PointF[] mas = new PointF[25];
            for (int i = 0; i < 25; i++)
                mas[i] = new PointF(i, (float)Math.Round(r.Next(0, 10) + r.NextDouble(), 2));
            float cdx = (pictureBox1.Width - 90) / mas.Max(x => x.X);
            float cdy = (pictureBox1.Height - 90) / mas.Max(x => x.Y);
            int max = pictureBox1.Height - 25;

            Pen p = new Pen(Brushes.DarkRed, 3);
            p.EndCap = LineCap.ArrowAnchor;

            gr.DrawLine(p, (int)(0 * cdx) + 25, max, (int)(0 * cdx + 25), 0);
            gr.DrawLine(p, 0, (int)(max - 0 * cdy), (int)pictureBox1.Width - 25, (int)(max - 0 * cdy));

            for (float x = 0; x < mas.Max(k => k.X) + 1; x++)
            {

                gr.DrawLine(Pens.LightCoral, (int)(x * cdx) + 25, (int)pictureBox1.Height - 25, (int)(x * cdx) + 25, 0);
            }
            for (float x = 0; x < mas.Max(k => k.Y) + 1; x++)
            {
                gr.DrawString((x * 10).ToString(), new Font("Arial", 10), Brushes.Black, 0, (int)(max - x * cdy));
                gr.DrawLine(Pens.LightCoral, 0, (int)(max - x * cdy), (int)pictureBox1.Width - 25, (int)(max - x * cdy));
            }

            pictureBox1.Image = bm;

            SQLiteConnection sql = new SQLiteConnection("DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            int i1 = 0;
            int q = 0;
            DateTime d1 = new DateTime();
            d1 = DateTime.Now;
            string date = d1.Month.ToString() + "." + d1.Year.ToString();
            string date1;
            if (d1.Month - 1 == 0)
            {
                date1 = "12." + d1.Year.ToString();
            }
            else
            {
                date1 = (d1.Month - 1).ToString() + "." + d1.Year.ToString();
            }
            if (comboBox1.SelectedItem.ToString() == "Приход деталей")
            {
                try
                {
                    cs.CommandText = "Select Sum(Количество) from История where (Дата like '%" + date + "%') AND (Артикул = " + comboBox2.SelectedItem.ToString() + ")";
                    i1 = Convert.ToInt32(cs.ExecuteScalar());
                    cs.CommandText = "Select Sum(Количество) from История where (Дата like '%" + date1 + "%') AND (Артикул = " + comboBox2.SelectedItem.ToString() + ")";
                    q = Convert.ToInt32(cs.ExecuteScalar());
                    gr.DrawString(date1, new Font("Arial", 10), Brushes.Black, (int)(7 * cdx) + 25, max + 10);
                    gr.DrawString(date.ToString(), new Font("Arial", 10), Brushes.Black, (int)(14 * cdx) + 25, max + 10);

                    gr.FillRectangle(Brushes.Blue, (int)(7 * cdx) + 20, max - q, 60, q);
                    gr.FillRectangle(Brushes.Red, (int)(7 * cdx) + 160, max - i1, 60, i1);

                    pictureBox1.Image = bm;
                }
                catch (Exception)
                {
                    MessageBox.Show("Такой артикул не продавался");
                }
                finally { }
            }
                if (comboBox1.SelectedItem.ToString() == "Выполненная работа")
                {
                    try
                    {
                        cs.CommandText = "Select Количество from История where (Дата like '%" + date + "%') AND (ФИО = '" + comboBox2.SelectedItem.ToString() + "')";
                        i1 = Convert.ToInt32(cs.ExecuteScalar());
                        cs.CommandText = "Select Количество from История where (Дата like '%" + date1 + "%') AND (ФИО = '" + comboBox2.SelectedItem.ToString() + "')";
                        q = Convert.ToInt32(cs.ExecuteScalar());
                        gr.DrawString(date1, new Font("Arial", 10), Brushes.Black, (int)(7 * cdx) + 25, max + 10);
                        gr.DrawString(date.ToString(), new Font("Arial", 10), Brushes.Black, (int)(14 * cdx) + 25, max + 10);

                        gr.FillRectangle(Brushes.Blue, (int)(7 * cdx) + 20, max - q, 60, q);
                        gr.FillRectangle(Brushes.Red, (int)(7 * cdx) + 160, max - i1, 60, i1);

                        pictureBox1.Image = bm;
                    }
                catch (Exception)
                {
                    MessageBox.Show("Такой работник не работал");
                }
                finally { }
                
            }
        }
    }
}
