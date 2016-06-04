using System;
using Tests.DB;
using Tests.Implementations;

namespace Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var dataContext = new DataContext())
            {
                var userRepository = new UserRepository(dataContext);
                var userEntity = userRepository.Find("first");
                Console.WriteLine(userEntity);
            }
        }
    }
}
