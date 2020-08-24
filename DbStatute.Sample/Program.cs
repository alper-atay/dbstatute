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

            await singleUserUpdate.UpdateAsync(1, null);

            Console.WriteLine("Hello World!");
        }
    }
}