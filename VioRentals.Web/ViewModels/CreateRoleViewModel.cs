using Microsoft.Build.Framework;

namespace VioRentals.ViewModels;

public class CreateRoleViewModel
{
    [Required] public string RoleName { get; set; }
}