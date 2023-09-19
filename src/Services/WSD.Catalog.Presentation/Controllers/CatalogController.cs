using Microsoft.AspNetCore.Mvc;
using WSD.Common.Tools.Helpers;

namespace WSD.Catalog.Presentation.Controllers
{
    [ApiVersion("1.0")]
    public class CatalogController : BaseApiController
    {

        [HttpGet]
        //[MapToApiVersion("1.0")]
        public string Get() => ".Net Core Web API Version 1";
    }

}
