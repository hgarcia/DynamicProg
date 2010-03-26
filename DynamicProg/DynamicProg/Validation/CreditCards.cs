using System;
using System.Text.RegularExpressions;
using DynamicProg;
using DynamicProg.Extensions;
using LaTrompa.Extensions;

namespace LaTrompa.Validation
{
    public class CreditCards
    {

        public static bool IsCreditCardAmericanExpress(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_AMERICAN_EXPRESS);
            }
            return false;
        }

        public static bool IsCreditCardAny(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_AMERICAN_EXPRESS) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_CARTE_BLANCHE) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_DINERS_CLUB) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_DISCOVER) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_EN_ROUTE) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_JCB) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_MASTER_CARD) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_VISA);
            }
            return false;
        }

        public static bool IsCreditCardBigFour(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_AMERICAN_EXPRESS) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_DISCOVER) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_MASTER_CARD) ||
                       Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_VISA);
            }
            return false;
        }

        public static bool IsCreditCardCarteBlanche(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_CARTE_BLANCHE);
            }
            return false;
        }

        public static bool IsCreditCardDinersClub(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_DINERS_CLUB);
            }
            return false;
        }

        public static bool IsCreditCardDiscover(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_DISCOVER);
            }
            return false;
        }

        public static bool IsCreditCardEnRoute(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_EN_ROUTE);
            }
            return false;
        }

        public static bool IsCreditCardJCB(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_JCB);
            }
            return false;
        }

        public static bool IsCreditCardMasterCard(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_MASTER_CARD);
            }
            return false;
        }

        public static bool IsCreditCardVisa(string creditCard)
        {
            if (creditPassesFormatCheck(creditCard))
            {
                creditCard = Utils.CleanCreditCardNumber(creditCard);
                return Regex.IsMatch(creditCard, RegexPattern.CREDIT_CARD_VISA);
            }
            return false;
        }

        private static bool creditPassesFormatCheck(string creditCardNumber)
        {
            creditCardNumber = Utils.CleanCreditCardNumber(creditCardNumber);
            if (creditCardNumber.IsInteger())
            {
                var numArray = new int[creditCardNumber.Length];
                for (int i = 0; i < numArray.Length; i++)
                {
                    numArray[i] = Convert.ToInt16(creditCardNumber[i].ToString());
                }

                return isValidLuhn(numArray);
            }
            return false;
        }

        private static bool isValidLuhn(int[] digits)
        {
            var sum = 0;
            var alt = false;
            for (var i = digits.Length - 1; i >= 0; i--)
            {
                if (alt)
                {
                    digits[i] *= 2;
                    if (digits[i] > 9)
                    {
                        digits[i] -= 9; // equivalent to adding the value of digits
                    }
                }
                sum += digits[i];
                alt = !alt;
            }
            return sum % 10 == 0;
        }

    }
}