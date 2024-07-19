namespace DaMid.Interfaces.Options {
    public class IJwtOptions {
        public string SecurityKey { get; set; }
        public string EncryptionAlgorithm { get; set; }
        public double Expiration { get; set; }
        public class IFieldsOptions {
            public string UserId { get; set; }
            public string Expiration { get; set; }
        }
        public IFieldsOptions Fields { get; set; }
    }
}