using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelectByQueryAndFieldQualifier<TId, TModel, TSelectQuery, TFieldQualifier> : MultipleSelectByQuery<TId, TModel, TSelectQuery>, IMultipleSelectByQueryAndFieldQualifier<TId, TModel, TSelectQuery, TFieldQualifier>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
        where TFieldQualifier : IFieldQualifier<TId, TModel>
    {
        private readonly List<dynamic> _dynamicSelectedModels = new List<dynamic>();

        protected MultipleSelectByQueryAndFieldQualifier(TSelectQuery selectQuery, TFieldQualifier fieldQualifier) : base(selectQuery)
        {
            FieldQualifier = fieldQualifier;
        }

        public int DynamicSelectedModelCount => _dynamicSelectedModels.Count;
        public IEnumerable<dynamic> DynamicSelectedModels => DynamicSelectedModelCount > 0 ? _dynamicSelectedModels : null;
        public TFieldQualifier FieldQualifier { get; }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            _dynamicSelectedModels.Clear();

            if (!ReadOnlyLogs.Safely)
            {
                return null;
            }

            IEnumerable<OrderField> orderFields = null;
            if (SelectQuery is ISortableQuery sortableQuery)
            {
                if (sortableQuery.HasOrderField)
                {
                    orderFields = sortableQuery.OrderFields;
                }
            }

            // Need generic type passed method for qualifiered fields
            string tableName = ClassMappedNameCache.Get<TModel>();

            ICache cache = null;
            int cacheItemExpiration = 180;
            string cacheKey = null;

            if (Cacheable != null)
            {
                cache = Cacheable.Cache;
                cacheItemExpiration = Cacheable.ItemExpiration ?? 180;
                cacheKey = Cacheable.Key;
            }

            IEnumerable<dynamic> dynamicSelectedModels = await dbConnection.QueryAsync(tableName, SelectQuery.QueryGroup, FieldQualifier.Fields, orderFields, MaxSelectCount, Hints, cacheKey, cacheItemExpiration, CommandTimeout, Transaction, cache, Trace, StatementBuilder);

            _dynamicSelectedModels.AddRange(dynamicSelectedModels);

            return _dynamicSelectedModels.Cast<TModel>();
        }
    }
}