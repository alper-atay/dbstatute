using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Queries
{
    public class FieldQuery : IFieldQuery
    {
        public FieldQuery()
        {
            Fields = new FieldQualifier();
        }

        public FieldQuery(IFieldQualifier fields)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        public IFieldQualifier Fields { get; }
    }

    public class FieldQuery<TModel> : IFieldQuery<TModel>
        where TModel : class, IModel, new()
    {
        public FieldQuery()
        {
            Fields = new FieldQualifier<TModel>();
        }

        public FieldQuery(IFieldQualifier<TModel> fields)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        public IFieldQualifier<TModel> Fields { get; }

        IFieldQualifier IFieldQuery.Fields => Fields;
    }
}