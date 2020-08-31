using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateByRawModel<TModel> : SingleUpdateBase<TModel>, ISingleUpdateByRawModel<TModel>
        where TModel : class, IModel, new()
    {
        public TModel RawModel { get; }
        object IRawModel.RawModel => RawModel;



        protected override Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {


            throw new NotImplementedException();
        }
    }
}
