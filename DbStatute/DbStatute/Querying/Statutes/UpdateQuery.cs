using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute.Querying.Statutes
{
    public class UpdateQuery<TModel> : StatuteQuery, IUpdateQuery<TModel>
        where TModel : class, IModel, new()
    {
        protected UpdateQuery(IFieldQualifier<TModel> fieldQualifier)
        {
            FieldQualifier = fieldQualifier;
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IUpdateQuery.FieldQualifier => FieldQualifier;
    }
}