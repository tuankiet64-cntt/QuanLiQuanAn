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
        public static int TableWidth=100;
        public static int Tableheight=100;
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
            foreach(DataRow  item in data.Rows)
            {
                table table = new table(item);   
                tablesList.Add(table);
            }
            return tablesList;
        }
    }
}
