using Demo.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public static class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("ranamhakim@gmail.com", "Rana123$"); 
			client.Send("ranamhakim@gmail.com", email.To, email.Subject, email.Body);
        }
	}
}
