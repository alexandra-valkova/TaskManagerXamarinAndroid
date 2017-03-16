using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid
{
    public static class AuthenticationService
    {
        public static UserModel LoggedUser { get; set; }

        public static bool IsLogged { get; set; }

        public static void LogOut()
        {
            LoggedUser = null;
            IsLogged = false;
        }
    }
}