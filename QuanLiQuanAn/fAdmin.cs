using QuanLiQuanAn.DAO;
using System.Data;
using System.Data.SqlClient;
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
        }
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

    }
}
