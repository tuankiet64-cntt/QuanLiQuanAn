using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance { 
            get => instance ?? new BillInfoDAO(); 
            set => instance = value; }
        public BillInfoDAO() { }
        public List<Billinfo> getListInfo( int id)
        {
            List<Billinfo> listbill = new List<Billinfo>();
            DataTable data = dataProvider.Instance.ExcuteQuery("Select * from BillInfo where idBill="+id );
            foreach(DataRow item in data.Rows)
            {
                Billinfo bill = new Billinfo(item);
                listbill.Add(bill);
            }
            return listbill;
        }
        public bool InsertBillInfo(int idbill,int idFood,int count)
        {
            int data = 0;
            data = dataProvider.Instance.ExcuteNonQuery("USP_InstertBillInfo @idbill , @idFood , @count", new object[] { idbill,idFood,count });
            return data > 0;
        }

        public bool DeleteBillInfo(int idfood)
        {
            int data = 0;
            string query = string.Format("delete from billInfo where idFood={0}",idfood);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
    }
}
