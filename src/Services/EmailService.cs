using System.Net;
using System.Net.Mail;

namespace FluxusApi.Services;

public class EmailService
{
    public void Send(string subject, string body)
    {
        var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);

        smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;

        var mail = new MailMessage();

        mail.From = new MailAddress("felipe@alafiabr.com", "Equipe FluxusApp");
        mail.To.Add(new MailAddress(Configuration.Smtp.TrelloEmail, "TrelloBoard"));
        mail.Body = body;
        mail.Subject = subject;
        mail.IsBodyHtml = true;

        try
        {
            smtpClient.SendAsync(mail,null);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}