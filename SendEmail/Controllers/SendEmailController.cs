using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.IO;

namespace SendEmail.Controllers
{
    public class SendEmailController : Controller
    {
        // GET: SendEmail
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(SendEmail.Models.SendEmail sendEmail)
        {
            string path = "~/Content/Upload/" + Guid.NewGuid() + "/";
            if (ModelState.IsValid)
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("From to Mail adress");
                mailMessage.To.Add(new MailAddress(sendEmail.to));
                mailMessage.CC.Add(new MailAddress("CC to Mail adress"));
                mailMessage.Bcc.Add(new MailAddress("Bcc to mail adress"));

                mailMessage.Subject = sendEmail.subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = sendEmail.body;

                if (sendEmail.file[0] != null)
                {
                    Directory.CreateDirectory(Server.MapPath(path));
                    foreach (HttpPostedFileBase file in sendEmail.file)
                    {
                        string filePath = Server.MapPath(path + file.FileName);
                        file.SaveAs(filePath);

                        mailMessage.Attachments.Add(new Attachment(Server.MapPath(path + file.FileName)));
                    }
                }

                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("mailadress", "password");
                client.Host = "mailserver";

                try
                {
                    client.Send(mailMessage);
                    ViewBag.Result = "Mail Sent";
                }
                catch (Exception ex)
                {
                    ViewBag.Result = ex.Message;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Reset()
        {
            return RedirectToAction("Index");
        }
    }
}