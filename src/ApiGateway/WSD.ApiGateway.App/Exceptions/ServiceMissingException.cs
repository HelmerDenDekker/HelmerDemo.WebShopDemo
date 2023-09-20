using WSD.Common.Tools.Exceptions;

namespace WSD.ApiGateway.App.Exceptions
{
    public class ServiceMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceMissingException" >ServiceMissingException</see>
        /// </summary>
        /// <param name="message">A message</param>
        public ServiceMissingException(string message) : base(message)
        {
        }
    }
}
