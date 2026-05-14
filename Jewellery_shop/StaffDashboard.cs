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
    public partial class StaffDashboard : Form
    {
        public StaffDashboard()
        {
            InitializeComponent();
        }

        private void btnBuyItem_Click(object sender, EventArgs e)
        {
            new StaffPurchaseForm().Show();
        }

        private void btnMyHistory_Click(object sender, EventArgs e)
        {
            new MyHistoryForm().Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Close();
        }
    }
}
