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
        public bool InsertBill(int id)
        {
            int data = 0;
            data=dataProvider.Instance.ExcuteNonQuery("USP_InstertBill @idTable",new object[]{id});
            return data>0;
        }
        public int selectMaxIdBill(int id)
        {
            try
            {
                int idbill = (int)dataProvider.Instance.ExcuteScalar("select max(id) from bill where idtable =" + id);
                return idbill;
            }
            catch (Exception ex)
            {
                return 1;
            }
            
        }
        public bool BillCheckout(int id,int discount) {
            int data = 0;
            string query = "update bill set status =1,discount="+discount+" where id= " + id;
            data=dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool swithtable(int idtable1,int idtable2)
        {
            int data = 0;
            string query = "USP_switchtable @idTable1 , @idTable2 ";
            data = dataProvider.Instance.ExcuteNonQuery(query, new object[] { idtable1, idtable2 });
            return data > 0;
        }
    }
}
