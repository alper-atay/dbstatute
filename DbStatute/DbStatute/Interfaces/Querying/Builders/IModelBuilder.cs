using Basiclog;
using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IModelBuilder
    {
        IReadOnlyLogbook BuildModel(out object model);
    }

    public interface IModelBuilder<TModel> : IQueryQualifier<TModel>, IModelBuilder
        where TModel : class, IModel, new()
    {
        IReadOnlyLogbook BuildModel(out TModel model);
    }
}