using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCMS
{
    public partial class Staff_Home : Form
    {
        public Staff_Home()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Staff_CampMembers obj = new Staff_CampMembers();
            obj.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Staff_AddStock obj = new Staff_AddStock();
            obj.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Staff_OrderItems obj = new Staff_OrderItems();
            obj.Show();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
