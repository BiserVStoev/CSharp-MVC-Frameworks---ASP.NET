﻿namespace CameraBazaar.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using static CameraBazaar.Models.Constants.ValidationMessages;

    public class UsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string username = value as string;

            if (username.Length < 4 || username.Length > 20)
            {
                return new ValidationResult(UsernameValidationMessage);
            }

            foreach (char c in username)
            {
                if (!char.IsLetter(c))
                {
                    return new ValidationResult(UsernameValidationMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
