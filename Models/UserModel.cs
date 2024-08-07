namespace DaMid.Models {
    public class UserModel : BaseModel {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}