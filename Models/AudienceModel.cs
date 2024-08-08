using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("audiences")]
    public class AudienceModel : BaseModel {
        [Required]
        [StringLength(128)]
        [Column("name")]
        public required string Name { get; set; }
    }
}