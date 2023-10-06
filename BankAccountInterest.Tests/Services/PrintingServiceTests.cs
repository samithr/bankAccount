using BankAccountInterest.Service;
using Xunit;

namespace BankAccountInterest.Tests.Services
{
    public class PrintingServiceTests
    {
        [Fact]
        public void ProcessPrintingStatement_WithValidData_ShoudReturnTransactionHistory()
        {
            // Arrange
            var printingService = new PrintingService();
            var accountNumber = "AC001";
            var dateString = "202306";

            // Act
            var result = printingService.ProcessPrintingStatement(accountNumber, dateString);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void ProcessPrintingStatement_WithInvalidData_ShoudReturnEmptyString()
        {
            // Arrange
            var printingService = new PrintingService();
            var accountNumber = "AC001";
            var dateString = "202505";

            // Act
            var result = printingService.ProcessPrintingStatement(accountNumber, dateString);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
