using DbStatute.Interfaces.Fundamentals;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleSelect : ISelectBase
    {
        object SelectedModel { get; }

        Task<object> SelectAsync(IDbConnection dbConnection);
    }

    public interface ISingleSelect<TModel> : ISelectBase<TModel>, ISingleSelect
        where TModel : class, IModel, new()
    {
        new TModel SelectedModel { get; }

        new Task<TModel> SelectAsync(IDbConnection dbConnection);
    }
}