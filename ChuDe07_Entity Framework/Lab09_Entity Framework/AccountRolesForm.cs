using System;
using System.Linq;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class AccountRolesForm : Form
    {
        private readonly int _accountId;
        private readonly RestaurantContext _db = new RestaurantContext();

        public AccountRolesForm(int accountId)
        {
            InitializeComponent();
            _accountId = accountId;
        }

        private void AccountRolesForm_Load(object sender, EventArgs e)
        {
            Account acc = _db.Accounts.Find(_accountId);
            if (acc == null)
                return;

            lblAccount.Text = $"Tài khoản: {acc.UserName}";
            var roles = acc.Roles.Select(r => new { r.Id, r.Name, r.Description }).ToList();
            dgvRoles.DataSource = roles;
        }
    }
}
