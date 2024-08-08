using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("classes")]
    public class ClassModel : BaseModel {
        [Required]
        [StringLength(256)]
        [Column("name")]
        public required string Name { get; set; }
    }
}