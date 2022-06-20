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

        [Required]
        public Course Course { get; set; }

        [Required]
        public Student Student { get; set; }
    }
}
