namespace WSD.Common.Tools.Exceptions
{
    public class ConfigurationValueMissingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationValueMissingException" >ConfigurationValueMissingException</see>
        /// </summary>
        /// <param name="message">A message</param>
        public ConfigurationValueMissingException(string message) : base(message)
        {
        }
    }
}
