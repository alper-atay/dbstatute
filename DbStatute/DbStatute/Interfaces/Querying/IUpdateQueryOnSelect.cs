using DbStatute.Interfaces.Querying.Fundamentals;

namespace DbStatute.Interfaces.Querying
{
    public interface IUpdateQueryOnSelect : IStatuteQueryBase
    {

    }

    public interface IUpdateQueryOnSelect<TModel> : IStatuteQueryBase<TModel>, IUpdateQueryOnSelect
        where TModel : class, IModel, new()
    {

    }
}