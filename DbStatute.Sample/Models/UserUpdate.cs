using System;

namespace DbStatute.Sample.Models
{

    public class UserUpdate : SingleUpdate<int, User, UserUpdateQuery>
    {
        public UserUpdate(UserUpdateQuery updateQuery) : base(updateQuery)
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
