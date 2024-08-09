namespace DaMid.Interfaces.Data {
    public class IAddClassData {
        public required int SubjectId { get; set; }
        public required int AudienceId { get; set; }
        public required int TeacherId { get; set; }
        public required int GroupId { get; set; }
        public required DateOnly Date { get; set; }
        public required TimeOnly Time { get; set; }
    }

    public class IRemoveClassData {
        public required int Id { get; set; }
    }
}