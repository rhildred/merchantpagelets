using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace epicnetwork.rocks.Models
{
    public class Promotion
    {
        public int id { get; set; }
        public Merchant merchant { get; set; }
        public string code { get; set; }
        public int week { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public decimal value { get; set; }

    }
}