using BankAccountInterest.Service;
using Xunit;

namespace BankAccountInterest.Tests.Services
{
    public class AccountServiceTests
    {
        [Fact]
        public void GetAccount_WithValidData_ShoudReturnAccount()
        {
            // Arrange
            var accountService = new AccountService();
            var accountNumber = "AC001";
            var transactionType = "D";
            decimal amount = 100;

            // Act
            var result = accountService.GetOrCreateAccount(accountNumber, transactionType, amount);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Balance >= 0);
        }

        [Fact]
        public void GetAccount_WithInvalidData_ShoudReturnNull()
        {
            // Arrange
            var accountService = new AccountService();
            var transactionType = "W";
            decimal amount = 100;

            // Act
            var result = accountService.GetOrCreateAccount(string.Empty, transactionType, amount);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateAccount_WithValidData_ShoudReturnAccount()
        {
            // Arrange
            var accountService = new AccountService();
            var accountNumber = "AC001";
            var transactionType = "D";
            decimal amount = 100;

            // Act
            var result = accountService.UpdateAccount(accountNumber, amount, transactionType);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void UpdateAccount_WithInvalidData_ShoudReturnNull()
        {
            // Arrange
            var accountService = new AccountService();
            var transactionType = "W";
            decimal amount = 100;

            // Act
            var result = accountService.UpdateAccount(string.Empty, amount, transactionType);

            // Assert
            Assert.False(result);
        }
    }
}
