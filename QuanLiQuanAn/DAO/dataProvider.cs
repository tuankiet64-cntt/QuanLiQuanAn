using System.Data;
using System.Data.SqlClient;

namespace QuanLiQuanAn.DAO
{
    class dataProvider
    {
        private static dataProvider instance;
        //Khi viết query có 2 param thì phải cách nhau bởi vì nó cắt bởi khoảng cách nên dính dấu , sẽ lỗi
        string StringConnection = @"Data Source=LAPTOP-ASKIDI2M;Initial Catalog=Quanliquancafe;Integrated Security=True";

        public static dataProvider Instance
        {
            get { if (instance == null) instance = new dataProvider(); return dataProvider.instance; }
            private set => instance = value;
        }
        // ExcuteQuery dùng để thực thi câu query với kết quả trả về là datatable
        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            /**
             * Using sau khi sử dụng connect nó có bị lỗi thì vẫn đc giải phóng
             */
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                // Có nghĩa là có biến truyền vào
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    // cắt câu query bởi dấu space
                    foreach (string item in listPara)
                    {
                        // tìm giá trị có @ gán vào item với parameter ở vị trí tương ứng trong mảng
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }

            return data;
        }
        // sử dụng để update và kết quả trả về là số lượng update thành công
        public int ExcuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            /**
             * Using sau khi sử dụng connect nó có bị lỗi thì vẫn đc giải phóng
             */
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                // Có nghĩa là có biến truyền vào
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    // cắt câu query bởi dấu space
                    foreach (string item in listPara)
                    {
                        // tìm giá trị có @ gán vào item với parameter ở vị trí tương ứng trong mảng
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();
                connection.Close();
            }
            return data;
        }
        // sử dụng để count(*) có bao nhiêu record
        public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            /**
             * Using sau khi sử dụng connect nó có bị lỗi thì vẫn đc giải phóng
             */
            using (SqlConnection connection = new SqlConnection(StringConnection))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                // Có nghĩa là có biến truyền vào
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    // cắt câu query bởi dấu space
                    foreach (string item in listPara)
                    {
                        // tìm giá trị có @ gán vào item với parameter ở vị trí tương ứng trong mảng
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }


    }
}
