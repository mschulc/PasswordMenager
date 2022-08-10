using PasswordMenager.Enums;

namespace PasswordMenager.Entities;

public class Passwords
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public int UserId { get; set; }
    public Types Type { get; set; }
}