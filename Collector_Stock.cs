using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace RCMS
{
    public partial class Collector_Stock : Form
    {
        BaseConnection con = new BaseConnection();
        public Collector_Stock()
        {
            InitializeComponent();
            fillfullgrid();
            fillcampidcombo();
        }

        public void fillcampidcombo()
        {
            try
            {

                combocid.Items.Clear();
                string query = "select CampId from CollectionCenter_Details where district='" + Program.district + "'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {

                    combocid.Items.Add(dr[0].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
       

        public void fillfullgrid()
        {
            string query = "select category,quantity from stock where district='" + Program.district + "'";

            DataSet ds = con.ret_ds(query);
            dstock.DataSource = ds.Tables[0].DefaultView;

            SqlDataReader dr = con.ret_dr(query);
            int i = 0;
            while (dr.Read())
            {
                 Series series = this.chart1.Series.Add(dr[0].ToString());

                // Add point.
                 series.Points.Add(Convert.ToInt32(dr[1].ToString()));
               

            }


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dstock_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void combocid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select category,quantity from stock where district='" + Program.district + "' and campid='"+combocid.Text+"'";

            DataSet ds = con.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }



       
    }
}
