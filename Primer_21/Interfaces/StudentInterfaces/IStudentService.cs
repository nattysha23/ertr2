using Primer_21.Database;
using Primer_21.Models;
using Primer_21.Filters.StudentFilters;
using Microsoft.EntityFrameworkCore;

namespace Primer_21.Interfaces.StudentInterfaces
{
    public interface IStudentService
    {
        public Task<Student[]> GetStudentsByGroupIDAsync(StudentsGroupFilterID filter, CancellationToken cancellationToken);
        public Task<Student[]> GetStudentsByGroupNameAsync(StudentsGroupFilterName filter, CancellationToken cancellationToken);

    }
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;
        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public Task<Student[]> GetStudentsByGroupIDAsync(StudentsGroupFilterID filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupId == filter.GroupId).ToArrayAsync(cancellationToken);

            return students;
        }
        public Task<Student[]> GetStudentsByGroupNameAsync(StudentsGroupFilterName filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Student>().Where(w => w.Group.GroupName == filter.GroupName).ToArrayAsync(cancellationToken);

            return students;
        }
    }
}
