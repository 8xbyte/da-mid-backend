using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("classes")]
    public class ClassModel : BaseModel {
        [Required]
        [StringLength(128)]
        public required string Name { get; set; }
    }
}