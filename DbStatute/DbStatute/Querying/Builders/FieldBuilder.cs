using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Querying.Qualifiers
{
    public class FieldBuilder : IFieldBuilder
    {
        public IFieldQualifier FieldQualifier { get; }

        public bool Build(out IEnumerable<Field> fields)
        {
            fields = null;

            IFieldQualifier fieldQualifier = FieldQualifier;

            if (fieldQualifier.HasField)
            {
                HashSet<Field> builtFields = new HashSet<Field>();

                foreach (Field field in fieldQualifier.Fields)
                {
                    builtFields.Add(field);
                }

                if (builtFields.Count > 0)
                {
                    fields = builtFields;
                }
            }

            return !(fields is null);
        }
    }

    public class FieldBuilder<TModel> : IFieldBuilder<TModel>
        where TModel : class, IModel, new()
    {
        public FieldBuilder()
        {
            FieldQualifier = new FieldQualifier<TModel>();
        }

        public FieldBuilder(IFieldQualifier<TModel> fieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IFieldBuilder.FieldQualifier => FieldQualifier;

        public bool Build(out IEnumerable<Field> fields)
        {
            fields = null;

            IFieldQualifier<TModel> fieldQualifier = FieldQualifier;

            if (fieldQualifier.HasField)
            {
                HashSet<Field> modelFields = Field.Parse<TModel>().ToHashSet();
                if (!modelFields.IsSupersetOf(fieldQualifier.Fields))
                {
                    throw new InvalidOperationException("Model fields must be superset of qualifier fields");
                }

                HashSet<Field> builtFields = new HashSet<Field>();

                foreach (Field field in fieldQualifier.Fields)
                {
                    builtFields.Add(field);
                }

                if (builtFields.Count > 0)
                {
                    fields = builtFields;
                }
            }

            return !(fields is null);
        }
    }
}