using QuanLiQuanAn.DAO;
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
        private Account loginAccount;

        public Account LoginAccount { 
            get => loginAccount;
            set { loginAccount = value; ChangePosition(value.Type); }
             }

        public fManager(Account account)
        {
            InitializeComponent();
            tableLoad();
            loadCategory();
            comboboxDataSource(cbSwitchTable);
            comboboxDataSource(cbMergeTable);
            this.LoginAccount = account;
        }
        #region event
        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            FInfo fi = new FInfo(loginAccount);
            fi.ShowDialog();
            this.LoginAccount = AccountDAO.Instance.GetAccountByUserName(loginAccount.UserNanme);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fAdmin fAdmin = new fAdmin();
            fAdmin.ShowDialog();
        }
        #endregion
        #region Method
        void ChangePosition(int type)
        {
            AdminToolMenuStrip.Enabled = type == 1 ? true : false;
            infoToolScrpit.Text = "Thông tin tài khoản" + " ( "+ loginAccount.DisplayName + " )";
        }
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
      

        private void btnpay_Click(object sender, EventArgs e)
        {
            table table = lstBill.Tag as table; 
            int idBill = BillDAO.Instance.getBillidByTableId(table.Id);
            int discount = (int)numudDiscount.Value > 0 ? (int)numudDiscount.Value : 0;
            float totalPrice = float.Parse(txtTotalPrice.Text.Split(',')[0]);
            float totalPriceFinal = totalPrice - (totalPrice / 100) * discount;
            if (idBill != -1)
            {
                if(MessageBox.Show(string.Format("Thanh toán bàn {0} \n Tổng tiền là : {1} sau khi giảm giá : {2} % ",table.Name,totalPriceFinal,discount),"Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    BillDAO.Instance.BillCheckout(idBill,discount, totalPriceFinal);
                }
            }
            showBill(table.Id);
            tableLoad();
        }

        private void btnswich_Click(object sender, EventArgs e)
        {
            
            int idTable1 = (lstBill.Tag as table).Id;
            int id2Table2 = (cbSwitchTable.SelectedItem as table).Id;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn: {0} sang bản  {1}  ", (lstBill.Tag as table).Name, (cbSwitchTable.SelectedItem as table).Name), "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                BillDAO.Instance.swithtable(idTable1, id2Table2);
                tableLoad();
            }
                
        }
        public void comboboxDataSource(ComboBox cb)
        {
            cb.DataSource = TableDAO.Intance.LoadTableList();
            cb.DisplayMember = "name";
        }

        private void btnMergeTable_Click(object sender, EventArgs e)
        {
            int idTable1 = (lstBill.Tag as table).Id;
            int id2Table2 = (cbMergeTable.SelectedItem as table).Id;
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn gộp bàn: {0} sang bản  {1}  ", (lstBill.Tag as table).Name, (cbMergeTable.SelectedItem as table).Name), "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                BillDAO.Instance.mergeTable(idTable1, id2Table2);
                tableLoad();
            }
        }
        #endregion
    }
}
