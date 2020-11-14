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
    public partial class Collector_Home : Form
    {
        public Collector_Home()
        {
            InitializeComponent();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click_2(object sender, EventArgs e)
        {
            Collector_CollectorateStaff obj = new Collector_CollectorateStaff();
            obj.Show();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            Collector_Camp obj = new Collector_Camp();
            obj.Show();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            Collector_CollectionCenter obj = new Collector_CollectionCenter();
            obj.Show();
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            Collector_Stock obj = new Collector_Stock();
            obj.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Collector_HandleRequests obj = new Collector_HandleRequests();
            obj.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
