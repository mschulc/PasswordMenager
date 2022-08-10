using PasswordMenager.Entities;
using PasswordMenager.Enums;
using PasswordMenager.Resources;
using System.Collections.Generic;
using System.Linq;

namespace PasswordMenager.Services;

public class PasswordsService
{
    public static void AddPssword(Passwords pass, PasswordsDbContext db)
    {
        db.Passwords.Add(pass);
        db.SaveChanges();
    }

    public static IEnumerable<Passwords> GetAllPasswords(PasswordsDbContext db, int userId)
    {
        var passwords = db.Passwords.Where(x => x.UserId == userId).ToList();

        foreach (var pass in passwords)
        {
            pass.Password = CryptoService.Decrypt(pass.Password, Resource.PrivateKey);
        }
        return passwords;
    }

    public static IEnumerable<Passwords> GetPasswordsByName(PasswordsDbContext db, int userId, string name)
    {
        var passwords = db.Passwords.Where
            (x => x.UserId == userId && x.Name.StartsWith(name)
            ).ToList();
        return passwords;
    }

    public static IEnumerable<Passwords> GetPasswordsByType(PasswordsDbContext db, int userId, string type)
    {
        var passwords = db.Passwords.Where
            (x => x.UserId == userId && x.Type.Equals((Types)CheckTheType(type))
            ).ToList();
        return passwords;
    }

    public static IEnumerable<Passwords> GetPasswordsByLogin(PasswordsDbContext db, int userId, string login)
    {
        var passwords = db.Passwords.Where
            (x => x.UserId == userId && x.Name.StartsWith(login)
            ).ToList();
        return passwords;
    }

    public static Passwords GetById(PasswordsDbContext db, int id)
    {
        var password = db.Passwords.FirstOrDefault(x => x.Id == id);
        return password;
    }

    public static bool Delete(PasswordsDbContext db, int id)
    {
        var password = db.Passwords.FirstOrDefault(x => x.Id == id);
        if (password != null)
        {
            db.Passwords.Remove(password);
            db.SaveChanges();
            return true;
        }
        else
            return false;
    }
    public static void Update(int id, PasswordsDbContext db, Passwords editedPassword)
    {
        var pass = db.Passwords.FirstOrDefault(x => x.Id == id);

        if(pass != null && editedPassword != null)
        {
            pass.Name = editedPassword.Name;
            pass.Password = editedPassword.Password;
            pass.Type = editedPassword.Type;
            pass.Login = editedPassword.Login;
            db.SaveChanges();
        }
    }

    public static int CheckTheType(string type)
    {
        if (type == "Private")
        { return 0;}
        else if (type == "Work")
        { return 1;}
        else if (type == "Family")
        { return 2;}
        else if (type == "Enterteiment")
        { return 3;}
        else if (type == "Games")
        { return 4;}
        else
        { return 5;}
    }
}