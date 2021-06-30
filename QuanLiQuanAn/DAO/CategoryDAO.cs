using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance { get => instance ?? new CategoryDAO(); set => instance = value; }
        
        public List<Category> getListCategory()
        {
            List<Category> listCategory = new List<Category>();
            string query = "select * from FoodCategory";
            DataTable data = dataProvider.Instance.ExcuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                Category category = new Category(item);
                listCategory.Add(category);
            }
            return listCategory;    
        }
        public bool insertFood(string name)
        {
            int data = 0;
            string query = string.Format("insert into FoodCategory(name) values (N'{0}')", name);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool UpdateFood(string name, int idCategory)
        {
            int data = 0;
            string query = string.Format("Update FoodCategory set name=N'{0}'where id={1}", name, idCategory);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }
        public bool DeleteFood(int idCategory)
        {
            int data = 0;
            string query = string.Format("delete from FoodCategory where id={0}", idCategory);
            data = dataProvider.Instance.ExcuteNonQuery(query);
            return data > 0;
        }

    }
}
