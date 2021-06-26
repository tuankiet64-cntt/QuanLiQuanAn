using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DTO
{
    public class table
    {
        public table(string name, string status, int id)
        {
            this.Id = id;
            this.Name = name;
            this.Status = status;
        }
        public table(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
        private string status;
        private string name;
        private int id;

        public string Status { get => status; set => status = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}
