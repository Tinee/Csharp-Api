using System.ComponentModel.DataAnnotations;

namespace SmallDbEntities
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public int CustomerId { get; set; }

        public string ServiceId { get; set; }

        public int AgreementId { get; set; }

        public string ActivityId { get; set; }

        public int ProjectId { get; set; }

        public int OrderId { get; set; }

    }
}
