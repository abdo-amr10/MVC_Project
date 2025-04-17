using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Identity;

namespace Demo.BLL.Common.Services.EmailSettings
{
    public class EmailSettings : IEmailSettings
    {
        public void SendEmail(Email email)
        {

            var Client = new SmtpClient("smtp.gmail.com", 587);

            Client.EnableSsl = true;

            Client.Credentials = new NetworkCredential("amr838836@gmail.com", "rgzfrxpbwnumuove"); 

            Client.Send("doaaamin.route@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
