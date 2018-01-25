using System.Linq;
using System.Threading.Tasks;
using Demo.DomainModel;
using Demo.Shared.Helpers;

namespace Demo.Business.Contracts
{
    public interface IDesignationManager
    {
        /// <summary>
        /// get designations list
        /// </summary>
        /// <returns></returns>
       Task<DemoResponse<IQueryable<Designation>>> GetDesignations();
    }
}
