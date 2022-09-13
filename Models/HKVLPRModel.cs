using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSDemo.Models
{
    internal class HKVLPRModel
    {
        public string DevTag { get; set; }
        public int status { get; set; }
        public int MsgType { get; set; }
        public string cardNo { get; set; }
        public string Date { get; set; }
        public string IP { get; set; }
        public string Image { get; set; }
    }
}
