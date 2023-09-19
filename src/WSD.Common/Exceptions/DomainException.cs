namespace WSD.Common.Exceptions
{
    public class DomainException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainException" >DomainException</see>
        /// </summary>
        /// <param name="message"></param>
        protected DomainException(string message) : base(message) { }
    }
}
