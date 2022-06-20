using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int CourseID { get; set; }
        [Required]
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}