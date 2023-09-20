using Microsoft.AspNetCore.Mvc;
using WSD.Common.Tools.Attributes;

namespace WSD.Common.Tools.Helpers
{
    [SecurityHeaders]
    [ApiController]
    [Route("v{v:apiVersion}/[controller]")]
    public class BaseApiController
    {
        /// <summary>
        /// A default ActionResponse for returning a generic response as returned by the result
        /// </summary>
        /// <param name="result"><see cref="Result"/> Result  with a StatusCode</param>
        /// <returns>ActionResult according to statuscode in the <see cref="Result"/> Result </returns>
        [NonAction]
        public ActionResult ActionResponse(Result result)
        {
            return new ContentResult
            {
                StatusCode = (int)result.StatusCode,
                Content = result.Messages.First(),
            };
        }
    }
}
