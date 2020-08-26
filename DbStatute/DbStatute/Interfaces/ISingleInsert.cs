﻿using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleInsert : IInsert
    {
        object InsertedModel { get; }
        object RawModel { get; }

        Task<object> InsertAsync(IDbConnection dbConnection);
    }

    public interface ISingleInsert<TModel> : IInsert<TModel>, ISingleInsert
        where TModel : class, IModel, new()
    {
        new TModel InsertedModel { get; }
        new TModel RawModel { get; }

        new Task<TModel> InsertAsync(IDbConnection dbConnection);
    }
}