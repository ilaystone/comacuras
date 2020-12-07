using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComaCuras.web.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z-\s]*",
                    ErrorMessage = "n'autorize que alphabet et \'-\'")]
        public string Name { get; set; }

        public ICollection<Shop> Shops { get; set; }
    }
}
