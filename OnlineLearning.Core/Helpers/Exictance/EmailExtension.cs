using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Helpers.Exictance
{
    public static class EmailExtension
    {
        public static string SendEmail(string inputEmail, string subject, string body)
        {

            string returnString = "";

            try
            {
                MailMessage email = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";

                // set up the Gmail server
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("gullarabidova@gmail.com", "r a y r h h p h b m m p o a t c");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                // draft the email
                MailAddress fromAddress = new MailAddress("gullarabidova@gmail.com");
                email.From = fromAddress;
                email.To.Add(inputEmail);
                email.Subject = subject;
                email.Body = body;
                email.IsBodyHtml = true;

                smtp.Send(email);

                returnString = "Success! Please check your e-mail.";
            }
            catch (Exception ex)
            {
                returnString = "Error: " + ex.ToString();
            }
            return returnString;
        }

    }
}
