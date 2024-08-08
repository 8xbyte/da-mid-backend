using DaMid.Models;

namespace DaMid.Interfaces {
    public class ITokenPayload {
        public required int UserId { get; set; }
        public required UserRole Role { get; set; }
    }
}