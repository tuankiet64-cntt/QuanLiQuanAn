using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance { get => instance ?? new FoodDAO(); set => instance = value; }

        public List<Food> getFoodByCateGoryId(int id)
        {
            List<Food> listFood = new List<Food>();
            string query = "select * from Food where idCategory ="+id;
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food Food = new Food(item);
                listFood.Add(Food);
            }
            return listFood;
        }
    }
}
