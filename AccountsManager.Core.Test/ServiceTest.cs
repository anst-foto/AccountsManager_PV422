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
    }
}
