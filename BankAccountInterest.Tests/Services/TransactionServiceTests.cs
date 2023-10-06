using BankAccountInterest.Service;
using Xunit;

namespace BankAccountInterest.Tests.Services
{
    public class TransactionServiceTests
    {
        [Fact]
        public void PerformTransaction_WithValidData_ShouldReturnTransactionHistory()
        {
            // Arrange
            var transactionService = new TransactionService();
            var dateString = "20230605";
            var accountNumber = "AC001";
            var transactionType = "D";
            var amountString = "100.00";

            // Act
            var result = transactionService.PerformTransaction(dateString, accountNumber, transactionType, amountString);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void PerformTransaction_WithInvalidData_ShouldReturnErrorMessage()
        {
            // Arrange
            var transactionService = new TransactionService();
            var dateString = "20230805";
            var accountNumber = "AC004";
            var transactionType = "X";
            var amountString = "400.00";

            // Act
            var result = transactionService.PerformTransaction(dateString, accountNumber, transactionType, amountString);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("Invalid input data!", result);
        }

        [Fact]
        public void GetTransactionByAccountAndDate_WithValidData_ShouldReturnTransactions()
        {
            // Arrange
            var transactionService = new TransactionService();
            var accountNumber = "AC001";
            var date = "20230605";

            // Act
            var result = transactionService.GetTransactionByAccountAndDate(accountNumber, date);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetTransactionByAccountAndDate_WithInvalidData_ShouldReturnEmptyList()
        {
            // Arrange
            var transactionService = new TransactionService();
            var accountNumber = "ACD001";
            var date = "20230605";

            // Act
            var result = transactionService.GetTransactionByAccountAndDate(accountNumber, date);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllTransaction_ShouldReturnTransactionList()
        {
            // Arrange
            var transactionService = new TransactionService();

            // Act
            var result = transactionService.GetAllTransactions();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }
    }
}