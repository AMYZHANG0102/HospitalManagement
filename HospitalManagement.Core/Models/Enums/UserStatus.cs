/* Amy Zhang
Summary: This enum represents the status of a user account.
- Inactive: The user has deleted their account, therefore the account is disabled.
- Active: User can use their account as normal as long as they provide their credentials. */

namespace HospitalManagement.Core.Models;

public enum UserStatus
{
    Inactive = 0,
    Active = 1
}