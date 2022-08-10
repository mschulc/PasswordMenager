using Microsoft.EntityFrameworkCore;
using PasswordMenager.Entities;
using System.Linq;

namespace PasswordMenager.Services;

public class UserService
{
    public static void Add(User user, PasswordsDbContext db)
    {
        db.Users.Add(user);
        db.SaveChanges();
    }

    public static void Delete(int id, PasswordsDbContext db)
    {
        var user = db.Users.FirstOrDefault(x => x.Id == id);

        if(user != null)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }

    public static User GetById (int id, PasswordsDbContext db)
    {
        var user = db.Users.Include(x => x.Passwords).FirstOrDefault(x => x.Id == id);
        return user;
    }

    public static int LoginValidateUser (string login, string password, PasswordsDbContext db)
    {
        var db1 = new PasswordsDbContext();
        var user = db1.Users.FirstOrDefault(x => x.Login == login && x.Password == password);
        if (user == null) return -1;
        else return user.Id;
    }

    public static bool FindUser(string login, PasswordsDbContext db)
    {
        var user = db.Users.FirstOrDefault(x => x.Login == login);
        if (user == null) return true;
        else return false;
    }

    public static User FindUserLogin(string login, PasswordsDbContext db)
    {
        var user = db.Users.FirstOrDefault(x => x.Login == login);
        return user;
    }

    public static bool UpdateUser(User editUser, int id, PasswordsDbContext db)
    {
        var user = db.Users.FirstOrDefault(x => x.Id == id);

        if(user != null)
        {
            user.Email = editUser.Email;
            user.Password = editUser.Password;
            db.SaveChanges();
            return true;
        }
        else
        {
            return false;
        }
    }
}
