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
        Account loginAccount;

        public Account LoginAccount { get => loginAccount; set => loginAccount = value; }

        public fAdmin(Account loginAccount)
        {
            this.LoginAccount = loginAccount;
            InitializeComponent();
            loadAccountList();
            Resetdtpk();
            LoadListFood();
            LoadCategory();
            showInfoFood();
            showInfoCate();
            LoadTable();
            showInfoTable();
            showInfoAccount();
            LoadBill(dtpkCheckin.Value, dtpkCheckout.Value);
        }
        #region Method

        void loadAccountList()
        {
            string query = "select displayname as [Tên hiển thị],username as [Tên tài khoản],type as [Loại tài khoản] from Account";
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
        void LoadListFood()
        {
            dtgvFood.DataSource = null;
            dtgvFood.DataSource = FoodDAO.Instance.GetAllListFood();

        }
        private void LoadCategory()
        {
            cbCategory.DataSource = CategoryDAO.Instance.getListCategory();
            cbCategory.DisplayMember = "name";
            cbCategory.ValueMember = "id";
            dtgvCategory.DataSource = CategoryDAO.Instance.getListCategory();
        }
        void LoadTable()
        {
            dtgvTable.DataSource = TableDAO.Intance.loadTableAdmin();
        }
        void Resetdtpk()
        {
            DateTime today = DateTime.Now;
            //Lấy ngày tháng hôm này và chọn ngày 1
            dtpkCheckin.Value = new DateTime(today.Year, today.Month, 1);
            //Lấy cuối tháng công thêm 1 để qua tháng sau rồi trừ lại 1 ngày
            dtpkCheckout.Value = dtpkCheckin.Value.AddMonths(1).AddDays(-1);

        }
        void showInfoFood()
        {
            //clear databinding vì khi refresh đã ta sorce đã thay đổi nên binding lại
            txtIDFood.DataBindings.Clear();
            txtNameFood.DataBindings.Clear();
            txtFoodPrice.DataBindings.Clear();
            // DataBinding đang 2 chiều set thêm option cuối để chỉ đi 1 chiều option là convert type
            txtIDFood.DataBindings.Add("text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never);
            txtNameFood.DataBindings.Add("text", dtgvFood.DataSource, "Tên Món", true, DataSourceUpdateMode.Never);
            txtFoodPrice.DataBindings.Add("text", dtgvFood.DataSource, "Giá tiền", true, DataSourceUpdateMode.Never);

        }
        void showInfoCate()
        {
            txtCategoryID.DataBindings.Clear();
            txtCategoryName.DataBindings.Clear();
            txtCategoryID.DataBindings.Add("text", dtgvCategory.DataSource, "id", true, DataSourceUpdateMode.Never);
            txtCategoryName.DataBindings.Add("text", dtgvCategory.DataSource, "name", true, DataSourceUpdateMode.Never);
        }
        void showInfoAccount()
        {
            txtUserName.DataBindings.Clear();
            txtDisplayName.DataBindings.Clear();
            nbudAccount.DataBindings.Clear();
            txtUserName.DataBindings.Add("text", dtgvAccount.DataSource, "Tên tài khoản", true, DataSourceUpdateMode.Never);
            txtDisplayName.DataBindings.Add("text", dtgvAccount.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never);
            nbudAccount.DataBindings.Add("value", dtgvAccount.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never);
        }
        void showInfoTable()
        {
            txtIDTable.DataBindings.Clear();
            txtNameTable.DataBindings.Clear();
            txtIDTable.DataBindings.Add("text", dtgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never);
            txtNameTable.DataBindings.Add("text", dtgvTable.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never);
        }
        #endregion

        #region event
        private void button6_Click(object sender, System.EventArgs e)
        {
            LoadBill(dtpkCheckin.Value, dtpkCheckout.Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadListFood();
            showInfoFood();
        }

        private void txtIDFood_TextChanged(object sender, EventArgs e)
        {
            int index = 0;
            int i = 0;
            if (txtIDFood.Text.ToString() == null || txtIDFood.Text.ToString() == "")
            {
                return;
            }
            int idFood = Convert.ToInt32(txtIDFood.Text.ToString());
            Food data = FoodDAO.Instance.GetFoodbyID(idFood);
            foreach (Category category in cbCategory.Items)
            {
                if (category.Id == data.Id)
                {
                    index = i;
                }
                i++;
            }
            cbCategory.SelectedIndex = index;
        }
        //Food CRUD
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtNameFood.Text;
            float price = float.Parse(txtFoodPrice.Text.ToString());
            int idCategory = (cbCategory.SelectedItem as Category).Id;
            if (FoodDAO.Instance.insertFood(name, idCategory, price))
            {
                MessageBox.Show("Tạo món ăn Thành công");
            }
            else
            {
                MessageBox.Show("Kiểm tra lại thông tin");
            }
            btnLoadFood.PerformClick();
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txtIDFood.Text);
            string name = txtNameFood.Text;
            float price = float.Parse(txtFoodPrice.Text.ToString());
            int idCategory = (cbCategory.SelectedItem as Category).Id;
            if (FoodDAO.Instance.UpdateFood(name, idCategory, price, idFood))
            {
                MessageBox.Show("Update Thành công");
            }
            else
            {
                MessageBox.Show("Kiểm tra lại thông tin");
            }
            btnLoadFood.PerformClick();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txtIDFood.Text);
            BillInfoDAO.Instance.DeleteBillInfo(idFood);
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa món: {0} \nĐiều này sẽ khiến món ăn này ở tất cả bill bị xóa ! ", txtNameFood.Text), "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (FoodDAO.Instance.DeleteFood(idFood))
                {
                    MessageBox.Show("Xóa thành công");
                }
                else
                {
                    MessageBox.Show("Kiểm tra lại thông tin");
                }
            }
            btnLoadFood.PerformClick();
        }

        //Category CRUD
        private void button7_Click(object sender, EventArgs e)
        {
            LoadCategory();
            showInfoCate();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string nameCategory = txtCategoryName.Text;
            if (CategoryDAO.Instance.insertFood(nameCategory))
            {
                MessageBox.Show("Tạo danh mục thành công");
            }
            else
            {
                MessageBox.Show("Kiểm tra lại thông tin");
            }
            btnReadCate.PerformClick();
        }

        private void btnUpdateCate_Click(object sender, EventArgs e)
        {
            string nameCategory = txtCategoryName.Text;
            int IdCategory = Convert.ToInt32(txtCategoryID.Text);
            try
            {
                if (CategoryDAO.Instance.UpdateFood(nameCategory, IdCategory))
                {
                    MessageBox.Show("Update danh mục thành công");
                }
                else
                {
                    MessageBox.Show("Kiểm tra lại thông tin");
                }
                btnReadCate.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kiểm tra lại thông tin");
            }

        }

        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
            string nameCategory = txtCategoryName.Text;
            int IdCategory = Convert.ToInt32(txtCategoryID.Text);
            try
            {
                if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa Danh Mục: {0} \nĐiều này sẽ khiến món ăn này ở tất cả bill bị xóa ! ", nameCategory), "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CategoryDAO.Instance.DeleteFood(IdCategory))
                    {
                        MessageBox.Show("Xóa danh mục thành công");
                    }
                    else
                    {
                        MessageBox.Show("Kiểm tra lại thông tin");
                    }
                    btnReadCate.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Kiểm tra lại thông tin");
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string displayname = txtDisplayName.Text;
            int type = (int)(nbudAccount.Value);
            try
            {
                if (AccountDAO.Instance.insertAccountByAdmin(username, displayname, type))
                {
                    MessageBox.Show("Thêm tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm tài khoản thất bại \n Vui lòng kiểm tra lại thông tin");
                throw ex;
            }
            btnReadAccount.PerformClick();
        }

        private void btnReadAccount_Click(object sender, EventArgs e)
        {
            loadAccountList();
            showInfoAccount();
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string displayname = txtDisplayName.Text;
            int type = (int)(nbudAccount.Value);
            try
            {
                if (AccountDAO.Instance.UpdateAccountByAdmin(username, displayname, type))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm tài khoản thất bại \n Vui lòng kiểm tra lại thông tin");
                throw ex;
            }
            btnReadAccount.PerformClick();
        }

        private void btnRemoveAccount_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string displayname = txtDisplayName.Text;
            int type = (int)(nbudAccount.Value);
            try
            {
                if (AccountDAO.Instance.DeleteAccountByAdmin(username, loginAccount))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thêm tài khoản thất bại \n Vui lòng kiểm tra lại thông tin");
                throw ex;
            }
            btnReadAccount.PerformClick();
        }
        #endregion

        private void btnReadTable_Click(object sender, EventArgs e)
        {
            LoadTable();
            showInfoTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            try
            {
                if (TableDAO.Intance.insertTable(name))
                {
                    MessageBox.Show("Tạo bàn thành công");
                }
                else
                {
                    MessageBox.Show("Tạo bàn thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin");
            }
            btnReadTable.PerformClick();
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            int idtable = Convert.ToInt32(txtIDTable.Text);
            try
            {
                if (TableDAO.Intance.UpdateTable(name, idtable))
                {
                    MessageBox.Show("Cập nhật bàn thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật bàn thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng kiểm tra lại thông tin");
            }
            btnReadTable.PerformClick();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string name = txtNameTable.Text;
            int idtable = Convert.ToInt32(txtIDTable.Text);
            if (MessageBox.Show(string.Format("Bạn có thật sự muốn xóa bàn : {0} \nĐiều này sẽ khiến bill của bàn này bị xóa ! ", name), "Thông báo", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (TableDAO.Intance.DeleteTable(idtable))
                    {
                        MessageBox.Show("Xóa bàn thành công");
                    }
                    else
                    {
                        MessageBox.Show("Xóa bàn thất bại");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại thông tin");
                }

            }
            btnReadTable.PerformClick();
        }
    }
}
