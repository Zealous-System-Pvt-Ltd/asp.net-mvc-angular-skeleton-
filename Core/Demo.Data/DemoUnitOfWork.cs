using Demo.Data.Contracts;
using Demo.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Demo.Data
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    public class DemoUnitOfWork : IDemoUnitOfWork, IDisposable
    {
        #region Declarations

        /// <summary>
        /// The data context.
        /// </summary>
        protected readonly DemoDataContext DataContext;

        /// <summary>
        /// The disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoUnitOfWork"/> class.
        /// </summary>
        public DemoUnitOfWork()
        {
            DataContext = new DemoDataContext();
            EmployeeRepository = new EmployeeRepository(DataContext);
            DesignationRepository = new DesignationRepository(DataContext);
        }

        #endregion Declarations

        /// <summary>
        /// Gets the employee repository.
        /// </summary>
        public IEmployeeRepository EmployeeRepository { get; }

        public IDesignationRepository DesignationRepository { get; }

        /// <summary>
        /// The save changes async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task<int> SaveChangesAsync()
        {
            return DataContext.SaveChangesAsync();
        }

        #region IDisposable Implementation

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Implementation

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DataContext.Dispose();
                }
            }
            _disposed = true;
        }
    }
}