namespace DbStatute.Sample.Models
{

    public class SingleUserUpdate : SingleUpdateByQuery<int, User, UserUpdateQuery>
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
