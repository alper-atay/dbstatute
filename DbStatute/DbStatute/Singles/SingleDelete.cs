﻿using DbStatute.Interfaces;
using DbStatute.Interfaces.Singles;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleDelete<TModel> : SingleDeleteById<TModel>, ISingleDelete<TModel>
        where TModel : class, IModel, new()
    {
        public SingleDelete(TModel readyModel) : base(readyModel.Id)
        {
            ReadyModel = readyModel ?? throw new ArgumentNullException(nameof(readyModel));
        }

        public TModel ReadyModel { get; }
        object IReadyModel.ReadyModel => ReadyModel;

        protected override async Task<TModel> DeleteOperationAsync(IDbConnection dbConnection)
        {
            return await base.DeleteOperationAsync(dbConnection) ?? ReadyModel;
        }
    }
}