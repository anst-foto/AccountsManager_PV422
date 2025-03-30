using AccountsManager.Models;

namespace AccountsManager.Core.Test
{
    public class ServiceTest
    {
        [Fact]
        public void AddAccountTest()
        {
            var service = new Service();
            var result = service.AddAccount(new Account()
            {
                LastName = "Starinin",
                FirstName = "Andrey",
                Login = "admin",
                Password = "123"
            });
            
            //Assert.Equal(result, true);
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
            
            var service = new Service();
            var actual = service.GetAllAccounts();
            
            Assert.Equal(expected, actual);
        }
    }
}
