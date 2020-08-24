using Basiclog;
using DbStatute.Querying;
using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbStatute.Sample.Models
{
    public class UserSelectQuery : BuildableSelectQuery<int, User>
    {
        public bool FullNameEnabled { get; set; }
        public bool NickEnabled { get; set; }

        public override QueryGroup Build()
        {
            ICollection<QueryField> queryFields = new Collection<QueryField>();

            if (NickEnabled)
            {
                queryFields.Add(new QueryField(nameof(User.Nick), Operation.Like, SelecterModel.Nick));
            }

            if (FullNameEnabled)
            {
                queryFields.Add(new QueryField(nameof(User.Nick), Operation.Like, SelecterModel.FullName));
            }

            return new QueryGroup(queryFields, Conjunction.And);
        }

        public override IReadOnlyLogbook Test()
        {
            ILogbook logs = Logger.New();

            if (NickEnabled)
            {
                // ... Examining
            }

            if (FullNameEnabled)
            {
                // ... Examining
            }

            return logs;
        }
    }
}