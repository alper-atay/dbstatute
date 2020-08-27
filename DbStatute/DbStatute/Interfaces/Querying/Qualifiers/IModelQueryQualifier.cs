using Basiclog;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IModelQueryQualifier : IQueryQualifier
    {
        IReadOnlyLogbook GetModel(out object model);
    }

    public interface IModelQueryQualifier<TModel> : IQueryQualifier<TModel>, IModelQueryQualifier
        where TModel : class, IModel, new()
    {
        IReadOnlyLogbook GetModel(out TModel model);
    }
}