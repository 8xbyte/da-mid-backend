using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("teachers")]
    public class TeacherModel : BaseModel {
        [Required]
        [StringLength(64)]
        [Column("name")]
        public required string Name { get; set; }

        [Required]
        [StringLength(64)]
        [Column("surname")]
        public required string Surname { get; set; }
    }
}