using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remote_Arduino.Data
{
    public class Code
    {
        public int Code_ID { get; set; }
        public byte[] Content { get; set; }
        public string User_ID { get; set; }
        public DateTime UploadDate { get; set; }
        public bool Sent { get; set; }
        public byte[] Message { get; set; }
    }
}
