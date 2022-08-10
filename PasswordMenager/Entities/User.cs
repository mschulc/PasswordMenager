using System.Collections.Generic;

namespace PasswordMenager.Entities;

public class User
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public virtual List<Passwords>? Passwords { get; set; }
}