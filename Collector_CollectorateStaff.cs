using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RCMS
{
    public partial class Collector_CollectorateStaff : Form
    {
        BaseConnection con = new BaseConnection();
        public Collector_CollectorateStaff()
        {
            InitializeComponent();
            fillgrid();
        }

        public void fillgrid()
        {
            try
            {
                string query = "select * from CollectorateStaff_Details";
                DataSet ds = con.ret_ds(query);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
