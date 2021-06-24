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
        }
        void loadAccountList()
        {
            string query = "select displayname as [Tên hiển thị] from Account";
            dataProvider provider = new dataProvider();
            dtgvAccount.DataSource = provider.ExcuteQuery(query);
        }

    }
}
