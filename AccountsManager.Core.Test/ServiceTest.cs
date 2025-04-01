using AccountsManager.Models;

namespace AccountsManager.Core.Test
{
    public class ServiceTest
    {
        private const string PATH = "db_config.json";
        private readonly string _connectionString;

        public ServiceTest()
        {
            _connectionString = DbConfig.GetConnectionString(PATH);
        }

        [Fact]
        public void AddAccountTest()
        {
            var service = new Service(_connectionString);
            var result = service.AddAccount(new Account()
            {
                LastName = "Starinin",
                FirstName = "Andrey",
                Login = "admin",
                Password = "123"
            });
            
            Assert.True(result);
        }

        [Fact]
        public void GetAllAccountsTest()
        {
            var expected = new List<Account>()
            {
                new Account()
                {
                    Id = 1,
                    LastName = "Starinin",
                    FirstName = "Andrey",
                    Login = "admin",
                    Password = "123"
                }
            };
            
            var service = new Service(_connectionString);
            var actual = service.GetAllAccounts();
            
            Assert.Equal(expected, actual);
        }
    }
}
