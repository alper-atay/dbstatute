using Basiclog;
using DbStatute.Enumerations;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IQueryGroupBuilder
    {
        QueryGroupUsage Usage { get; }

        #region Qualifiers

        IFieldBuilder FieldBuilder { get; }
        IPredicateFieldQualifier PredicateFieldQualifier { get; }
        IValueFieldQualifier ValueFieldQualifier { get; }

        #endregion Qualifiers

        IReadOnlyLogbook Build(out QueryGroup queryGroup);
    }

    public interface IQueryGroupBuilder<TModel> : IQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        new IFieldBuilder<TModel> FieldBuilder { get; }
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}