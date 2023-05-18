using System.ComponentModel.DataAnnotations;

namespace VioRentals.Models;

public class AgeValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var customer = (Customer)validationContext.ObjectInstance;
        if (customer.MembershipTypeId == MembershipType.Unknown ||
            customer.MembershipTypeId == MembershipType.PayAsYouGo)
            return ValidationResult.Success;
        if (customer.DateOfBirth == null)
            return new ValidationResult("Date of Birth is required.");
        if (customer.DateOfBirth > DateTime.Today)
            return new ValidationResult("Date of Birth cannot be in the future.");
        var age = DateTime.Today.Year - customer.DateOfBirth.Value.Year;
        return age >= 18
            ? ValidationResult.Success
            : new ValidationResult("Customer should be at least 18 years old to go on a membership.");
    }
}