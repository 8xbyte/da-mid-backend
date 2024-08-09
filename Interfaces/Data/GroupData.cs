namespace DaMid.Interfaces.Data {
    public class IAddGroupData {
        public required string Name { get; set; }
    }

    public class IRemoveGroupData {
        public required int Id { get; set; }
    }
}