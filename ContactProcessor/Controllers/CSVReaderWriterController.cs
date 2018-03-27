using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ContactProcessor.Email;
using ContactProcessor.IO;
using ContactProcessor.Models;

namespace ContactProcessor.Controllers
{
    public class CSVReaderWriterController : Controller
    {
        IStreamService streamService;
        IFileService fileService;
        IEmailService emailService;

        public CSVReaderWriterController(IStreamService streamService, IFileService fileService, IEmailService emailService)
        {
            this.streamService = streamService;
            this.fileService = fileService;
            this.emailService = emailService;
        }

        #region Read

        [HttpGet]
        public ActionResult Read()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Read(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                string folderPath = Server.MapPath(ConfigurationManager.AppSettings["UploadFolderPath"]);
                var fileName = string.Format("{0}{1}{2}",
                    Path.GetFileNameWithoutExtension(file.FileName),
                    Guid.NewGuid(),
                    Path.GetExtension(file.FileName));

                // save file
                var fileBytes = streamService.StreamToByteArray(file.InputStream, file.ContentLength);
                fileService.WriteFile(folderPath, fileName, fileBytes);

                // read file content
                var fileContent = Encoding.Default.GetString(fileBytes);
                
                return View("DisplayInput", new DisplayInputViewModel()
                {
                    FileName = fileName,
                    Contacts = GetContacts(fileContent)
                });
            }
            return View();
        }

        #endregion

        #region Write

        [HttpGet]
        public ActionResult Write(string filename)
        {
            var model = new NewContactViewModel("", "", "", "", filename);
            return View(model);
        }

        [HttpPost]
        public ActionResult Write(NewContactViewModel model)
        {
            string folderPath = Server.MapPath(ConfigurationManager.AppSettings["UploadFolderPath"]);

            // append content to file
            fileService.AppendToFile(folderPath, model.FileName, model.ToCSVString());

            // read file content
            var fileContent = fileService.ReadFileContent(folderPath, model.FileName);

            return View("DisplayInput", new DisplayInputViewModel()
            {
                FileName = model.FileName,
                Contacts = GetContacts(fileContent)
            });
        }

        #endregion

        #region Process

        public ActionResult Process(string fileName)
        {
            string hostName = ConfigurationManager.AppSettings["Host"];
            string emailFrom = ConfigurationManager.AppSettings["From"];
            string subject = ConfigurationManager.AppSettings["Subject"];
            string body = ConfigurationManager.AppSettings["Body"];

            string folderPath = Server.MapPath(ConfigurationManager.AppSettings["UploadFolderPath"]);

            // read file content
            var fileContent = fileService.ReadFileContent(folderPath, fileName);

            // get contacts
            foreach (var contact in GetContacts(fileContent))
            {
                try
                {
                    if (contact.Email.Contains("@"))
                    {
                        emailService.Send(hostName, emailFrom, contact.Email, subject, body);
                    }
                    else if (contact.PhoneNumber.Contains("07"))
                    {
                        //TODO: send text message
                    }
                }
                catch (Exception) { return Content("Email Failed"); }
            }

            return Content("Email Sent");
        }

        #endregion

        private List<ContactViewModel> GetContacts(string fileContent)
        {
            var contacts = new List<ContactViewModel>();
            using (var reader = new StringReader(fileContent))
            {
                reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    contacts.Add(new ContactViewModel(line));
                }
            }
            return contacts;
        }
    }
}