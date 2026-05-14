using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jewellery_shop
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        //btnReport
        private void button5_Click(object sender, EventArgs e)
        {
            ReportsForm rf = new ReportsForm();
            rf.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JewelleryForm jf = new JewelleryForm();
            jf.Show();
            //MessageBox.Show("Clicked");

        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm cf = new CustomerForm();
            cf.Show();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            SalesForm sf = new SalesForm();
            sf.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            InventoryForm inf = new InventoryForm();
            inf.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }
    }
}
