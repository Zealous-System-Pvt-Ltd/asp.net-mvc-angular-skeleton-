using Demo.Business.Contracts;
using Demo.Business.Validators;
using Demo.Data.Contracts;
using Demo.DomainModel;
using Demo.Shared.Helpers;
using Demo.Shared.Resources;
using FluentValidation.Results;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Demo.Business.Manager
{
    /// <summary>
    /// EmployeeManager contains business logic regards employee modules
    /// </summary>
    public class EmployeeManager : IEmployeeManager
    {
        /// <summary>
        /// The demo unit of work.
        /// </summary>
        private readonly IDemoUnitOfWork _demoUnitOfWork;

        private readonly ICacheManager _cacheManager;

        //private ObjectCache objectCache;

        //private CacheItemPolicy cacheItemPolicy;

        private IList<ValidationFailure> _errorsList;

        /// <summary>
        /// The cache key.
        /// </summary>
        private const string CacheKey = "EmpList";

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeManager"/> class.
        /// </summary>
        /// <param name="demoUnitOfWork">
        /// The ag live unit of work.
        /// </param>
        /// <param name="cacheManager">The Cache Manager</param>
        public EmployeeManager(IDemoUnitOfWork demoUnitOfWork, ICacheManager cacheManager)
        {
            _demoUnitOfWork = demoUnitOfWork;
            _cacheManager = cacheManager;
            //  this.objectCache = MemoryCache.Default;
            // this.cacheItemPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(15) };
        }

        /// <summary>
        /// The get employees.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<DemoResponse<IQueryable<Employee>>> GetEmployees()
        {
            var exceptions = new Dictionary<string, string>();

            IQueryable<Employee> employeesList = null;

            try
            {
                if (_cacheManager.DoesCacheContains(CacheKey))
                {
                    employeesList = _cacheManager.GetFromCache<IQueryable<Employee>>(CacheKey);
                    if (employeesList != null)
                    {
                        return new DemoResponse<IQueryable<Employee>> { Response = employeesList, Exceptions = exceptions };
                    }
                }
                employeesList = await _demoUnitOfWork.EmployeeRepository.RetrieveAllRecordsAsync();

                // here you have employeesList as still IQueryable which is in form of expression tree and you can still take the
                // advantage of deffered Linq Execution. If you want to apply further filters over here based on your business rules
                // you can do that, before auto mapper converts it back to List from Queryable
                if (employeesList != null)
                {
                    // got the data from db add it in cache
                    _cacheManager.AddToCache(CacheKey, employeesList, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(10) });
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

            var response = new DemoResponse<IQueryable<Employee>> { Response = employeesList, Exceptions = exceptions };
            return response;
        }

        public async Task<DemoResponse<Employee>> GetEmployeeById(Guid id)
        {
            var exceptions = new Dictionary<string, string>();

            Employee employee = null;

            try
            {
                employee = await _demoUnitOfWork.EmployeeRepository.GetByIdAsync(id);
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

            var response = new DemoResponse<Employee> { Response = employee, Exceptions = exceptions };
            return response;
        }

        public async Task<DemoResponse<bool>> UpdateEmployee(Employee employee)
        {
            var exceptions = new Dictionary<string, string>();
            var saveResponse = false;

            try
            {
                await _demoUnitOfWork.EmployeeRepository.Update(employee);
                saveResponse = await _demoUnitOfWork.SaveChangesAsync() > 0;

                if (saveResponse)
                {
                    // Saved successfully Invalidate Cache
                    _cacheManager.RemoveFromCache(CacheKey);
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
            catch (DBConcurrencyException dbConcurrencyException)
            {
                Log.LogException(dbConcurrencyException);
                exceptions.Add("Error 3", ErrorMessages.DbError);
            }
            catch (Exception exception)
            {
                Log.LogException(exception);
                exceptions.Add("Error 4", ErrorMessages.GenericError);
            }

            var response = new DemoResponse<bool> { Response = saveResponse, Exceptions = exceptions };
            return response;
        }

        /// <summary>
        /// The add employee.
        /// </summary>
        /// <param name="employee">
        /// The employee.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<DemoResponse<bool>> AddEmployee(Employee employee)
        {
            var exceptions = new Dictionary<string, string>();
            var saveResponse = false;

            try
            {
                if (IsEmployeeValid(employee, "add"))
                {
                    employee.EmployeeId = Guid.NewGuid();
                    await _demoUnitOfWork.EmployeeRepository.Insert(employee);
                    saveResponse = await _demoUnitOfWork.SaveChangesAsync() > 0;
                    if (saveResponse)
                    {
                        // Saved successfully Invalidate Cache
                        _cacheManager.RemoveFromCache(CacheKey);
                    }
                }
                else
                {
                    foreach (var validationFailure in _errorsList)
                    {
                        exceptions.Add(validationFailure.PropertyName, validationFailure.ErrorMessage);
                    }
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
            catch (DBConcurrencyException dbConcurrencyException)
            {
                Log.LogException(dbConcurrencyException);
                exceptions.Add("Error 3", ErrorMessages.DbError);
            }
            catch (Exception exception)
            {
                Log.LogException(exception);
                exceptions.Add("Error 4", ErrorMessages.GenericError);
            }

            var response = new DemoResponse<bool> { Response = saveResponse, Exceptions = exceptions };
            return response;
        }

        public bool IsEmailUnique(string email)
        {
            return _demoUnitOfWork.EmployeeRepository.IsEmailUnique(email).Result;
        }

        private bool IsEmployeeValid(Employee employee, string mode)
        {
            var validator = new EmployeeValidator(ServiceLocator.Current.GetInstance<IEmployeeManager>(), mode);
            var result = validator.Validate(employee);
            if (result.IsValid)
            {
                return true;
            }
            _errorsList = result.Errors;
            return false;
        }
    }
}