using System;

namespace LegacyApp
{
    public class User
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string EmailAddress { get; set; }

        public bool HasCreditLimit { get; set; }

        public int CreditLimit { get; set; }

        public Client Client { get; set; }

        public static bool IsValidFirstName(string firname) {
	        return string.IsNullOrEmpty(firname);
        }

        public static bool IsValidSurnameName(string surname) {
	        return string.IsNullOrEmpty(surname);
        }

        public static bool IsValidEmail(string email) {
	        return !email.Contains("@") && !email.Contains(".");
        }

        public static bool IsValidAge(DateTime dateOfBirth) {
            DateTime now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) {
	            age--;
            }

            if (age < 21)
            {
                return false;
            } else {
	            return true;
            }
        }

        public void ApplyClientCategoryToBaseCreditLimitAndSet(int creditLimit) {
            int creditMultiplier = 1;
            switch (Client.SpecialClientCategory) {
	            case SpecialClientCategory.VeryImportantClient:
		            // Skip credit check
		            HasCreditLimit = false;
		            break;
	            case SpecialClientCategory.ImportantClient: {
		            // Do credit check and double credit limit
		            HasCreditLimit = true;
		            creditMultiplier = 2;
		            break;
	            }
	            case SpecialClientCategory.SuperClient: {
		            // Do credit check and double credit limit
		            HasCreditLimit = true;
		            creditMultiplier = 2;
		            break;
	            }
	            default: {
		            // Do credit check
		            HasCreditLimit = true;
		            break;
	            }
            }
				CreditLimit = creditLimit*creditMultiplier;
        }

        public bool DoesNotPassesCreditCheck() {
	        return HasCreditLimit && CreditLimit < 500;
        }
    }
}
