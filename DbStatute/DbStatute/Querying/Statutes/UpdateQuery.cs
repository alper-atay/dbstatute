using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public abstract class UpdateQuery<TId, TModel> : StatuteQuery, IUpdateQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        protected UpdateQuery(IFieldQualifier fieldQualifier)
        {
            FieldQualifier = fieldQualifier;
        }

        public IFieldQualifier FieldQualifier { get; }
    }
}