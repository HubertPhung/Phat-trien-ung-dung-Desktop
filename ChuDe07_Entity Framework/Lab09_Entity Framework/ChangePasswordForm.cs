using System;
using System.Linq;
using System.Windows.Forms;
using Lab09_Entity_Framework.Models;

namespace Lab09_Entity_Framework
{
    public partial class ChangePasswordForm : Form
    {
        private readonly int _accountId;
        private readonly RestaurantContext _db = new RestaurantContext();
        public ChangePasswordForm(int accountId)
        {
            InitializeComponent();
            _accountId = accountId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Account acc = _db.Accounts.Find(_accountId);
            if (acc == null)
            {
                MessageBox.Show("Không tìm thấy tài khoản");
                return;
            }
            if (string.IsNullOrEmpty(txtNew.Text))
            {
                MessageBox.Show("Mật khẩu mới không được trống");
                return;
            }
            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("Xác nhận không khớp");
                return;
            }
            acc.Password = txtNew.Text;
            acc.IsActive = true;
            _db.SaveChanges();
            DialogResult = DialogResult.OK;
        }
    }
}
