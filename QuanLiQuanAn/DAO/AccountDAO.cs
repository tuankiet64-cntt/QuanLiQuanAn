using System;
using System.Collections.Generic;
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
    }
}
