using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChuDe4
{
 public class AccountEditDialog : Form
 {
 private TextBox txtUserName;
 private TextBox txtDisplayName;
 private TextBox txtPassword;
 private TextBox txtGroup;
 private CheckBox chkActive;
 private Button btnOK;
 private Button btnCancel;

 public string UserName { get => txtUserName.Text; set => txtUserName.Text = value; }
 public string DisplayName { get => txtDisplayName.Text; set => txtDisplayName.Text = value; }
 public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }
 public string Group { get => txtGroup.Text; set => txtGroup.Text = value; }
 public bool IsActive { get => chkActive.Checked; set => chkActive.Checked = value; }

 public AccountEditDialog()
 {
 this.Text = "Tài kho?n";
 this.Width =400;
 this.Height =260;
 this.FormBorderStyle = FormBorderStyle.FixedDialog;
 this.StartPosition = FormStartPosition.CenterParent;
 this.MaximizeBox = false;
 this.MinimizeBox = false;

 var lblU = new Label { Text = "UserName", Location = new Point(10,10) };
 txtUserName = new TextBox { Location = new Point(120,8), Width =240 };
 var lblD = new Label { Text = "DisplayName", Location = new Point(10,45) };
 txtDisplayName = new TextBox { Location = new Point(120,43), Width =240 };
 var lblP = new Label { Text = "Password", Location = new Point(10,80) };
 txtPassword = new TextBox { Location = new Point(120,78), Width =240 };
 var lblG = new Label { Text = "Group", Location = new Point(10,115) };
 txtGroup = new TextBox { Location = new Point(120,113), Width =240 };
 chkActive = new CheckBox { Text = "Active", Location = new Point(120,148) };
 btnOK = new Button { Text = "OK", Location = new Point(200,180), DialogResult = DialogResult.OK };
 btnCancel = new Button { Text = "Cancel", Location = new Point(285,180), DialogResult = DialogResult.Cancel };

 this.Controls.AddRange(new Control[]{ lblU, txtUserName, lblD, txtDisplayName, lblP, txtPassword, lblG, txtGroup, chkActive, btnOK, btnCancel });
 this.AcceptButton = btnOK;
 this.CancelButton = btnCancel;
 }
 }
}
