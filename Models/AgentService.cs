using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class AgentService
    {
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
