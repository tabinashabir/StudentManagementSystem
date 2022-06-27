using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseID { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be atleast 3 characters long")]
        [MaxLength(15, ErrorMessage = "Limit of characters reached")]
        public string Title { get; set; }

        public TimeSpan Duration { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}