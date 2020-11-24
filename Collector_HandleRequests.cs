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
    public partial class Collector_HandleRequests : Form
    {
        BaseConnection con = new BaseConnection();
        public Collector_HandleRequests()
        {
            InitializeComponent();
            fillgrid();
            fillcampidcombo();
        }


        public void fillcampidcombo()
        {
            try
            {

                campid.Items.Clear();
                string query = "select CampId from CollectionCenter_Details where district='" + Program.district + "'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {

                    campid.Items.Add(dr[0].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillgrid()
        {
            try
            {
                string query = "select distinct(orderid),district,campid from order_Details where status='Order Placed'";
                DataSet ds = con.ret_ds(query);

                dunprocessed.DataSource = ds.Tables[0].DefaultView;


                SqlDataReader dr = con.ret_dr(query);
                crequest.Items.Clear();
                while (dr.Read())
                {

                    crequest.Items.Add(dr[0].ToString());

                }
                string query1 = "select distinct(orderid),district,campid from order_Details where status='Approved'";
                DataSet ds1 = con.ret_ds(query1);

                dprocessed.DataSource = ds1.Tables[0].DefaultView;


            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void crequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select category,Requested from order_Details where orderid='"+crequest.Text+"'";
                DataSet ds1 = con.ret_ds(query);

                dreq.DataSource = ds1.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void campid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select category,quantity from stock where district='" + Program.district + "' and campid='" + campid.Text + "'";
            
            DataSet ds = con.ret_ds(query);
            dstock.DataSource = ds.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            try
            {
                string status = "Approved";
                string query = "update order_Details set Status='" + status + "',districtsupplied='" + Program.district + "',Collectioncenter='" + campid.Text + "' where orderid='" + crequest.Text + "'";
                if (con.exec1(query) > 0)
                {

                    MessageBox.Show("order no "+crequest.Text+" placed to collection center "+campid.Text);
                }
                fillgrid();
                crequest.Text = "";
                campid.Text = "";
                dstock.DataSource = null;
                dreq.DataSource = null;

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
