using DbStatute.Interfaces;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Extensions
{
    public static class ModelExtension
    {
        public static IEnumerable<object> GetIds(this IEnumerable<IModel> models)
        {
            return models.Select(x => x.Id);
        }

        public static QueryField GetIdsInQuery(this IEnumerable<IModel> models)
        {
            var modelIds = GetIds(models);

            return new QueryField(nameof(IModel.Id), Operation.In, modelIds);
        }
    }
}