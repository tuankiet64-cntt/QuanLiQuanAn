using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DTO
{
     public class BIll
    {
        public BIll(int id,DateTime?dateCheckout,DateTime?dateCheckin,int status)
        {
            this.Id = id;
            this.DateCheckin = dateCheckin;
            this.DateCheckout = dateCheckout;
            this.Status = status;
        }

        public BIll(DataRow row)
        {
            this.id = (int)row["id"];
            this.DateCheckin = row["datacheckin"].ToString()!="" ? (DateTime?)row["datacheckin"] : null;
            this.DateCheckout = row["datecheckout"].ToString() != "" ? (DateTime?)row["datecheckout"] : null;
            this.Status = (int)row["status"];
        }

        private int id;
        private int status;
        private DateTime? dateCheckout;
        private DateTime? dateCheckin;

        public int Status { get => status; set => status = value; }
        public DateTime? DateCheckout { get => dateCheckout; set => dateCheckout = value; }
        public DateTime? DateCheckin { get => dateCheckin; set => dateCheckin = value; }
        public int Id { get => id; set => id = value; }
    }
}
