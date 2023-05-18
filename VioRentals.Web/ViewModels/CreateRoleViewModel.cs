using System.ComponentModel.DataAnnotations;

namespace VioRentals.ViewModels;

public class CreateRoleViewModel
{
    [Required] public string RoleName { get; set; }
}