using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Jewellery_shop
{
    
    public partial class JewelleryForm : Form
    {
       
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";
        int selectedID = 0;
        public JewelleryForm()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                string query = "SELECT * FROM JewelleryItems";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvJewellery.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Error: " + ex.Message);
            }
        }

        private void JewelleryForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);

            SqlCommand cmd = new SqlCommand("sp_InsertJewellery", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text);
            cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
            cmd.Parameters.AddWithValue("@Material", txtMaterial.Text);
            cmd.Parameters.AddWithValue("@Purity", txtPurity.Text);
            cmd.Parameters.AddWithValue("@Weight", Convert.ToDecimal(txtWeight.Text));
            cmd.Parameters.AddWithValue("@PurchasePrice", Convert.ToDecimal(txtPurchase.Text));
            cmd.Parameters.AddWithValue("@SellingPrice", Convert.ToDecimal(txtSelling.Text));
            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Jewellery Item added");
            LoadData();
        }

        private void dgvJewellery_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvJewellery.Rows[e.RowIndex];

                selectedID = Convert.ToInt32(row.Cells["ItemID"].Value);

                txtItemName.Text = row.Cells["ItemName"].Value.ToString();
                txtCategory.Text = row.Cells["Category"].Value.ToString();
                txtMaterial.Text = row.Cells["Material"].Value.ToString();
                txtPurity.Text = row.Cells["Purity"].Value.ToString();
                txtWeight.Text = row.Cells["Weight"].Value.ToString();
                txtPurchase.Text = row.Cells["PurchasePrice"].Value.ToString();
                txtSelling.Text = row.Cells["SellingPrice"].Value.ToString();
                txtQuantity.Text = row.Cells["QuantityInStock"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);

                SqlCommand cmd = new SqlCommand("sp_UpdateJewellery", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemID", selectedID);
                cmd.Parameters.AddWithValue("@SellingPrice", Convert.ToDecimal(txtSelling.Text));
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Item Updated Successfully");

                LoadData(); // refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);

                SqlCommand cmd = new SqlCommand("sp_DeleteJewellery", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ItemID", selectedID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Item Deleted Successfully");

                LoadData(); // refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemName.Clear();
            txtCategory.Clear();
            txtMaterial.Clear();
            txtPurity.Clear();
            txtWeight.Clear();
            txtPurchase.Clear();
            txtSelling.Clear();
            txtQuantity.Clear();
            txtDescription.Clear();

            selectedID = 0; // VERY IMPORTANT
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard();
            d.Show();
        }
    }
}
