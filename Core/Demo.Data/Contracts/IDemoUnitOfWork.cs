using System.Threading.Tasks;

namespace Demo.Data.Contracts
{
    /// <summary>
    /// The Demo Unit Of Work interface.
    /// </summary>
    public interface IDemoUnitOfWork
    {
        /// <summary>
        /// Gets the employee repository.
        /// </summary>
        IEmployeeRepository EmployeeRepository { get; }

        IDesignationRepository DesignationRepository { get; }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> SaveChangesAsync();
    }
}