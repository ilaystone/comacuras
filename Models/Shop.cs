using ComaCuras.web.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class Shop
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"[+-]?([0-9]*[.])?[0-9]+[:][+-]?([0-9]*[.])?[0-9]+",
            ErrorMessage = "Format: \"latitude:longitude\" exemple: \"34.01325:-6.83255\"")]
        public string Location { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        [EmailAddress]
        public string E_mail { get; set; }
        [MaxLength(10), MinLength(10)]
        [RegularExpression(@"[\d]*")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime HolidayEndDate { get; set; } = DateTime.Now.AddDays(-1);
        [DataType(DataType.Date)]
        public DateTime SubscriptionEndDate { get; set; } = DateTime.Now.AddDays(-1);
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int MyProperty { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Agent> Agent { get; set; }
        public ICollection<Schedule> Schedule { get; set; }
        public ICollection<Rate> Rates { get; set; }

        private readonly ComaCuraswebContext _context;
        public Shop(ComaCuraswebContext context)
        {
            _context = context;
        }

        public Shop() { }

        public string GetImageUrl()
        {
            return($"data:image/jpg;base64, {Convert.ToBase64String(Image)}");
        }

        public bool ServicesCheck()
        {
            return _context.Service.Where(s => s.ShopId == Id).Count() > 0;
        }
        public bool AgentsCheck()
        {
            var getAgents = _context.Agent.Where(a => a.ShopId == Id);
            foreach (var item in getAgents)
            {
                if (item.IsActive())

                    return true;
            }
            return false;
        }
    }
}
