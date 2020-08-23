using DbStatute.Interfaces;
using DbStatute.Querying;
using System;

namespace DbStatute
{
    public abstract class MultipleUpdate<TId, TModel, TUpdateQuery> : Update
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : UpdateQuery<TId, TModel>
    {


        protected MultipleUpdate(TUpdateQuery updateQuery)
        {
        }
    }
}