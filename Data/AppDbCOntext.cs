using Microsoft.EntityFrameworkCore;
using ProjectBackend.Models;

namespace ProjectBackend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; } 
    public DbSet<Enrollment> Enrollments { get; set; }
}
