namespace DaMid.Interfaces.Data {
    public class IAddSubjectData {
        public required string Name { get; set; }
    }

    public class IRemoveSubjectData {
        public required int Id { get; set; }
    }
}