﻿using QuanLiQuanAn.DAO;
using QuanLiQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class fManager : Form
    {
        public fManager()
        {
            InitializeComponent();
            tableLoad();
            loadCategory();
        }
        #region event
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            FInfo fi = new FInfo();
            fi.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fAdmin fAdmin = new fAdmin();
            fAdmin.ShowDialog();
        }
        #endregion
        #region Method
        void loadCategory()
        {
            List<Category> ListCategory = CategoryDAO.Instance.getListCategory();
            cbCate.DataSource = ListCategory;
            cbCate.DisplayMember = "name";
        }
        void LoadFoodByCategoryId( int id)
        {
            List<Food> ListFood = FoodDAO.Instance.getFoodByCateGoryId(id);
            cbFood.DataSource = ListFood;
            cbFood.DisplayMember = "name";

        }
        void tableLoad()
        {
            flpTable.Controls.Clear();
            List<table> tableList = TableDAO.Intance.LoadTableList();

            foreach(table item in tableList)
            {   
                Button btn = new Button() { Width=TableDAO.TableWidth ,Height =TableDAO.TableWidth };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += Btn_Click;
                btn.Tag = item;
                switch (item.Status) {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                      
                };
               
            flpTable.Controls.Add(btn);
            }
        }
        public void showBill(int idtable)
        {
            lstBill.Items.Clear();
            float totalPrice = 0;
            List < Menu > listBIllInfo = MenuDAO.Instance.GetListByid(idtable);
            foreach(Menu item in listBIllInfo)
            {
                totalPrice += item.TotalPrice;
                ListViewItem lstItem = new ListViewItem(item.FoodName.ToString());    
                lstItem.SubItems.Add(item.Count.ToString());
                lstItem.SubItems.Add(item.Price.ToString());
                lstItem.SubItems.Add(item.TotalPrice.ToString());
                lstBill.Items.Add(lstItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTotalPrice.Text = totalPrice.ToString("c",culture);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            int idtable = ((sender as Button).Tag as table).Id;
            lstBill.Tag = (sender as Button).Tag;
            showBill(idtable); 
        }
    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if(cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            id = selected.Id;
            LoadFoodByCategoryId(id);
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            table table = lstBill.Tag as table;
            int idBill = BillDAO.Instance.getBillidByTableId(table.Id);
            int idFood = (cbFood.SelectedItem as Food).Id;
            int count =(int)numudFood.Value;
            //Bill chưa tồn tại tạo bill và lấy cái idbill lớn nhất của cái bàn đó
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.Id);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.selectMaxIdBill(table.Id),idFood,count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            showBill(table.Id);
            tableLoad();
        }
        #endregion

        private void btnpay_Click(object sender, EventArgs e)
        {
            table table = lstBill.Tag as table;
            int idBill = BillDAO.Instance.getBillidByTableId(table.Id);
            if (idBill != -1)
            {
                if(MessageBox.Show("Thanh toán bàn "+table.Name,"Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    BillDAO.Instance.BillCheckout(idBill);
                }
            }
            showBill(table.Id);
            tableLoad();
        }
    }
}
