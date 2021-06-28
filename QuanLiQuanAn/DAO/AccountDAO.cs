using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance {
            get { if (instance == null) instance = new AccountDAO(); return AccountDAO.instance; }
            set => instance = value; }
        private AccountDAO() { }
        public bool Login(string uName,string pw) {
            string query = "USP_Login @userName , @password";
            System.Data.DataTable rs =dataProvider.Instance.ExcuteQuery(query, new object[] { uName, pw });
            return rs.Rows.Count > 0 ? true : false ;
        }
        public Account GetAccountByUserName(string UserName)
        {
            string query = "select * from Account where username='"+UserName+"'";
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                return new Account(item);
            }
            return null;
        }
        public bool UpdateAccount(Account account,string newpassword)
        {
            int data = 0;
            string query = "USP_EditInfoAccount @userName , @pw , @displayName , @newPassword ";
            data = dataProvider.Instance.ExcuteNonQuery(query,new object[] { account.UserNanme,account.PassWord,account.DisplayName,newpassword });
            return data > 0;
        }
    }
}
