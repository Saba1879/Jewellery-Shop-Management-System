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
    public partial class CustomerForm : Form
    {
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";

        int selectedID = 0;
        public CustomerForm()
        {
            InitializeComponent();
        }
        void LoadData()
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);
                string query = "SELECT * FROM Customers";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvCustomer.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);

                string query = "INSERT INTO Customers (CustomerName, Phone, Email, Address) VALUES (@n,@p,@e,@a)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@p", txtPhone.Text);
                cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                cmd.Parameters.AddWithValue("@a", txtAddress.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Customer Added");

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];

                selectedID = Convert.ToInt32(row.Cells["CustomerID"].Value);

                txtName.Text = row.Cells["CustomerName"].Value.ToString();
                txtPhone.Text = row.Cells["Phone"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedID == 0)
            {
                MessageBox.Show("Please select a record first");
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(conStr);

                string query = "UPDATE Customers SET CustomerName=@n, Phone=@p, Email=@e, Address=@a WHERE CustomerID=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@p", txtPhone.Text);
                cmd.Parameters.AddWithValue("@e", txtEmail.Text);
                cmd.Parameters.AddWithValue("@a", txtAddress.Text);
                cmd.Parameters.AddWithValue("@id", selectedID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Customer Updated");

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedID == 0)
            {
                MessageBox.Show("Select a record first");
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(conStr);
                con.Open();

                SqlCommand cmd1 = new SqlCommand("DELETE FROM Sales WHERE CustomerID=@id", con);
                cmd1.Parameters.AddWithValue("@id", selectedID);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM Customers WHERE CustomerID=@id", con);
                cmd2.Parameters.AddWithValue("@id", selectedID);
                cmd2.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Customer Deleted");

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
             txtName.Clear();
    txtPhone.Clear();
    txtEmail.Clear();
    txtAddress.Clear();

    selectedID = 0;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Dashboard d=new Dashboard();
            d.Show();
        }
    }
}
