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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.Office.Interop.Excel;


namespace Диплом
{
    public partial class Otchet : Form
    {
        public Otchet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand("select * from История", sql);
            sql.Open();
            SQLiteDataReader sdr = cs.ExecuteReader();
            System.Data.DataTable dt = new System.Data.DataTable();
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

        private void button4_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            SQLiteCommand sc = new SQLiteCommand("delete from История where ФИО = " + id, sql);
            sql.Open();
            sc.ExecuteNonQuery();
            sql.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            string date = DateTime.Now.ToString();
            var doc = new Document();
            var writer = PdfWriter.GetInstance(doc, new FileStream(@"Отчёт.pdf", FileMode.Create));
            BaseFont baseFont = BaseFont.CreateFont(@"ARIAL.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

            doc.Open();
            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("Отчёт: проделанная работа", font));
            iTextSharp.text.Phrase jo = new Phrase("", new iTextSharp.text.Font(baseFont, 11, iTextSharp.text.Font.BOLDITALIC, new BaseColor(Color.Red)));
            Paragraph r = new Paragraph(jo);
            cell.Colspan = dataGridView1.Columns.Count;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            r.Add(Environment.NewLine);
            table.AddCell(cell);
            for (int j = 0; j < dataGridView1.Rows.Count; j++)
            {
                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    if (dataGridView1.Rows[j].Cells[k].Value != null)
                        table.AddCell(new Phrase(dataGridView1.Rows[j].Cells[k].Value.ToString(), font));
                }
            }
            sql.Close();
            doc.Add(table);
            doc.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQLiteConnection sql = new SQLiteConnection(@"DataSource=bd.sqlite");
            SQLiteCommand cs = new SQLiteCommand();
            cs.Connection = sql;
            sql.Open();
            cs.CommandText = "delete from История";
            cs.ExecuteNonQuery();
            sql.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "")
            {
                SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
                SQLiteCommand cs = new SQLiteCommand("select * from История ", sql);
                sql.Open();
                SQLiteDataReader sdr = cs.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(sdr);
                dataGridView1.DataSource = dt;
                sdr.Close();
                sql.Close();
            }
            else
            {
                SQLiteConnection sql = new SQLiteConnection(@"DataSource = bd.sqlite");
                SQLiteCommand cs = new SQLiteCommand("select * from История order by " + comboBox1.SelectedItem.ToString(), sql);
                sql.Open();
                SQLiteDataReader sdr = cs.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(sdr);
                dataGridView1.DataSource = dt;
                sdr.Close();
                sql.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Analit f1 = new Analit();
            f1.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Номер";
            ExcelApp.Cells[1, 2] = "Номер заказа";
            ExcelApp.Cells[1, 3] = "ФИО";
            ExcelApp.Cells[1, 4] = "Артикул";
            ExcelApp.Cells[1, 5] = "Название";
            ExcelApp.Cells[1, 6] = "Количество";
            ExcelApp.Cells[1, 7] = "Дата";


            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    ExcelApp.Cells[j + 2, i + 1] = (dataGridView1[i, j].Value).ToString();
                }
            }
            ExcelApp.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Mail f1 = new Mail();
            f1.Show();
        }
    }
}
