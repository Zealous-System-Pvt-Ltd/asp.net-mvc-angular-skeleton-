using Demo.Data.Contracts;
using Demo.DomainModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// The demo data context.
        /// </summary>
        private readonly DemoDataContext _demoDataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="demoDataContext">
        /// The demo data context.
        /// </param>
        public EmployeeRepository(DemoDataContext demoDataContext)
        {
            _demoDataContext = demoDataContext;
        }

        public async Task<Employee> GetByIdAsync(Guid id)
        {
            using (_demoDataContext)
            {
                var employee = await _demoDataContext.Employees.SingleOrDefaultAsync(e => e.EmployeeId.Equals(id));
                return employee;
            }
        }

        public Task<IQueryable<Employee>> RetrieveAllRecordsAsync()
        {
            return Task.Run(
                 () =>
                    {
                        var employees = _demoDataContext.Employees.AsQueryable();
                        return employees;
                    });
        }

        public Task Insert(Employee entity)
        {
            return Task.Run(() => _demoDataContext.Employees.Add(entity));
        }

        public Task Update(Employee entity)
        {
            return Task.Run(async () =>
                {
                    var orignial = await _demoDataContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId.Equals(entity.EmployeeId));
                    if (orignial != null)
                    {
                        _demoDataContext.Entry(orignial).CurrentValues.SetValues(entity);
                    }
                });
        }

        public Task Delete(Guid id)
        {
            return Task.Run(async () =>
                {
                    var employeetobeDeleted = await _demoDataContext.Employees.FirstOrDefaultAsync(e => e.EmployeeId.Equals(id));
                    if (employeetobeDeleted != null)
                    {
                        _demoDataContext.Employees.Remove(employeetobeDeleted);
                    }
                });
        }

        /// <summary>
        /// The is email unique.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<bool> IsEmailUnique(string email)
        {
            return _demoDataContext.Employees.AnyAsync(e => e.Email.Equals(email));
        }
    }
}