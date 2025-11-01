using System.Collections.Generic;

namespace Lab09_Entity_Framework.Models
{
    public class Role
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string Description
        {
            get; set;
        }
        public virtual ICollection<Account> Accounts
        {
            get; set;
        }
    }
}
