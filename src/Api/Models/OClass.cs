using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("classes")]
    public class OClass
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedOn { get; set; }
    }
}
