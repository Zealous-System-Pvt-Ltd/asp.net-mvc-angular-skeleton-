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
    public class DesignationsController : ODataController
    {
        private static readonly ODataValidationSettings _validationSettings = new ODataValidationSettings();

        /// <summary>
        /// The designation manager.
        /// </summary>
        private readonly IDesignationManager _designationManager;

        public DesignationsController(IDesignationManager designationManager)
        {
            _designationManager = designationManager;
        }

        // GET: odata/Designations
        [EnableQuery]
        public async Task<IHttpActionResult> GetDesignations(ODataQueryOptions<Designation> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
                var response = await _designationManager.GetDesignations();
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

        // GET: odata/Designations(5)
        public IHttpActionResult GetDesignation([FromODataUri] Guid key, ODataQueryOptions<Designation> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Designations(5)
        public IHttpActionResult Put([FromODataUri] Guid key, Delta<Designation> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // TODO: Save the patched entity.

            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Designations
        public IHttpActionResult Post(Designation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(designation);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Designations(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Guid key, Delta<Designation> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(designation);

            // TODO: Save the patched entity.

            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Designations(5)
        public IHttpActionResult Delete([FromODataUri] Guid key)
        {
            // TODO: Add delete logic here.

            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}