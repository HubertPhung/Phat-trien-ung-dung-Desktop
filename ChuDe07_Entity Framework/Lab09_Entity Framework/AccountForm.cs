using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class AccountForm : Form
    {
        private readonly RestaurantContext _db = new RestaurantContext();

        public AccountForm()
        {
            InitializeComponent();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            LoadRolesIntoFilter();
            LoadAccounts();
        }

        private void LoadRolesIntoFilter()
        {
            var roles = _db.Roles.OrderBy(r => r.Name).Select(r => new { r.Id, r.Name }).ToList();
            roles.Insert(0, new
            {
                Id = 0,
                Name = "-- Tất cả vai trò --"
            });
            cbbRoleFilter.DisplayMember = "Name";
            cbbRoleFilter.ValueMember = "Id";
            cbbRoleFilter.DataSource = roles;
        }

        private void LoadAccounts()
        {
            int roleId = (cbbRoleFilter.SelectedValue is int) ? (int)cbbRoleFilter.SelectedValue : 0;
            string keyword = txtKeyword.Text?.Trim() ?? string.Empty;
            IQueryable<Account> query = _db.Accounts.AsQueryable();

            if (roleId > 0)
            {
                query = query.Where(a => a.Roles.Any(r => r.Id == roleId));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a => a.UserName.Contains(keyword) || a.DisplayName.Contains(keyword));
            }
            var data = query
                .OrderBy(a => a.UserName)
                .Select(a => new { a.Id, a.UserName, a.DisplayName, a.Email, a.Tel, a.IsActive })
                .ToList();
            dgvAccounts.DataSource = data;
        }

        private Account GetSelectedAccount()
        {
            if (dgvAccounts.CurrentRow == null)
                return null;
            var id = (int)dgvAccounts.CurrentRow.Cells["Id"].Value;
            return _db.Accounts.Find(id);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void cbbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var dlg = new UpdateAccountForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadAccounts();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
                return;
            using (var dlg = new UpdateAccountForm(acc.Id))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadAccounts();
                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
                return;

            if (MessageBox.Show("Reset mật khẩu và đặt tài khoản Inactive?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                acc.IsActive = false;
                acc.Password = null; // sẽ fail nếu cột NOT NULL
                try
                {
                    _db.SaveChanges();
                }
                catch
                {
                    // Fallback: nếu cột Password NOT NULL, set rỗng
                    acc.Password = string.Empty;
                    _db.SaveChanges();
                }
                LoadAccounts();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
                return;
            using (var dlg = new ChangePasswordForm(acc.Id))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    LoadAccounts();
                }
            }
        }

        private void ctxDeleteAccount_Click(object sender, EventArgs e)
        {
            btnResetPassword_Click(sender, e);
        }

        private void ctxViewRoles_Click(object sender, EventArgs e)
        {
            Account acc = GetSelectedAccount();
            if (acc == null)
                return;
            using (var dlg = new AccountRolesForm(acc.Id))
            {
                dlg.ShowDialog(this);
            }
        }
    }
}
