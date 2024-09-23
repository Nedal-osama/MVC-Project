using MVC_03.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace MVC_03.PL.Helpers
{
	public class EmailSeetings
	{
		public static void sendEmail(Email email)
		{
			var clint = new SmtpClient("sntp.gmail.com", 587);
			clint.Credentials = new NetworkCredential("nedalosama3@gmail.com", "Nedalosama.2002");
			clint.Send("nedalosama3@gmail.com", email.Reciepints, email.Subject, email.Body);
		}
	}
}
