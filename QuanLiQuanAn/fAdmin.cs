using QuanLiQuanAn.DAO;
using QuanLiQuanAn.DTO;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLiQuanAn
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            loadAccountList();
            Resetdtpk();
            LoadListFood();
            LoadCategory();
            showInfoFood();
            LoadBill(dtpkCheckin.Value, dtpkCheckout.Value);
        }
        #region Method

        void loadAccountList()
        {
            string query = "select displayname as [Tên hiển thị] from Account";
            dtgvAccount.DataSource = dataProvider.Instance.ExcuteQuery(query);
        }
        void LoadBill(DateTime checkin, DateTime checkout)
        {
            float totalBill = 0;

            DataTable data = BillDAO.Instance.getListFromDate(checkin, checkout);
            foreach (DataRow row in data.Rows)
            {
                totalBill += (float)Convert.ToDouble(row[3].ToString());
            }
            dtgvTotal.DataSource = data;
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTotal.Text = totalBill.ToString("c", culture);
        }
        #endregion

        #region event
        private void button6_Click(object sender, System.EventArgs e)
        {
            LoadBill(dtpkCheckin.Value, dtpkCheckout.Value);
        }
        #endregion
        void Resetdtpk()
        {
            DateTime today = DateTime.Now;
            //Lấy ngày tháng hôm này và chọn ngày 1
            dtpkCheckin.Value = new DateTime(today.Year, today.Month, 1);
            //Lấy cuối tháng công thêm 1 để qua tháng sau rồi trừ lại 1 ngày
            dtpkCheckout.Value = dtpkCheckin.Value.AddMonths(1).AddDays(-1);

        }
        void LoadListFood()
        {
            dtgvFood.DataSource = null;
            dtgvFood.DataSource = FoodDAO.Instance.GetAllListFood();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadListFood();
            showInfoFood();
        }
        private void LoadCategory()
        {
            cbCategory.DataSource = CategoryDAO.Instance.getListCategory();
            cbCategory.DisplayMember = "name";
            cbCategory.ValueMember = "id";
        }
        void showInfoFood()
        {
            //clear databinding vì khi refresh đã ta sorce đã thay đổi nên binding lại
            txtIDFood.DataBindings.Clear();
            txtNameFood.DataBindings.Clear();
            txtFoodPrice.DataBindings.Clear();
            // DataBinding đang 2 chiều set thêm option cuối để chỉ đi 1 chiều option là convert type
            txtIDFood.DataBindings.Add("text", dtgvFood.DataSource, "ID",true,DataSourceUpdateMode.Never);
            txtNameFood.DataBindings.Add("text", dtgvFood.DataSource, "Tên Món", true, DataSourceUpdateMode.Never);
            txtFoodPrice.DataBindings.Add("text", dtgvFood.DataSource, "Giá tiền", true, DataSourceUpdateMode.Never);
        }
        private int idCategory;
        private void txtIDFood_TextChanged(object sender, EventArgs e)
        {
            int index = 0;
            int i = 0;
            if (txtIDFood.Text.ToString() == null || txtIDFood.Text.ToString() == "")
            {
                return ;
            }
            int idFood = Convert.ToInt32(txtIDFood.Text.ToString());
            Food data = FoodDAO.Instance.GetFoodbyID(idFood);
            foreach (Category category in cbCategory.Items)
            {
                if (category.Id == data.Id)
                {
                    index = i;
                    idCategory = category.Id;
                }
                i++;
            }
            cbCategory.SelectedIndex = index;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtNameFood.Text;
            float price = float.Parse(txtFoodPrice.Text.ToString());
            int id = this.idCategory;
            if (FoodDAO.Instance.insertFood(name, id, price))
            {
                MessageBox.Show("Update Thành công");
            }
            else
            {
                MessageBox.Show("Kiểm tra lại thông tin");
            }
            btnLoadFood.PerformClick();
        }
    }
}
