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
      
    }
}
