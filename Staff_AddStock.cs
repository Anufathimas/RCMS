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
    public partial class Staff_AddStock : Form
    {
        BaseConnection con = new BaseConnection();
        public static string sid = "";
        public Staff_AddStock()
        {
            InitializeComponent();
            fillcampidcombo();
            fillgrid();
            fillcombo();
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

        public void fillgridstock()
        { 
        try
        {
            string status = "Stock";
            string query = "select category,sum(quantity) as stock from stock_details where campid='" + comboBox2.Text + "' and process='" + status + "' group by category";
            DataSet ds = con.ret_ds(query);
            dstock.DataSource = ds.Tables[0].DefaultView;

          

           
            string query1 = "select category,quantity from stock_details where campid='" + comboBox2.Text + "' and process='"+status+"'";

            DataSet ds1 = con.ret_ds(query1);
            dincoming.DataSource = ds1.Tables[0].DefaultView;


            string status1 = "Released";
            string query2 = "select category,quantity from stock_details where campid='" + comboBox2.Text + "' and process='" + status1 + "'";

            DataSet ds2 = con.ret_ds(query2);

            doutgoing.DataSource = ds2.Tables[0].DefaultView;

            string query4 = "select category,sum(quantity) as stock from stock_details where campid='" + comboBox2.Text + "' and process='" + status1 + "' group by category";

            DataSet ds4 = con.ret_ds(query4);

            doutgoing1.DataSource = ds4.Tables[0].DefaultView;


            string query3 = "select category,quantity from stock where campid='" + comboBox2.Text + "' and district='" + Program.district + "'";
            DataSet ds3 = con.ret_ds(query3);
            dstock1.DataSource = ds3.Tables[0].DefaultView;
            dstock2.DataSource = ds3.Tables[0].DefaultView;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                fillstockid();
                string status = "Stock";
                string q = "insert into Stock_details values(" + sid + ",'" + Program.district + "','" + comboBox2.Text + "','" + cat.Text + "'," + quantity.Text + ",'" + status + "','" + Program.csid + "')";
                if (con.exec1(q) > 0)
                {
                    addstock();
                    MessageBox.Show("Stock details added......");
                    cat.Text = "";
                    quantity.Text = "";
                    fillgridstock();
                }
            }
            else
            {
                MessageBox.Show("Please select a camp......");
            }

        }

        public void addstock()
        {
            string c = "";
            string q = "";
            int q1 = 0;
            string query = "select quantity from stock where district='" + Program.district + "' and campid='" + comboBox2.Text + "' and category='"+cat.Text+"'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {

                q = dr[0].ToString();
                q1 = Convert.ToInt32(q) + Convert.ToInt32(quantity.Text);
                string qu2 = "update stock set quantity=" + q1 + " where category='" + cat.Text + "'and district='" + Program.district + "' and campid='" + comboBox2.Text + "'";
                con.exec(qu2);

            }
            else
            {
                string quer = "insert into stock values('" + Program.district + "','" + comboBox2.Text + "','" + cat.Text + "'," + quantity.Text + ")";
                con.exec(quer);
            }
        }

        public void fillstockid()
        {
            try
            {
                string query = "select isnull(max(sid),100)+1 from Stock_details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    sid = dr[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabControl1.Enabled = true;
            if (comboBox2.Text != "")
            {
                fillgridstock();
                fillreqcombo();
            }
        }

        public void fillreqcombo()
        { 
             reqcombo.Items.Clear();
             string q = "select orderid from Order_details where Collectioncenter='" + comboBox2.Text + "' and status='Approved'";
             SqlDataReader dr = con.ret_dr(q);
             while (dr.Read())
             {

                 reqcombo.Items.Add(dr[0].ToString());

             }
        
        
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select category,Requested from order_Details where orderid='" + reqcombo.Text + "'";
            DataSet ds1 = con.ret_ds(query);
            dfinal.DataSource = null;
            dreq.DataSource = ds1.Tables[0].DefaultView;


            itemcombo.Items.Clear();
            SqlDataReader dr = con.ret_dr(query);
            while (dr.Read())
            {

                itemcombo.Items.Add(dr[0].ToString());

            }
        }

        private void itemcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select quantity from Stock where category='" + itemcombo.Text + "' and district='" + Program.district + "' and campid='"+comboBox2.Text+"'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {

                ts.Text = dr[0].ToString();

            }
            else
            {
                ts.Text = "0";
                ta.Text = "0";
            }

            string q = "select requested from order_Details where orderid='" + reqcombo.Text + "' and category='" + itemcombo.Text + "'";
            SqlDataReader dr1 = con.ret_dr(q);
            if (dr1.Read())
            {

                tr.Text = dr1[0].ToString();

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if((Convert.ToInt32(ta.Text)<=Convert.ToInt32(ts.Text))&&(Convert.ToInt32(ta.Text)<=Convert.ToInt32(tr.Text)))
            {string status1 = "Released";
                int n=Convert.ToInt32(ts.Text)-Convert.ToInt32(ta.Text);
                fillstockid();
                string q1 = "insert into Stock_details values(" + sid + ",'" + Program.district + "','" + comboBox2.Text + "','" + itemcombo.Text + "'," + ta.Text + ",'" + status1 + "','" + Program.csid + "')";
                if (con.exec1(q1) > 0)
                { 
                    string q="update Stock set quantity="+n+" where category='"+ itemcombo.Text + "' and district='" + Program.district + "' and campid='"+comboBox2.Text+"'";
                    if (con.exec1(q) > 0)
                    {
                        string s="Supplied";
                      
                        string quer = "update order_Details set Supplied=" + ta.Text + ",status='" + s + "' where category='" + itemcombo.Text + "' and orderid='" + reqcombo.Text + "'";
                        if (con.exec1(quer) > 0)
                        {
                            MessageBox.Show("Stock Released....");
                            string query = "select category,Requested,Supplied from order_Details where orderid='" + reqcombo.Text + "' and status='"+s+"'";
                            DataSet ds1 = con.ret_ds(query);
                            fillgridstock();
                            itemcombo.Items.Remove(itemcombo.Text);
                            dfinal.DataSource = ds1.Tables[0].DefaultView;
                            ta.Text = "";
                            ts.Text = "";
                            tr.Text = "";
                            itemcombo.Text = "";

                        }
                    }
                }
            }

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
