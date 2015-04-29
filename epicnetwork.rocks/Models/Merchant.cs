using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace epicnetwork.rocks.Models
{
    public class Merchant
    {
        public int id { get; set; }
        public string name { get; set; }
        public string formatted_address { get; set; }
        public string formatted_phone_number { get; set; }
        public string image { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
    }
}