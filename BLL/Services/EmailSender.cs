using System.Net.Mail;
using System.Net;

namespace GUI.Services
{
    public class EmailSender
    {
        // Config your mail Address
        private const string USERNAME = "conghau31052004@gmail.com";

        // Config key pass
        private const string PASSWORD = "xdqzdhcrbmtryjyn";

        public readonly string Code = string.Empty;
        public readonly DateTime TimeCreate;
        public readonly string EmailTO;


        public EmailSender(string emailTO)
        {
            EmailTO = emailTO;
            TimeCreate = DateTime.Now;
            Code = GenerateCode();
            SendCodeEmail(emailTO, Code);
        }

        public EmailSender(string emailTO, string username)
        {
            EmailTO = emailTO;
            TimeCreate = DateTime.Now;
            Code = GenerateCode(10);
            SendInforEmail(emailTO, Code, username);
        }

        public static bool SendCodeEmail(string emailTO, string code)
        {

            // Config mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(USERNAME);
            mail.To.Add(emailTO);
            mail.Subject = "RESET PASSWORD CODE";
            mail.Body = $"<strong>{code}</strong> is your code.";
            mail.IsBodyHtml = true;

            // Config smtp to send mail => Can be change into a function
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(USERNAME, PASSWORD);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool SendInforEmail(string emailTO,string code, string username)
        {

            // Config mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(USERNAME);
            mail.To.Add(emailTO);
            mail.Subject = "ACCOUNT INFORMATION";
            mail.Body = $"Username: <strong>{username}</strong></br>" +
                        $"Password: <strong>{username}</strong></br>";
            mail.IsBodyHtml = true;

            // Config smtp to send mail => Can be change into a function
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(USERNAME, PASSWORD);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool CheckCode(string inputCode)
        {
            TimeSpan timeSpan = DateTime.Now.Subtract(TimeCreate);
            if (timeSpan.TotalMinutes > 2)
                return false;
            if (inputCode != Code)
                return false;
            return true;
        }
        public static string GenerateCode(int number = 6)
        {
            string code = string.Empty;
            int a = new int();
            Random random = new Random();
            for (int i = 0; i < number; i++)
            {
                a = random.Next(0, 9);
                code += a.ToString();
            }
            return code;
        }
    }
}
