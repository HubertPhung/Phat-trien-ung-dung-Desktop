using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public static class AppSession
    {
        public static Account CurrentUser
        {
            get; set;
        }
        public static bool IsAdmin()
        {
            Account u = CurrentUser;
            if (u == null || u.Roles == null)
                return false;
            // Admin when has ManageAccounts role
            foreach (Role r in u.Roles)
            {
                if (r.Name == "ManageAccounts")
                    return true;
            }
            return false;
        }
    }
}
