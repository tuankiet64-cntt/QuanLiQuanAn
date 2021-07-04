using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    class TableDAO
    {
        private static TableDAO intance;
        public static int TableWidth = 100;
        public static int Tableheight = 100;
        public static TableDAO Intance {
            get { if (intance == null) intance = new TableDAO(); return intance; }
            set => intance = value;
        }
        private TableDAO() { }
        public List<table> LoadTableList()
        {
            List<table> tablesList = new List<table>();
            string query = "USP_LoadTableList";
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                table table = new table(item);
                tablesList.Add(table);
            }
            return tablesList;
        }
        public DataTable loadTableAdmin()
        {
            string query = "select id as [ID],name as [Tên bàn], status as [Trạng thái] from  tableFood";
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            return data;
        }
        public bool insertTable(string name)
        {
            int data = 0;
            string query = string.Format("insert into tableFood(name) values (N'{0}')", name);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool UpdateTable(string name, int idTable)
        {
            int data = 0;
            string query = string.Format("Update tableFood set name=N'{0}'where id={1}", name, idTable);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool DeleteTable(int idTable)
        {
            int data = 0;
            string query = string.Format("delete from tableFood where id={0}", idTable);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
    }
}
