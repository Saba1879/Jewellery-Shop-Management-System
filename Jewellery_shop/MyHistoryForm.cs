using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jewellery_shop
{
    public partial class MyHistoryForm : Form
    {
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";

        public MyHistoryForm()
        {
            InitializeComponent();
        }
        void LoadHistory()
        {
            SqlConnection con = new SqlConnection(conStr);

            string query = @"SELECT 
     S.InvoiceNo,
    S.SaleDate,
    SD.ItemID,
    SD.Quantity,
    SD.UnitPrice,
    SD.SubTotal
FROM Sales S
JOIN SaleDetails SD ON S.SaleID = SD.SaleID
WHERE S.UserID = @uid";

            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@uid", LoginForm.LoggedInUserID);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvHistory.DataSource = dt;
        }

        private void MyHistoryForm_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StaffDashboard sf=new StaffDashboard();
            sf.Show();
        }
    }
}
