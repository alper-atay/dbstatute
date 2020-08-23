using System;

namespace DbStatute.Sample.Models
{

    public class SingleUserUpdate : SingleUpdate<int, User, UserUpdateQuery>
    {
        public SingleUserUpdate(UserUpdateQuery updateQuery) : base(updateQuery)
        {
        }

        protected override void OnFailed()
        {
        }

        protected override void OnSucceed()
        {
        }
    }
}
