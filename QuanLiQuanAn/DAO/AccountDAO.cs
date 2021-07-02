using QuanLiQuanAn.DTO;
using System.Data;
using System.Windows.Forms;

namespace QuanLiQuanAn.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            set => instance = value;
        }
        private AccountDAO() { }
        public bool Login(string uName, string pw)
        {
            string query = "USP_Login @userName , @password";
            System.Data.DataTable rs = dataProvider.Instance.ExcuteQuery(query, new object[] { uName, pw });
            return rs.Rows.Count > 0 ? true : false;
        }
        public Account GetAccountByUserName(string UserName)
        {
            string query = "select * from Account where username='" + UserName + "'";
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool UpdateAccount(Account account, string newpassword)
        {
            int data = 0;
            string query = "USP_EditInfoAccount @userName , @pw , @displayName , @newPassword ";
            data = dataProvider.Instance.ExcuteNonQuery(query, new object[] { account.UserNanme, account.PassWord, account.DisplayName, newpassword });
            return data > 0;
        }
        public bool insertAccountByAdmin(string userName, string DisplayName, int type)
        {
            int data = 0;
            string query = string.Format("insert into Account(username,displayname,type) values ('{0}',N'{1}',{2})", userName, DisplayName, type);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool UpdateAccountByAdmin(string userName, string DisplayName, int type)
        {
            int data = 0;
            string query = string.Format("Update Account set username='{0}',displayname=N'{1}',type = {2} where username='{0}'", userName, DisplayName, type);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool DeleteAccountByAdmin(string userName,Account CurrentAccount)
        {
            if (userName == CurrentAccount.UserNanme)
            {
                MessageBox.Show("Tài khoản hiện đang sử dụng");
                return false;
            }
            int data = 0;
            string query = string.Format("delete from Account where username='{0}'", userName);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
    }
}
