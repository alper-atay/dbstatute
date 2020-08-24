namespace DbStatute.Sample.Models
{
    public class SingleUserInsert : SingleInsert<int, User>
    {
        public SingleUserInsert(User rawModel) : base(rawModel)
        {
        }

        protected override void OnFailed()
        {
            Logs.Failure("User insert failed");
        }

        protected override void OnSucceed()
        {
            Logs.Success("User inserted");
        }
    }
}