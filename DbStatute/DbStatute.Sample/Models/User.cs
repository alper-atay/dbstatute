namespace DbStatute.Sample.Models
{
    public class User : IUser
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Nick { get; set; }

        public object Clone()
        {
            return new User()
            {
                Id = Id,
                FullName = FullName,
                Nick = Nick
            };
        }
    }
}