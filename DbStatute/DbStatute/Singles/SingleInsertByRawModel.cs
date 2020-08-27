using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByRawModel<TModel> : SingleInsertBase<TModel>, ISingleInsertByRawModel<TModel>
        where TModel : class, IModel, new()
    {
        public TModel RawModel { get; }

        object IRawModel.RawModel => RawModel;

        protected override Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}