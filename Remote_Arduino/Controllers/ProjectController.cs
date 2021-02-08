using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Remote_Arduino.Data;
using Remote_Arduino.Models.Project;

namespace Remote_Arduino.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        ApplicationDbContext DbContext { get; set; }
        public IActionResult Index()
        {
            bool canRun = !DbContext.Set<Code>().Where(x => x.Sent == false).Any();
            return View(new ProjectViewModel { CanRun= canRun });
        }

        public ProjectController(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        [HttpPost]
        [RequestSizeLimit(20485760)]
        public IActionResult Upload(UploadModel model)
        {
            if (!DbContext.Set<Code>().Where(x => x.Sent==false).Any())
            {
                byte[] fileBytes;
                using (var ms = new MemoryStream())
                {
                    model.file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                var uploadDate = DateTime.Now;

                var identity = this.User.Identity.Name.ToString();
                var user = DbContext.Set<IdentityUser>().Where(x => x.Email == identity).FirstOrDefault();
                var newCode = new Code { Content = fileBytes, Sent = false, UploadDate = uploadDate, User_ID = user.Id };
                DbContext.Set<Code>().Add(newCode);
                DbContext.SaveChanges();
                return RedirectToAction(nameof(Manage), new { id = newCode.Code_ID });
            }

            return RedirectToAction("Index");
        }

        public string GetInfo(int id)
        {
            var identity = this.User.Identity.Name.ToString();
            var user = DbContext.Set<IdentityUser>().Where(x => x.Email == identity).FirstOrDefault();

            var myFile= DbContext.Set<Code>().Where(x => x.Code_ID== id & x.User_ID==user.Id).First();

            var model = new FileModel
            {
                Sent=myFile.Sent,
                Message = myFile.Message!=null ? System.Text.Encoding.UTF8.GetString(myFile.Message) : ""
            };

            return JsonConvert.SerializeObject(model);
        }

        public ActionResult Manage(int id)
        {
            return View(new ManageViewModel { Id=id });
        }
    }
}