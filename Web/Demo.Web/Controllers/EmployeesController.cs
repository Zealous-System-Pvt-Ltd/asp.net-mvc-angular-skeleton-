#region License and Copyrights

// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeesController.cs" company="zealous">
//  MIT License
// </copyright>
// <summary>
//   The employees controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#endregion License and Copyrights

using Demo.Business.Contracts;
using Demo.DomainModel;
using Demo.Shared.Helpers;
using Microsoft.Data.OData;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace Demo.Web.Controllers
{
    /// <summary>
    /// The employees controller.
    /// </summary>

    [Authorize]
    public class EmployeesController : ODataController
    {
        /// <summary>
        /// The validation settings.
        /// </summary>
        private static readonly ODataValidationSettings ValidationSettings = new ODataValidationSettings();

        /// <summary>
        /// The employee manager.
        /// </summary>
        private readonly IEmployeeManager _employeeManager;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeesController"/> class.
        /// </summary>
        /// <param name="employeeManager">
        /// The employee manager.
        /// </param>
        public EmployeesController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        #endregion Constructor

        #region Action Methods

        // GET: odata/Employees

        /// <summary>
        /// The get employees.
        /// </summary>
        /// <param name="queryOptions">
        /// The query options.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [EnableQuery]
        public async Task<IHttpActionResult> GetEmployees(ODataQueryOptions<Employee> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(ValidationSettings);
                var response = await _employeeManager.GetEmployees();
                if (response.Response == null)
                {
                    return InternalServerError(new Exception(response.Exceptions.DictionaryToString()));
                }

                return Ok(response.Response);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: odata/Employees(5)
        /// <summary>
        /// The get employee.
        /// </summary>
        /// <param name="key">
        ///     The key.
        /// </param>
        /// <param name="queryOptions">
        ///     The query options.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IHttpActionResult> GetEmployee(Guid key, ODataQueryOptions<Employee> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(ValidationSettings);
                var response = await _employeeManager.GetEmployeeById(key);
                if (response.Response != null)
                {
                    return Ok(response.Response);
                }
                return InternalServerError(new Exception(response.Exceptions.DictionaryToString()));
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: http://localhost:4119/odata/Employees(216a100c-81f0-4362-b462-b6e7062dcea3)
        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="employee">
        /// The employee.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPut]
        public async Task<IHttpActionResult> Put(Employee employee)
        {
            var response = await _employeeManager.UpdateEmployee(employee);

            if (response.Response)
            {
                return Updated(employee);
            }
            return InternalServerError(new Exception(response.Exceptions.DictionaryToString()));
        }

        // POST: odata/Employees
        [HttpPost]
        public async Task<IHttpActionResult> Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _employeeManager.AddEmployee(employee);

            if (response.Response)
            {
                return Created(employee);
            }

            return InternalServerError(new Exception(response.Exceptions.DictionaryToString()));
        }

        // PATCH: odata/Employees(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<Employee> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // employee.Patch(employee);

            // TODO: Save the patched entity.

            // return Updated(employee);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Employees(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        /// <summary>
        /// The is email unique.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>
        /// The <see cref="IHttpActionResult"/>.
        /// </returns>
        [HttpPost]
        public IHttpActionResult IsEmailUnique(ODataActionParameters parameters)
        {
            var email = (string)parameters["email"];
            var isExists = _employeeManager.IsEmailUnique(email);
            return Ok(isExists);
        }

        #endregion Action Methods
    }
}