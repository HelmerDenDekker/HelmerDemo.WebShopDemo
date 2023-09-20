namespace WSD.Common.Exceptions
{
    public class InvalidOperationException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidOperationException" >InvalidOperationException</see>
        /// </summary>
        /// <param name="message"></param>
        protected InvalidOperationException(string message) : base(message) { }
    }
}
