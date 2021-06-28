using QuanLiQuanAn.DAO;
using QuanLiQuanAn.DTO;
using System;
using System.Data;
using System.Data.SqlClient;
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
            loadFood();
            Resetdtpk();
            LoadBill(dtpkCheckin.Value, dtpkCheckout.Value);
        }
        #region Method
        void loadFood()
        {
            string query = "select * from food";
            dtgvFood.DataSource = dataProvider.Instance.ExcuteQuery(query);
        }
        void loadAccountList()
        {
            string query = "select displayname as [Tên hiển thị] from Account";
            dtgvAccount.DataSource = dataProvider.Instance.ExcuteQuery(query);
        }
        void LoadBill(DateTime checkin, DateTime checkout)
        {
            float totalBill=0;
           
            DataTable data = BillDAO.Instance.getListFromDate(checkin,checkout);
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
            LoadBill(dtpkCheckin.Value,dtpkCheckout.Value);
        }
        #endregion
        void Resetdtpk()
        {
            DateTime today = DateTime.Now;
            //Lấy ngày tháng hôm này và chọn ngày 1
            dtpkCheckin.Value = new DateTime(today.Year,today.Month,1);
            //Lấy cuối tháng công thêm 1 để qua tháng sau rồi trừ lại 1 ngày
            dtpkCheckout.Value = dtpkCheckin.Value.AddMonths(1).AddDays(-1);

        }
    }
}
