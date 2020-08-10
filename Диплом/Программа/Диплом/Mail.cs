using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Диплом
{
    public partial class Mail : Form
    {
        public Mail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MailAddress from = new MailAddress(textBox1.Text, "ООО Инструмент-Сервис");
            MailAddress to = new MailAddress(textBox2.Text);
            MailMessage m = new MailMessage(from, to);
            m.Attachments.Add(new Attachment(@"Отчёт.pdf"));
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 465);
            smtp.Credentials = new NetworkCredential(textBox1.Text, textBox3.Text);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
