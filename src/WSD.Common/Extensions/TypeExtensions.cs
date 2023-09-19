using System.Reflection;

namespace WSD.Common.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Method to get a list of constants
        /// </summary>
        /// <param name="type"></param>
        public static IEnumerable<FieldInfo> GetPublicConstants(this Type type)
        {
            return type.GetPublicClassConstants()
                .Concat(type.GetNestedTypes(BindingFlags.Public).SelectMany(GetPublicConstants));
        }

        private static IEnumerable<FieldInfo> GetPublicClassConstants(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fielddInfo => fielddInfo.IsLiteral && !fielddInfo.IsInitOnly);
        }
    }
}
