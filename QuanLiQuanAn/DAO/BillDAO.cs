using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance { 
            get => instance ?? new BillDAO(); 
          private  set => instance = value; }
        private BillDAO() { }
        /**
         * Thành công trả về bill id
         * không có trả về -1
         */
        public int getBillidByTableId(int id)
        {
            DataTable data= dataProvider.Instance.ExcuteQuery("select * from bill where idtable ='"+id+"' and status =0 ");
            if (data.Rows.Count > 0)
            {
                BIll bill = new BIll(data.Rows[0]);
                return bill.Id;
            }
            return -1;

        }
    }
}
