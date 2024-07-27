
using HW31DataBaseTest.Methods;
using HW31DataBaseTest.Models;

namespace HW31DataBaseTest.Tests
{
    public class BaseTest
    {
        private protected const string connectionString = "Server=localhost;Database=univer;User Id=root;Password=1234;";
        public DBMethods dbHelper;
        private static Random random = new Random();

        [SetUp]
        public void Setup()
        {  
            dbHelper = new(connectionString);
        }

        [TearDown]
        public void TearDown()
        {
            // dbHelper = null;
        }

        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static GroupModel GetGroup() => new()
        {
            Name = RandomString(),
            idGroupList = new Random().Next(0, 10)
        };

        public static StudentModel GetStudent(GroupModel group) => new()
        {
            Name = RandomString(),
            IdGroupList = group.idGroupList
        };
    }
}
