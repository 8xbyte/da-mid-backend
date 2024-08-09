using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("groups")]
    public class GroupModel : BaseModel {
        [Required]
        [StringLength(16)]
        [Column("name")]
        public required string Name { get; set; }
    }
}