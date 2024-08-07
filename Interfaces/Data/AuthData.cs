namespace DaMid.Interfaces.Data {
    public class ILoginData {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }

    public class IRegisterData {
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}