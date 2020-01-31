using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Api.Models
{
    [Table("teachers")]
    public class Teacher
    {
        public Guid Id { get; set; }
        [Required]
        public String Name {get;set;}
        [Required]
        public String LastName{ get; set; }
        public string FullName => Name + " " + LastName;
        [Required]
        public String Tck { get; set; }
        public String Tel { get; set; }

        public virtual ICollection<SubjectTeacher> SubjectTeachers { get; set; }

    }

    public class TeacherShortList
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; }
        public virtual Guid[] Subjects { get; set; }
    }

    public class TeacherDetail
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public string FullName => Name + " " + LastName;
        public String Tck { get; set; }
        public String Tel { get; set; }

        public virtual Guid[] Subjects { get; set; }

    }

}
