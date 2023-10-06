using BankAccountInterest.Extension;
using BankAccountInterest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BankAccountInterest.Service
{
    public class InterestRuleService
    {
        // Better to move away from the source code
        const string ruleFile = @"Data\rules.json";
        const string ruleWriteFilePath = "..//..//.//..//Data\\rules.json";

        public string ProcessInteresRule(string dateString, string ruleString, string rateString)
        {
            if (ValidateData(dateString, ruleString, rateString))
            {
                var rate = decimal.Parse(rateString);
                return ProcessRule(dateString, ruleString, rate);
            }
            return "Invalid input data!";
        }

        public string ProcessRule(string dateString, string ruleString, decimal rate)
        {
            try
            {
                if (rate > 0 && rate < 100)
                {
                    var allRuleItems = GetAllRules().ToList();
                    var rule = allRuleItems.FirstOrDefault(rule => rule.Date.Equals(dateString) && rule.RuleId.Equals(ruleString));
                    if (rule != null)
                    {
                        rule.Rate = rate;
                    }
                    else
                    {
                        var newRule = new InterestRule()
                        {
                            Date = dateString,
                            RuleId = ruleString,
                            Rate = rate
                        };
                        allRuleItems.Add(newRule);
                    }
                    var updatedRuleList = JsonConvert.SerializeObject(allRuleItems, Formatting.Indented);
                    File.WriteAllText(ruleWriteFilePath, updatedRuleList);
                    return AllRules(allRuleItems);
                }
                return "Invalid interest rate";
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods

        private bool ValidateData(string dateString, string ruleString, string rateString)
        {
            if (AccountDataValidator.ValidateDate(dateString))
            {
                if (AccountDataValidator.ValidateRule(ruleString))
                {
                    return AccountDataValidator.ValidateDecimalAmount(rateString);
                }
            }
            return false;
        }

        private IEnumerable<InterestRule> GetAllRules()
        {
            try
            {
                IEnumerable<InterestRule> ruleItems = null;
                if (File.Exists(ruleFile))
                {
                    using StreamReader r = new StreamReader(ruleFile);
                    string json = r.ReadToEnd();
                    ruleItems = JsonConvert.DeserializeObject<IEnumerable<InterestRule>>(json).ToList();
                }
                if (ruleItems.Any())
                {
                    return ruleItems;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string AllRules(List<InterestRule> allRules)
        {
            var response = new StringBuilder();
            if (allRules.Any())
            {
                foreach (var rule in allRules)
                {
                    _ = response.Append($"{rule.Date} - {rule.RuleId} - {rule.Rate}% \n");
                }
            }
            return response.ToString();
        }

        #endregion
    }
}
