using WSD.Common.Tools.Exceptions;

namespace WSD.ApiGateway.App.Exceptions
{
    public class DiscoveryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryException" >DiscoveryException</see>
        /// </summary>
        /// <param name="message">A message</param>
        public DiscoveryException(string message) : base(message)
        {
        }
    }
}
