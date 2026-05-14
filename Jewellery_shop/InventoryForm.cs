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
    public partial class InventoryForm : Form
    {
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";
        public InventoryForm()
        {
            InitializeComponent();
        }
        void LoadInventory()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);

                string query = "SELECT ItemID, ItemName, QuantityInStock FROM JewelleryItems";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvInventory.DataSource = dt;

                // LOW STOCK ALERT 🔥
                foreach (DataGridViewRow row in dgvInventory.Rows)
                {
                    if (row.Cells["QuantityInStock"].Value != null &&
                        Convert.ToInt32(row.Cells["QuantityInStock"].Value) < 5)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvInventory.CurrentRow.Cells["ItemID"].Value);
            int qty = Convert.ToInt32(txtNewQty.Text);

            SqlConnection con = new SqlConnection(conStr);

            string query = "UPDATE JewelleryItems SET QuantityInStock=@q WHERE ItemID=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@q", qty);
            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Stock Updated");

            LoadInventory();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();

        }
    }
}
