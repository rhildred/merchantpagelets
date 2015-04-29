using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace epicnetwork.rocks.Models
{
    public class MerchantContext : DbContext
    {
        public MerchantContext() : base("Merchants") { }

        public System.Data.Entity.DbSet<epicnetwork.rocks.Models.Merchant> Merchants { get; set; }

        public System.Data.Entity.DbSet<epicnetwork.rocks.Models.Promotion> Promotions { get; set; }

        public System.Data.Entity.DbSet<epicnetwork.rocks.Models.Pagelet> Pagelets { get; set; }

    }
}