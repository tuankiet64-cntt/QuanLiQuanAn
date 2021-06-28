using QuanLiQuanAn.DAO;
using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class FInfo : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; ChangePosition(); }
        }
        public FInfo(Account account)
        {
            InitializeComponent();
            this.LoginAccount = account;
        }
        void ChangePosition()
        {
            txtUserName.Text = loginAccount.UserNanme;
            txtDisplayName.Text = loginAccount.DisplayName;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            string pw = txtPW.Text;
            string newpw = txtNewPW.Text;
            string replayNewpw = txtReplaceNew.Text;
            if (!newpw.Equals(replayNewpw))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới");
            }
            else
            {
                if (AccountDAO.Instance.UpdateAccount(new Account(userName, pw, loginAccount.Id, displayName, loginAccount.Type), replayNewpw)) {
                    MessageBox.Show("Cập nhật thông tin thành công");
                    
                }
                else
                {
                    MessageBox.Show("Vui Lòng kiểm tra lại mật khẩu");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
