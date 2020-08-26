using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleDeleteById<TModel, TMultipleSelectById>
        where TModel : class, IModel, new()
        where TMultipleSelectById : IMultipleSelectById<TModel>
    {
        TMultipleSelectById MultipleSelectById { get; }

        Task<int> DeleteAsync(IDbConnection dbConnection);
    }
}