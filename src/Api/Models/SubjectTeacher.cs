using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("subjects_teachers")]
    public class SubjectTeacher
    {
        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }
        public Subject Subject { get; set; }

        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }
}
