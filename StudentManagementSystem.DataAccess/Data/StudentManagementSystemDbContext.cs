using StudentManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StudentManagementSystem.DataAccess.Data
{
    public class StudentManagementSystemDbContext : IdentityDbContext
    {
        public StudentManagementSystemDbContext(DbContextOptions<StudentManagementSystemDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
