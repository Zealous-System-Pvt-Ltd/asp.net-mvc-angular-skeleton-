using Demo.Business.Contracts;
using Demo.Data.Contracts;
using Demo.DomainModel;
using Demo.Shared.Helpers;
using Demo.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Demo.Business.Manager
{
    /// <summary>
    /// DesinationManager contains business logic regards designation entity
    /// </summary>
    public class DesignationManager : IDesignationManager
    {
        /// <summary>
        /// The cache key.
        /// </summary>
        private const string CacheKey = "DesignationsList";

        /// <summary>
        /// The demo unit of work.
        /// </summary>
        private readonly IDemoUnitOfWork _demoUnitOfWork;

        private readonly ICacheManager _cacheManager;

        public DesignationManager(IDemoUnitOfWork demoUnitOfWork, ICacheManager cacheManager)
        {
            _demoUnitOfWork = demoUnitOfWork;
            _cacheManager = cacheManager;
        }

        public async Task<DemoResponse<IQueryable<Designation>>> GetDesignations()
        {
            var exceptions = new Dictionary<string, string>();

            IQueryable<Designation> designationsList = null;

            try
            {
                if (_cacheManager.DoesCacheContains(CacheKey))
                {
                    designationsList = _cacheManager.GetFromCache<IQueryable<Designation>>(CacheKey);
                    if (designationsList != null)
                    {
                        return new DemoResponse<IQueryable<Designation>> { Response = designationsList, Exceptions = exceptions };
                    }
                }
                designationsList = await _demoUnitOfWork.DesignationRepository.RetrieveAllRecordsAsync();

                // here you have employeesList as still IQueryable which is in form of expression tree and you can still take the
                // advantage of deffered Linq Execution. If you want to apply further filters over here based on your business rules
                // you can do that, before auto mapper converts it back to List from Queryable
                if (designationsList != null)
                {
                    // got the data from db add it in cache
                    _cacheManager.AddToCache(CacheKey, designationsList, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) });
                }
            }
            catch (TaskCanceledException taskCanceledException)
            {
                Log.LogException(taskCanceledException);
                exceptions.Add("Error 1", ErrorMessages.AsyncError);
            }
            catch (SqlException sqlException)
            {
                Log.LogException(sqlException);
                exceptions.Add("Error 2", ErrorMessages.DbError);
            }
            catch (Exception exception)
            {
                Log.LogException(exception);
                exceptions.Add("Error 3", ErrorMessages.GenericError);
            }

            var response = new DemoResponse<IQueryable<Designation>> { Response = designationsList, Exceptions = exceptions };
            return response;
        }
    }
}