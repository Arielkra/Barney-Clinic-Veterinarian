using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


    public class mailSender
    {
    //פונקציות שמטפלות בכל מה שנוגע לאימייל
        static MailMessage mail = new MailMessage();
        public static bool SendMail(List<string> to, string subject, string message, List<Attachment> attachments)
        {
            try
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("guyariel75@gmail.com", "מרפאה וטרינרית בארני");
            foreach (string user in to)
            {
                mail.To.Add(user);

            }
            foreach (Attachment file in attachments)
                mail.Attachments.Add(file);
            mail.Subject = subject;

            mail.Body = "<b>" + message + "<h3><b><br><br>בתודה , המרפאה הוטרינרית בארני,באר שבע. טל : 08-6668888</h3></b></br>" + "\n" + @"<img src=""https://lh3.googleusercontent.com/pw/ACtC-3fuOb_Rvpf2Frpiw3ZTQeef2JxA7yJGJr_ulLuc367R8B_0vu5kDjhFCiJ1BAXWT5eAX7lS2rpgM56z8Cr18xnbTWSK7e5SmlkHQC761jqnWtb6UOKKmBuxqBOf_FmocdWHJJfdeTuvGcmAZurtTn9DpA=s72-no?authuser=0""/>";
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("guyariel75@gmail.com", "utmn awtv pfql cbsp");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            mail = new MailMessage();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        }

    public static bool SendMail2(List<string> to, string subject, string message)
    {
       try
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("guyariel75@gmail.com", "מרפאה וטרינרית בארני");
            foreach (string user in to)
            {
                mail.To.Add(user);

            }
        
            mail.Body = "<b>" + message + "<h3><b><br><br>בתודה , המרפאה הוטרינרית בארני,באר שבע. טל : 08-6668888</h3></b></br>" + "\n" + @"<img src=""https://lh3.googleusercontent.com/pw/ACtC-3fuOb_Rvpf2Frpiw3ZTQeef2JxA7yJGJr_ulLuc367R8B_0vu5kDjhFCiJ1BAXWT5eAX7lS2rpgM56z8Cr18xnbTWSK7e5SmlkHQC761jqnWtb6UOKKmBuxqBOf_FmocdWHJJfdeTuvGcmAZurtTn9DpA=s72-no?authuser=0""/>";
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("guyariel75@gmail.com", "utmn awtv pfql cbsp");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            mail = new MailMessage();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


}
   
