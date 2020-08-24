using RepoDb.Resolvers;
using System;
using System.Data;

namespace DbStatute.Internals
{
    internal static class TypeExtension
    {
        public static bool IsDbType(this Type type)
        {
            ClientTypeToDbTypeResolver dbTypeResolver = new ClientTypeToDbTypeResolver();
            DbType? resolvedDbType = dbTypeResolver.Resolve(type);

            return resolvedDbType.HasValue;
        }
    }
}