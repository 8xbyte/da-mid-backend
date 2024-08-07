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
        public string Login { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [Column("role")]
        public UserRole Role { get; set; }

        [Required]
        [Column("registration_date")]
        public DateTime RegistrationDate { get; set; }
    }
}