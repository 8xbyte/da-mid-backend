using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    public enum UserRole {
        User, Admin
    }

    [Table("users")]
    public class UserModel : BaseModel {
        [Required]
        [Column("login")]
        public required string Login { get; set; }

        [Required]
        [Column("password")]
        public required string Password { get; set; }

        [Required]
        [Column("role")]
        public required UserRole Role { get; set; }

        [Required]
        [Column("registration_date")]
        public required DateTime RegistrationDate { get; set; }
    }
}