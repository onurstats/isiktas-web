﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("subjects")]
    public class Subject
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }

        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedOn { get; set; }
    }
}
