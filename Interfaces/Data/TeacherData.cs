namespace DaMid.Interfaces.Data {
    public class IAddTeacherData {
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }

    public class IRemoveTeacherData {
        public required int Id { get; set; }
    }
}