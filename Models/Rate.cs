using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public string ClientMail { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
