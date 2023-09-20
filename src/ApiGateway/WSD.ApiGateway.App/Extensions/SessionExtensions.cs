using System.Text.Json;

namespace WSD.ApiGateway.App.Extensions
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Sets a string value in the session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }

        /// <summary>
        /// Gets string value from ISession.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetObject<T>(this ISession session, string key)
        {
            if (!session.Keys.Contains(key))
            {
                return default;
            }

            var value = session.GetString(key);

            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}