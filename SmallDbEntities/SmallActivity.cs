using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmallDbEntities
{
    public class SmallActivity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual List<SmallService> SmallServices { get; set; }
    }
}
