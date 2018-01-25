using Demo.Data.Contracts;
using Demo.DomainModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Data.Repositories
{
    /// <summary>
    /// The designation repository.
    /// </summary>
    public class DesignationRepository : IDesignationRepository
    {
        private readonly DemoDataContext _demoDataContext;

        public DesignationRepository(DemoDataContext demoDataContext)
        {
            _demoDataContext = demoDataContext;
        }

        public Task<Designation> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Designation>> RetrieveAllRecordsAsync()
        {
            return Task.Run(
                 () =>
                    {
                        var designations = _demoDataContext.Designations.AsQueryable();
                        return designations;
                    });
        }

        public Task Insert(Designation entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Designation entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}