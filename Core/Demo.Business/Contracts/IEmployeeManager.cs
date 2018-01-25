using System;
using System.Linq;
using System.Threading.Tasks;
using Demo.DomainModel;
using Demo.Shared.Helpers;

namespace Demo.Business.Contracts
{
    /// <summary>
    /// The Employee Manager interface.
    /// </summary>
    public interface IEmployeeManager
    {
        /// <summary>
        /// The get employees.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<DemoResponse<IQueryable<Employee>>> GetEmployees();

        /// <summary>
        /// The get employee by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<DemoResponse<Employee>> GetEmployeeById(Guid id);

        /// <summary>
        /// update employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<DemoResponse<bool>> UpdateEmployee(Employee employee);

        /// <summary>
        /// add new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        Task<DemoResponse<bool>> AddEmployee(Employee employee);

        /// <summary>
        /// check email uniqueness
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailUnique(string email);
    }
}