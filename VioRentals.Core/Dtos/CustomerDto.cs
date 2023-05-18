using System.ComponentModel.DataAnnotations;

namespace VioRentals.Dtos;

public class CustomerDto
{
    public int Id { get; set; }

    [StringLength(100)] public string Name { get; set; }

    [StringLength(100)] public string Surname { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public bool IsSubscribedToNewsletter { get; set; }

    public byte MembershipTypeId { get; set; }
}