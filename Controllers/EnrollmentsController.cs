using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBackend.Data;
using ProjectBackend.Models;
using ProjectBackend.DTOs;

namespace ProjectBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EnrollmentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Enrollment>> CreateEnrollment([FromBody] EnrollmentDto enrollmentDto)
    {
        var student = await _context.Students.FindAsync(enrollmentDto.StudentId);
        var course = await _context.Courses.FindAsync(enrollmentDto.CourseId);

        if (student == null || course == null) return NotFound("Aluno ou curso não encontrado.");

        var existingEnrollment = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentId == enrollmentDto.StudentId && e.CourseId == enrollmentDto.CourseId);

        if (existingEnrollment != null) return BadRequest("O aluno já está matriculado neste curso.");

        var enrollment = new Enrollment
        {
            StudentId = enrollmentDto.StudentId,
            CourseId = enrollmentDto.CourseId
        };

        _context.Enrollments.Add(enrollment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEnrollmentById), new { id = enrollment.Id }, enrollment);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Enrollment>> GetEnrollmentById(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();

        return Ok(enrollment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnrollment(int id)
    {
        var enrollment = await _context.Enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();

        _context.Enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByStudent(int studentId)
    {
        var courses = await _context.Enrollments
            .Where(e => e.StudentId == studentId)
            .Select(e => e.Course)
            .ToListAsync();

        return courses;
    }

    [HttpGet("course/{courseId}")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByCourse(int courseId)
    {
        var students = await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.Student)
            .ToListAsync();

        return students;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enrollment>>> GetAllEnrollments()
    {
        var enrollments = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .ToListAsync();

        return Ok(enrollments);
    }
}
