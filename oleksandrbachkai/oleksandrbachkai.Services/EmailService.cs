using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace oleksandrbachkai.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage identityMessage)
        {                        
            var message = new MailMessage("oleksandrbachkaiweb@gmail.com", identityMessage.Destination)
            {
                Subject = identityMessage.Subject,
                Body = identityMessage.Body
            };
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("oleksandrbachkaiweb@gmail.com", "Alex142426009")
            };
            await client.SendMailAsync(message);
        }
    }
}