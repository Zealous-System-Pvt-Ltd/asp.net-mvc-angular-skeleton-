using System;
using System.Threading.Tasks;
using Demo.DomainModel;

namespace Demo.Data.Contracts
{
    /// <summary>
    /// The EmployeeRepository interface.
    /// </summary>
    public interface IEmployeeRepository : IDataRepository<Employee, Guid>
    {
        /// <summary>
        /// The is email unique.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        Task<bool> IsEmailUnique(string email);  
    }
}