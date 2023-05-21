using System.ComponentModel.DataAnnotations;

namespace VioRentals.Models;

public class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter customer's name.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Please enter customer's surname.")]
    //[StringLength(100)]
    //public string Surname { get; set; }

    //[Display(Name = "Date of Birth")]
    //[AgeValidation]
    //public DateTime? DateOfBirth { get; set; }

    //public bool IsSubscribedToNewsletter { get; set; }

    //public MembershipType? MembershipType { get; set; }

    //[Display(Name = "Membership Type")] public byte MembershipTypeId { get; set; }
}