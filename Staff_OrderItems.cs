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
    public partial class Staff_OrderItems : Form
    {
        BaseConnection con = new BaseConnection();
        public static string oid = "";
        public Staff_OrderItems()
        {
            InitializeComponent();
            fillcampidcombo();
            fillgrid();
            fillcombo();
            fillorderid();
        }
        public void fillgrid()
        {
            try
            {
                string query = "select * from Category_Details";
                DataSet ds = con.ret_ds(query);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillorderid()
        {
            try
            {
                string query = "select isnull(max(Orderid),100)+1 from order_details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    oid = dr[0].ToString();
                    orderno.Text = oid;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillcombo()
        {
            try
            {
                cat.Items.Clear();
                string query = "select category from Category_Details";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {

                    cat.Items.Add(dr[0].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillcampidcombo()
        {
            try
            {

                comboBox2.Items.Clear();
                string query = "select CampId from CollectionCenter_Details where district='" + Program.district + "'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {

                    comboBox2.Items.Add(dr[0].ToString());

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                tabControl1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {try
        {
            int sup=0;
            string na="";
            string status = "Order Placed";

            string query = "insert into order_details values(" + orderno.Text + ",'" + Program.district + "','" + Program.csid + "','" + cat.Text + "','" + quantity.Text + "'," + sup + ",'" + na + "','" + na + "','"+status+"')";
            if (con.exec1(query) > 0)
            {

                MessageBox.Show("Item order details added......");

                string query1 = "select category,requested from order_details where orderid='"+orderno.Text+"'";
                DataSet ds = con.ret_ds(query1);
                dataGridView3.DataSource = ds.Tables[0].DefaultView;
                cat.Text = "";
                quantity.Text = "";
            }
             }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order has been placed....");
            this.Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
