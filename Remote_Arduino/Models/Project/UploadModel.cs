using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remote_Arduino.Models.Project
{
    public class UploadModel
    {
        public IFormFile file { set; get; }
    }
}
