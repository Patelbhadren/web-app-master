using System;
using System.Collections.Generic;

namespace UI.Models;

public partial class Registration
{
    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Id { get; set; }
}
