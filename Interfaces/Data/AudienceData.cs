namespace DaMid.Interfaces.Data {
    public class IAddAudienceData {
        public required string Name { get; set; }
    }

    public class IRemoveAudienceData {
        public required int Id { get; set; }
    }
}