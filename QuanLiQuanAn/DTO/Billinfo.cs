using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DTO
{
    public class Billinfo
    {
        public Billinfo(int id,int idbill,int idFood,int count)
        {
            this.Id = id;
            this.IdFood = idFood;
            this.Idbill = idbill;
            this.Count = count;
        }
        private int id;
        private int idbill;
        private int idFood;
        private int count;

        public int Id { get => id; set => id = value; }
        public int Idbill { get => idbill; set => idbill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }

        public Billinfo(DataRow row) {
            this.Id = (int)row["id"];
            this.IdFood = (int)row["idFood"] ;
            this.Idbill = (int)row["idbill"];
            this.Count = (int)row["count"];
        }
    }
}
