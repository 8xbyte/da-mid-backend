using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    public class BaseModel {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
    }
}