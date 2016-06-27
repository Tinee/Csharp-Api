using System.ComponentModel.DataAnnotations;

namespace SmallDbEntities
{
    public class SmallCustomer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
