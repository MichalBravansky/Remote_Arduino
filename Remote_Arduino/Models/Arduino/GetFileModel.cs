using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remote_Arduino.Models.Arduino
{
    public class GetFileModel
    {
        public byte[] File { get; set; }
        public bool IsWaiting { get; set; }
        public int Id { get; set; }
    }
}
