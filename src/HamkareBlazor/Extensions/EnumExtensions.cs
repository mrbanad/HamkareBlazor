using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HamkareBlazor.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Universal method that retrieves an array of the values of the constant in specified enumeration, works with nullable and non-nullable enums.
        /// Original <see cref="Enum.GetValues"/> works only with non-nullable enums and will throw exception.
        /// </summary>
        /// <returns>An array that contains the values of constant in type</returns>
        internal static IEnumerable<Enum> GetSafeEnumValues(Type? type)
        {
            if (type is null)
            {
                return [];
            }

            if (type.IsEnum)
            {
                return Enum.GetValues(type).Cast<Enum>();
            }

            if (type.IsGenericType && typeof(Nullable<>) == type.GetGenericTypeDefinition())
            {
                var actualType = type.GetGenericArguments()[0];
                return Enum.GetValues(actualType).Cast<Enum>();
            }

            return [];
        }

        /// <summary>
        /// Converts an <see cref="Adornment"/> to its corresponding <see cref="Edge"/> value.
        /// </summary>
        /// <param name="adornment">The adornment value to convert.</param>
        /// <returns>The corresponding <see cref="Edge"/> value.</returns>
        internal static Edge ToEdge(this Adornment adornment)
        {
            return adornment switch
            {
                Adornment.Start => Edge.Start,
                Adornment.End => Edge.End,
                _ => Edge.False
            };
        }
        
        /// <summary>
        /// Converts a string to the corresponding enum value. Throws if conversion fails.
        /// </summary>
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = true) where TEnum : struct, Enum
        {
            return Enum.TryParse<TEnum>(value.Replace("-", ""), ignoreCase, out var result)
                ? result
                : throw new ArgumentException($"Unable to convert '{value}' to enum type {typeof(TEnum).Name}");
        }

        /// <summary>
        /// Gets the display name of an enum value from its <see cref="DisplayAttribute"/> or returns its name.
        /// </summary>
        public static string GetEnumDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null) return value.ToString();

            var attribute = field.GetCustomAttribute<DisplayAttribute>();
            return attribute?.GetName() ?? field.ToString() ?? value.ToString();
        }
    }
}
