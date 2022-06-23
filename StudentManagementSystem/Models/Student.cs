using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be atleast 3 characters long")]
        [MaxLength(15, ErrorMessage = "Limit of characters reached")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Must be atleast 3 characters long")]
        [MaxLength(15, ErrorMessage = "Limit of characters reached")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "This is an invalid email address")]
        public string EmailAddress { get; set; }
        
        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone no. must be numeric")]
        [MinLength(10, ErrorMessage = "Phone no. must contain 10 digits")]
        [MaxLength(10, ErrorMessage = "Phone no. cannot contain more than 10 digits")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }
        
        [Required]
        [DisplayName("Enrollment Date")]
        public DateTime EnrollmentDate { get; set; } = DateTime.Now.Date;

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}