using BankAccountInterest.Service;
using Xunit;

namespace BankAccountInterest.Tests.Services
{
    public class InterestRuleServiceTests
    {
        [Fact]
        public void ProcessInteresRule_WithValidData_ShoudReturnAllRules()
        {
            // Arrange
            var interestRuleService = new InterestRuleService();
            var dateString = "20230615";
            var ruleString = "RULE03";
            var interestRateString = "2.20";

            // Act
            var result = interestRuleService.ProcessInteresRule(dateString, ruleString, interestRateString);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void ProcessInteresRule_WithInvalidData_ShoudReturnErrorMessage()
        {
            // Arrange
            var interestRuleService = new InterestRuleService();
            var dateString = "20240615";
            var interestRateString = "2.20";

            // Act
            var result = interestRuleService.ProcessInteresRule(dateString, string.Empty, interestRateString);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid input data!", result);
        }

        [Fact]
        public void ProcessRule_WithValidData_ShoudReturnAllRules()
        {
            // Arrange
            var interestRuleService = new InterestRuleService();
            var dateString = "20240615";
            var interestRateString = 500;

            // Act
            var result = interestRuleService.ProcessRule(dateString, string.Empty, interestRateString);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void ProcessRule_WithInvalidInterestRate_ShoudReturnErrorMessage()
        {
            // Arrange
            var interestRuleService = new InterestRuleService();
            var dateString = "20240615";
            var interestRateString = 500;

            //20230615 RULE03 2.20

            // Act
            var result = interestRuleService.ProcessRule(dateString, string.Empty, interestRateString);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Invalid interest rate", result);
        }
    }
}
