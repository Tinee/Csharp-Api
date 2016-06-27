using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmallDbEntities
{
    public class SmallService
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual List<SmallActivity> SmallActivities { get; set; }
    }
}
