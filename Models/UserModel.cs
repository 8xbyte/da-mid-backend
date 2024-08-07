namespace DaMid.Models {
    public enum UserRole {
        User, Admin
    }

    public class UserModel : BaseModel {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}