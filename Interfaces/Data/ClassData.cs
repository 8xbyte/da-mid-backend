namespace DaMid.Interfaces.Data {
    public class IAddClassData {
        public required string Name { get; set; }
    }

    public class IRemoveClassData {
        public required int Id { get; set; }
    }
}