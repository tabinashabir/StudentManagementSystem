using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public int StudentID { get; set; }

        public Course? Course { get; set; }

        public Student? Student { get; set; }
    }
}
