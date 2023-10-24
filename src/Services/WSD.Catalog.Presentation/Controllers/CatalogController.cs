using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WSD.Common.Tools.Helpers;

namespace WSD.Catalog.Presentation.Controllers
{
    [ApiVersion("1.0")]
    public class CatalogController : BaseApiController
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        //[MapToApiVersion("1.0")]
        public ActionResult Get()
        {
            return new OkResult();
        }
    }
}
