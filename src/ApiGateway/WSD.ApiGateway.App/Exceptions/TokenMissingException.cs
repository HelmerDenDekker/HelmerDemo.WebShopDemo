using WSD.Common.Tools.Exceptions;

namespace WSD.ApiGateway.App.Exceptions
{
    public class TokenMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenMissingException" >TokenMissingException</see>
        /// </summary>
        /// <param name="message">A message</param>
        public TokenMissingException(string message) : base(message)
        {
        }
    }
}
