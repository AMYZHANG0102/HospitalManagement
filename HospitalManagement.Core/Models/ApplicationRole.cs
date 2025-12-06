/* Hira Ahmad
Summary: ApplicationRole extends IdentityRole to include additional properties for role management.*/

using Microsoft.AspNetCore.Identity;
namespace IdentityManagement.Core.Models;

public class ApplicationRole : IdentityRole
{
    public string? Description { get; set; }
}