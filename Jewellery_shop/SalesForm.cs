using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Jewellery_shop
{
    public partial class SalesForm : Form
    {
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";

        decimal totalBill = 0;
        public SalesForm()
        {
            InitializeComponent();
        }
        void LoadCustomers()
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerID, CustomerName FROM Customers", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbCustomer.DisplayMember = "CustomerName";
            cmbCustomer.ValueMember = "CustomerID";
            cmbCustomer.DataSource = dt;
        }

        void LoadItems()
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT ItemID, ItemName FROM JewelleryItems", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbItem.DisplayMember = "ItemName";
            cmbItem.ValueMember = "ItemID";
            cmbItem.DataSource = dt;
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadItems();
            dgvBill.Columns.Add("Item", "Item");
            dgvBill.Columns.Add("Quantity", "Quantity");
            dgvBill.Columns.Add("Price", "Price");
            dgvBill.Columns.Add("Total", "Total");
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conStr);

            string query = "SELECT SellingPrice FROM JewelleryItems WHERE ItemID=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", cmbItem.SelectedValue);

            con.Open();
            txtPrice.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
            {
                int qty = Convert.ToInt32(txtQuantity.Text);
                decimal price = Convert.ToDecimal(txtPrice.Text);

                txtTotal.Text = (qty * price).ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int qty = Convert.ToInt32(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            decimal total = qty * price;

            dgvBill.Rows.Add(cmbItem.Text, qty, price, total);

            totalBill += total;
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔴 1. VALIDATION
                if (cmbCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Please select a customer");
                    return;
                }

                if (dgvBill.Rows.Count == 0)
                {
                    MessageBox.Show("No items in bill");
                    return;
                }

                SqlConnection con = new SqlConnection(conStr);
                con.Open();

                int userID = 1; // later replace with logged-in user

                // 🔴 2. GENERATE UNIQUE INVOICE NUMBER
                string invoiceNo = "INV" + DateTime.Now.Ticks;

                decimal discount = 0; // you can add textbox later
                decimal finalAmount = totalBill - discount;

                // 🔴 3. INSERT INTO SALES + GET SALE ID
                string saleQuery = @"INSERT INTO Sales 
        (CustomerID, UserID, SaleDate, TotalAmount, Discount, FinalAmount, PaymentMethod, InvoiceNo) 
        OUTPUT INSERTED.SaleID 
        VALUES (@c,@u,GETDATE(),@t,@d,@f,@pm,@inv)";

                SqlCommand cmd = new SqlCommand(saleQuery, con);

                cmd.Parameters.AddWithValue("@c", cmbCustomer.SelectedValue);
                cmd.Parameters.AddWithValue("@u", userID);
                cmd.Parameters.AddWithValue("@t", totalBill);
                cmd.Parameters.AddWithValue("@d", discount);
                cmd.Parameters.AddWithValue("@f", finalAmount);
                cmd.Parameters.AddWithValue("@pm", "Cash"); // later make dropdown
                cmd.Parameters.AddWithValue("@inv", invoiceNo);

                int saleID = (int)cmd.ExecuteScalar(); // 🔥 IMPORTANT

                // 🔴 4. INSERT INTO SALEDETAILS
                foreach (DataGridViewRow row in dgvBill.Rows)
                {
                    if (row.Cells["ItemID"].Value == null) continue;

                    SqlCommand cmd2 = new SqlCommand(
                        @"INSERT INTO SaleDetails 
                (SaleID, ItemID, Quantity, Price, Total) 
                VALUES (@sid,@iid,@q,@p,@tot)", con);

                    cmd2.Parameters.AddWithValue("@sid", saleID);
                    cmd2.Parameters.AddWithValue("@iid", row.Cells["ItemID"].Value);
                    cmd2.Parameters.AddWithValue("@q", Convert.ToInt32(row.Cells["Quantity"].Value));
                    cmd2.Parameters.AddWithValue("@p", Convert.ToDecimal(row.Cells["Price"].Value));
                    cmd2.Parameters.AddWithValue("@tot", Convert.ToDecimal(row.Cells["Total"].Value));

                    cmd2.ExecuteNonQuery();
                }

                con.Close();

                MessageBox.Show("Invoice Generated Successfully\nInvoice No: " + invoiceNo);

                // 🔴 5. CLEAR UI AFTER SUCCESS
                dgvBill.Rows.Clear();
                totalBill = 0;
                txtTotal.Clear();
                txtQuantity.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnback_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }
    }
}
