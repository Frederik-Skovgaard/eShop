using DataLayer;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class MailService : IMail
    {
        public void SendCheck(string email)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                #region Credentials
                smtp.Credentials = new NetworkCredential("min email", "mine admin kode");
                #endregion

                MailMessage message = new MailMessage();
                message.To.Add(email);
                message.Subject = "Confrimation from eShop";
                message.Body = "You buy stuff. Good for you!";
                message.From = new MailAddress("Tes@gmail.com");
                smtp.Send(message);
            }
        }
    }
}
