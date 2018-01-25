using System;
using Demo.DomainModel;

namespace Demo.Data.Contracts
{
    /// <summary>
    /// The Designation Repository interface.
    /// </summary>
    public interface IDesignationRepository : IDataRepository<Designation, Guid>
    {
    }
}
