using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeShield.Models
{
    public class RequestModel
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Approveflag { get; set; }
        public string RequestGUID { get; set; }

        public string UserName { get; set; }        
        public string Product1 { get; set; }

        public DateTime RequestTime { get; set; }


    }
}