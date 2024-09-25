using Microsoft.AspNetCore.Mvc;
using Primer_21.Interfaces.StudentInterfaces;
using Primer_21.Filters.StudentFilters;
using Microsoft.AspNetCore.Http;
using Primer_21.Models;
using Primer_21.Database;

using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;



namespace Primer_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        private StudentDbContext _context;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, StudentDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }


        [HttpPost("GetStudentsByGroupID")]
        public async Task<IActionResult> GetStudentsByGroupIDAsync(StudentsGroupFilterID filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupIDAsync(filter, cancellationToken);

            return Ok(students);
        }
       
        [HttpPost("GetStudentsByGroupName")]
        public async Task<IActionResult> GetStudentsByGroupNameAsync(StudentsGroupFilterName filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupNameAsync(filter, cancellationToken);

            return Ok(students);
        }
       
        [HttpPost("AddStudent", Name = "AddStudent")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPut("EditStudent")]
        public IActionResult UpdateStudent(string firstname, [FromBody] Student updatedStudent)
        {
            var existingStudent = _context.Students.FirstOrDefault(g => g.FirstName == firstname);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.GroupId = updatedStudent.GroupId;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteGroup(string LastName, Primer_21.Models.Student updatedGroup)
        {
            var existingStudent = _context.Students.FirstOrDefault(g => g.LastName == LastName);

            if (existingStudent == null)
            {
                return NotFound();
            }
            _context.Students.Remove(existingStudent);
            _context.SaveChanges();

            return Ok();
        }

    }
}
