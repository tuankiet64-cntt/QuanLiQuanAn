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
        public DataTable GetAllListFood()
        {
            string query = "select f.id as ID,f.name as N'Tên món',fc.name as 'Tên Danh Mục',f.price as 'Giá tiền' from food as f join foodCategory as fc on f.idCategory=fc.id  ";
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            return data;
        }
        public Food GetFoodbyID(int id)
        {
            Food food = null;
            string query = "select * from food where id="+id;
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            foreach(DataRow row in data.Rows)
            {
                 food = new Food(row);
                return food;
            }
            return food;

        }
        public bool insertFood(string name,int idCategory,float price)
        {
            int data = 0;
            string query =string.Format("insert into Food(name,idCategory,price) values (N'{0}',{1},{2})",name,idCategory,price);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;            
        }
        public bool UpdateFood(string name, int idCategory, float price,int id)
        {
            int data = 0;
            string query = string.Format("Update Food set name=N'{0}',idCategory={1},price={2} where id={3}", name, idCategory, price,id);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
    }
}
