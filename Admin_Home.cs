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
    public partial class Admin_Home : Form
    {
        public Admin_Home()
        {
            InitializeComponent();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Admin_Items obj = new Admin_Items();
            obj.Show();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            Admin_Collector obj = new Admin_Collector();
            obj.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Admin_CollectorateStaff obj = new Admin_CollectorateStaff();
            obj.Show();
        }
    }
}
