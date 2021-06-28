using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DTO
{
    public class Account
    {
        public Account(string username,string password,int id,string displayname,int type)
        {
            this.userNanme = username;
            this.DisplayName = displayname;
            this.Id = id;
            this.Type = type;
            this.PassWord = password;
        }

        public Account(DataRow row)
        {
            this.userNanme = row["username"].ToString();
            this.DisplayName = row["displayname"].ToString();
            this.Id = (int)row["id"];
            this.Type = (int)row["type"];
            this.PassWord = row["password"].ToString();
        }

        private string userNanme;
        private string passWord;
        private string displayName;
        private int id;
        private int type;

        public string UserNanme { get => userNanme; set => userNanme = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Id { get => id; set => id = value; }
        public int Type { get => type; set => type = value; }
    }
}
