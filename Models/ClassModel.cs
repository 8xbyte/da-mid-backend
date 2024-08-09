using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaMid.Models {
    [Table("classes")]
    public class ClassModel : BaseModel {
        [Required]
        [Column("subject_id")]
        public int SubjectId { get; set; }
        public SubjectModel Subject { get; set; }

        [Required]
        [Column("audience_id")]
        public int AudienceId { get; set; }
        public AudienceModel Audience { get; set; }

        [Required]
        [Column("teacher_id")]
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        [Required]
        [Column("group_id")]
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }

        [Required]
        [Column("date")]
        public required DateOnly Date { get; set; }

        [Required]
        [Column("time")]
        public required TimeOnly Time { get; set; }
    }
}