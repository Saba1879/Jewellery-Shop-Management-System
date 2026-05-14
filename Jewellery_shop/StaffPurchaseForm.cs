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
    public partial class StaffPurchaseForm : Form
    {
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";
        int selectedItemID = -1;
        decimal price = 0;

        public StaffPurchaseForm()
        {
            InitializeComponent();
        }
        void LoadItems()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string query = "SELECT ItemID, ItemName, SellingPrice, QuantityInStock FROM JewelleryItems";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvItems.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message);
            }
        }


        private void StaffPurchaseForm_Load(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void dgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvItems.Rows[e.RowIndex];

                selectedItemID = Convert.ToInt32(row.Cells["ItemID"].Value);
                price = Convert.ToDecimal(row.Cells["SellingPrice"].Value);

                txtTotal.Clear();
                txtQty.Clear();
            }
        }


        private void btnBuy_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedItemID == 0)
                {
                    MessageBox.Show("Please select an item");
                    return;
                }

                if (txtQty.Text == "")
                {
                    MessageBox.Show("Enter quantity");
                    return;
                }

                int qty = Convert.ToInt32(txtQty.Text);

                if (qty <= 0)
                {
                    MessageBox.Show("Invalid quantity");
                    return;
                }

                // ✅ Calculate Total
                decimal total = price * qty;

                // ✅ SHOW IN TEXTBOX
                txtTotal.Text = total.ToString();

                SqlConnection con = new SqlConnection(conStr);
                con.Open();

                int userID = LoginForm.LoggedInUserID;
                int customerID = 1;

                string invoiceNo = "INV" + DateTime.Now.Ticks;

                SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Sales 
        (CustomerID, UserID, SaleDate, TotalAmount, Discount, FinalAmount, PaymentMethod, InvoiceNo)
        OUTPUT INSERTED.SaleID
        VALUES (@c,@u,GETDATE(),@t,0,@t,'Cash',@inv)", con);

                cmd.Parameters.AddWithValue("@c", customerID);
                cmd.Parameters.AddWithValue("@u", userID);
                cmd.Parameters.AddWithValue("@t", total);
                cmd.Parameters.AddWithValue("@inv", invoiceNo);

                int saleID = (int)cmd.ExecuteScalar();

                SqlCommand cmd2 = new SqlCommand(
                @"INSERT INTO SaleDetails 
        (SaleID, ItemID, Quantity, UnitPrice, SubTotal) 
        VALUES (@sid,@iid,@q,@p,@tot)", con);

                cmd2.Parameters.AddWithValue("@sid", saleID);
                cmd2.Parameters.AddWithValue("@iid", selectedItemID);
                cmd2.Parameters.AddWithValue("@q", qty);
                cmd2.Parameters.AddWithValue("@p", price);
                cmd2.Parameters.AddWithValue("@tot", total);

                cmd2.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Purchase Successful!\nTotal Bill: " + total);

                

                LoadItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            StaffDashboard sf= new StaffDashboard();
            sf.Show();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != "" && selectedItemID != 0)
            {
                int qty = Convert.ToInt32(txtQty.Text);
                decimal total = price * qty;

                txtTotal.Text = total.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadItems();
            txtQty.Clear();
            txtTotal.Clear();
        }
    }
}
