using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public class InsertQuery<TId, TModel> : IInsertQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public InsertQuery()
        {
            FieldQualifier = new FieldQualifier<TId, TModel>();
        }

        public InsertQuery(IFieldQualifier<TId, TModel> fieldQualifier)
        {
            FieldQualifier = fieldQualifier;
        }

        public IFieldQualifier<TId, TModel> FieldQualifier { get; }
        IFieldQualifier IInsertQuery.FieldQualifier => FieldQualifier;
    }
}