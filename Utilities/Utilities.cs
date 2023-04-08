using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Website.Utility
{
    public static class Extensions
    {
        public static string ExceptionHandle(this Exception ex)
        {
            if (ex.InnerException != null)
                return ExceptionHandle(ex.InnerException);
            return ex.Message;
        }
    }

    public class Utilities
    {
        public static bool IsArabic
        {
            get
            {
                if (HttpContext.Current != null && HttpContext.Current.Session != null &&
                    HttpContext.Current.Session["DefaultLang"] != null)
                {
                    return HttpContext.Current.Session["DefaultLang"].ToString().StartsWith("ar");
                }
                else
                {
                    //Get User Default Language
                    return true;// GetDefaultLanguage();
                }
            }
        }

        public static void SendMail(string subject , string toEmail , string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage();

                mailMessage.To.Add(new MailAddress(toEmail));
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                mailMessage.Subject = "Automatic reply- Don't Replay to this email";

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static bool IsUserAuthenticatedOnWebsite => HttpContext.Current?.Session["IsActiveUser"] != null;

        public static string UserLoginName => HttpContext.Current?.Session["UserName"]?.ToString();
    }
 
}