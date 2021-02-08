using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Remote_Arduino.Data;
using Remote_Arduino.Models.Arduino;

namespace Remote_Arduino.Controllers
{
    [Authorize]
    public class ArduinoController : Controller
    {
        ApplicationDbContext DbContext { get; set; }
        public string File()
        {
            var file = DbContext.Set<Code>().Where(x => !x.Sent).OrderBy(x=>x.UploadDate);

            if (!file.Any())
            {
                return JsonConvert.SerializeObject(new GetFileModel
                {
                    IsWaiting = false
                });
            }
            return JsonConvert.SerializeObject(new GetFileModel
            {
                Id = file.First().Code_ID,
                File = file.First().Content,
                IsWaiting = true
            });
        }

        public ArduinoController(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        [HttpPost]
        public void File(int id, string message)
        {
            var code=DbContext.Set<Code>().Where(x => x.Code_ID == id).First();
            code.Message = Encoding.ASCII.GetBytes(message);
            code.Sent = true;
            DbContext.Set<Code>().Update(code);
            DbContext.SaveChanges();
        }
    }
}