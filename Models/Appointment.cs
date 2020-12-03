using ComaCuras.web.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string UserMail { get; set; }
        [Required]
        public int AgentNumber { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string Start { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string End { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int ShopId { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        private readonly ComaCuraswebContext _context;

        public Appointment() { }

        public Appointment(ComaCuraswebContext context)
        {
            _context = context;
        }

        public string GetAgentName()
        {
            return _context.Agent.Where(a => a.Id == this.AgentNumber).Select(a => a.Name).FirstOrDefault();
        }

        public string GetShopName()
        {
            return _context.Shop.Where(s => s.Id == ShopId).Select(s => s.Name).FirstOrDefault();
        }

    }
}
