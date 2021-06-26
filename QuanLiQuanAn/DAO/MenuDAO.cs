using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace QuanLiQuanAn.DAO
{
    class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance { 
            get => instance ?? new MenuDAO(); 
            set => instance = value; }
        public List<Menu> GetListByid(int idTable)
        {
            List<Menu> listMenu = new List<Menu>();
            DataTable data =dataProvider.Instance.ExcuteQuery("select Food.name,Food.price,bi.count,Food.price*bi.count as totalPrice  from bill as b , billInfo as bi ,Food  where b.id=bi.idbill and bi.idFood = Food.id and b.idtable=" + idTable);
            foreach(DataRow row in data.Rows)
            {
                Menu menu = new Menu(row);
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}
