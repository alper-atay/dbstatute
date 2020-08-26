using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute.Querying.Statutes
{
    public class InsertQuery : IInsertQuery
    {
        public InsertQuery()
        {
        }

        public IFieldQualifier FieldQualifier { get; }
    }

    public class InsertQuery<TModel> : IInsertQuery<TModel>
        where TModel : class, IModel, new()
    {
        public InsertQuery()
        {
            FieldQualifier = new FieldQualifier<TModel>();
        }

        public InsertQuery(IFieldQualifier<TModel> fieldQualifier)
        {
            FieldQualifier = fieldQualifier;
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IInsertQuery.FieldQualifier => FieldQualifier;
    }
}