using System;
using System.Collections.Generic;

namespace SE.Models;

public partial class UserInformation
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public bool? IsLocked { get; set; }

    public string Password { get; set; } = null!;

    public int UserType { get; set; }

    public string? Email { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public bool? IsEmailVerified { get; set; }

    public string? ProfilePicture { get; set; }

    public string? PhoneNumber { get; set; }
}
