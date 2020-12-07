using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[A-Z][a-zA-Z0-9\s]+",
                ErrorMessage = "n\'alphanumerique et capitalise")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"[0-9]+",
              ErrorMessage = @"cout en dirham")]
        public int Cost { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"[0-9]+",
            ErrorMessage = @"duration en minutes")]
        public int Duration { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [RegularExpression("[0-9]+",
                ErrorMessage = "pourcentage reduction de promotion")]
        [Range(0,100, ErrorMessage = "entre 0 - 100")]
        public int DiscountRatio { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<AgentService> Agents { get; set; }

        /*Methodes*/
        public bool HasDiscount() => DiscountRatio > 0;
    }
}
