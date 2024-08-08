using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("subjects")]
    public class SubjectModel : BaseModel {
        [Required]
        [StringLength(256)]
        [Column("name")]
        public required string Name { get; set; }
    }
}