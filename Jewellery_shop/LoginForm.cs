using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Jewellery_shop
{
    public partial class LoginForm : Form
    {
        public static int LoggedInUserID;
        public static string LoggedInRole;
        string conStr = "Data Source=DESKTOP-2E6VN4A\\SQLEXPRESS;Initial Catalog=Jewelery_Shop;Integrated Security=True;TrustServerCertificate=True";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(conStr);

                string query = "SELECT * FROM Users WHERE Username=@u AND PasswordHash=@p";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    LoggedInUserID = Convert.ToInt32(dr["UserID"]);
                    LoggedInRole = dr["Role"].ToString();

                    if (LoggedInRole == "Admin")
                    {
                        new Dashboard().Show();
                    }
                    else
                    {
                        new StaffDashboard().Show();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Login");
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
