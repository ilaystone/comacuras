using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required]
        public int Day { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string Start { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string End { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string Pstart { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string Pend { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
    }
}
