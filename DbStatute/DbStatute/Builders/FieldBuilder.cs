using DbStatute.Extensions;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;

namespace DbStatute.Builders
{
    public class FieldBuilder<TModel> : Builder<IEnumerable<Field>>, IFieldBuilder<TModel>
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

        protected override bool BuildOperation(out IEnumerable<Field> built)
        {
            built = null;

            IFieldQualifier<TModel> fieldQualifier = FieldQualifier;

            if (fieldQualifier.HasField)
            {
                if (!fieldQualifier.IsSubsetOfModel<TModel>())
                {
                    throw new InvalidOperationException("Qualifier fields must be subset of model fields");
                }

                HashSet<Field> builtFields = new HashSet<Field>();

                foreach (Field field in fieldQualifier.Fields)
                {
                    builtFields.Add(field);
                }

                if (builtFields.Count > 0)
                {
                    built = builtFields;
                }
            }

            return !(built is null);
        }
    }
}