using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
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
        private IEnumerable<dynamic> _dynamicModels;

        protected MultipleSelectByQueryAndFieldQualifier(TSelectQuery selectQuery, TFieldQualifier fieldQualifier) : base(selectQuery)
        {
            FieldQualifier = fieldQualifier;
        }

        public TFieldQualifier FieldQualifier { get; }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (ReadOnlyLogs.Safely)
            {
                IEnumerable<OrderField> orderFields = null;
                if (SelectQuery is ISortableQuery sortableQuery)
                {
                    if (sortableQuery.HasOrderField)
                    {
                        orderFields = sortableQuery.OrderFields;
                    }
                }

                string tableName = ClassMappedNameCache.Get<TModel>();

                _dynamicModels = await dbConnection.QueryAsync(tableName, SelectQuery.QueryGroup, FieldQualifier.Fields, orderFields);

                return _dynamicModels.Cast<TModel>();
            }

            return null;
        }
    }
}