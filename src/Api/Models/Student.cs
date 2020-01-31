using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("students")]
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String LastName { get; set; }
        public string FullName => Name + " " + LastName;
        [Required]
        public String Tck { get; set; }
        public String Tel { get; set; }

        public virtual OClass OClass { get; set; }


        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedOn { get; set; }
    }

    public class StudentShortList
    {
        public Guid Id { get; set; }
        public String Fullname { get; set; }
        public String Tck { get; set; }
        
        public OClass OClass { get; set; }
    }
}
