using RepoDb.Resolvers;
using System;
using System.Data;

namespace DbStatute.Internals
{
    internal static class TypeExtension
    {
        public static bool IsDbType(this Type type)
        {
            ClientTypeToDbTypeResolver clientTypeToDbTypeResolver = new ClientTypeToDbTypeResolver();
            DbType? propertyMemberDbType = clientTypeToDbTypeResolver.Resolve(type);

            return propertyMemberDbType.HasValue;
        }
    }
}