using DbStatute.Sample.Models;
using System;

namespace DbStatute.Sample
{
    internal class Program
    {
        private static async void Main(string[] args)
        {
            UserUpdateQuery userUpdateQuery = new UserUpdateQuery();
            userUpdateQuery.IsFieldEnabled(x => x.FullName);
            userUpdateQuery.SetField(x => x.Nick, "13123");
            userUpdateQuery.UnsetField(x => x.Nick);
            userUpdateQuery.SetField(x => x.FullName, "New full name");

            SingleUserUpdate singleUserUpdate = new SingleUserUpdate(userUpdateQuery);

            User insertUser = new User()
            {
                FullName = "Alper Atay",
                Nick = "echo man"
            };

            SingleUserInsert singleUserInsert = new SingleUserInsert(insertUser);
            await singleUserInsert.InsertAsync(null);

            User insertedUser = singleUserInsert.InsertedModel;

            await singleUserUpdate.UpdateAsync(null, 1);

            Console.WriteLine("Hello World!");
        }
    }
}