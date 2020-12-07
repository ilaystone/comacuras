using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class Agent
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[A-Z][a-zA-Z\s]+",
                ErrorMessage = "ecrire nom capitalse (ex : Ahmed)")]
        [MaxLength(50)]
        public string Name { get; set; }
        public string SevicesList { get; set; } // contains table
        [DataType(DataType.Date)]
        public DateTime HolidayEndDate { get; set; } = DateTime.Now.AddDays(-1);
        public byte[] Image { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }
        public ICollection<AgentService> Services { get; set; }

        /*Methodes*/
        public bool IsActive()
        {
            return DateTime.Compare(DateTime.Now, HolidayEndDate) > 0 &&
                    !string.IsNullOrEmpty(SevicesList);
        }
    }
}
