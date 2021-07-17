using System;
using System.Reflection;

namespace Xtz.StronglyTyped.BuiltinTypes.AutoFixture
{
    public static class TypeExtensions
    {
        public static bool TryGetSingleGenericTypeArgument(
            this Type currentType,
            Type expectedGenericDefinition,
            out Type? enumerableType)
        {
            if (!expectedGenericDefinition.GetTypeInfo().IsGenericTypeDefinition) throw new ArgumentException("Must be a generic type definition", nameof(expectedGenericDefinition));

            var typeInfo = currentType.GetTypeInfo();
            if (typeInfo.IsGenericType && currentType.GetGenericTypeDefinition() == expectedGenericDefinition)
            {
                var typeArguments = typeInfo.GenericTypeArguments;
                if (typeArguments.Length == 1)
                {
                    enumerableType = typeArguments[0];
                    return true;
                }
            }

            enumerableType = null;
            return false;
        }
    }
}