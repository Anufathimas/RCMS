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
    public partial class Admin_CollectorateStaff : Form
    {
        BaseConnection con = new BaseConnection();
        public static string cid = "";
        public static string ccid = "";
        public static string uid = "";
        public static string utype = "CollectorateStaff";
        public Admin_CollectorateStaff()
        {
            InitializeComponent();
            fillgrid();
            fillcollectoratestaffid();
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

        public void filluserid()
        {
            try
            {
                string query = "select isnull(max(userid),100)+1 from Login";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    uid = dr[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillcollectoratestaffid()
        {
            try
            {
                string query = "select isnull(max(CSid),100)+1 from CollectorateStaff_Details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    cid = dr[0].ToString();
                    tcid.Text = dr[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void cleartab1()
        {
            name.Text = "";
            district.Text = "";
            office.Text = "";
            mailid.Text = "";
            address.Text = "";
            username.Text = "";
            password.Text = "";
            home.Text = "";
            mob.Text = "";
            fillcollectoratestaffid();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                fillcollectoratestaffid();
                filluserid();
                string query1 = "insert into CollectorateStaff_Details values(" + cid + ",'" + name.Text + "','" + district.Text + "','" + mob.Text + "','" + office.Text + "','" + home.Text + "','" + address.Text + "','" + mailid.Text + "'," + uid + ")";
                string query2 = "insert into login values(" + uid + ",'" + username.Text + "','" + password.Text + "','" + utype + "')";

                if (con.exec1(query1) > 0)
                {
                    if (con.exec1(query2) > 0)
                    {
                        MessageBox.Show("Collectorate Staff details added......");

                        cleartab1();
                        fillgrid();
                    }
                    else
                    {
                        MessageBox.Show("Problem encountered while adding login details.....");
                        cleartab1();
                        fillgrid();
                    }

                }
                else
                {
                    MessageBox.Show("Problem encountered while adding Collectorate Staff details.....");
                    cleartab1();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured while inserting data....");
            }
        }

        private void district1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillcombo();
        }

        public void fillcombo()
        {
            try
            {
                comboBox1.Items.Clear();
                string query = "select csid from CollectorateStaff_Details where District='"+district1.Text+"'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from CollectorateStaff_Details where csid='" + comboBox1.Text + "'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                ccid = dr[0].ToString();
                name1.Text = dr[1].ToString();
                mob1.Text = dr[3].ToString();
                office1.Text = dr[4].ToString();
                home1.Text = dr[5].ToString();
                address1.Text = dr[6].ToString();
                mailid1.Text = dr[7].ToString();
            }
        }
        public void cleartab2()
        {
            comboBox1.Text = "";
            name1.Text = "";
            district1.Text = "";
            office1.Text = "";
            mailid1.Text = "";
            address1.Text = "";

            home1.Text = "";
            mob1.Text = "";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update CollectorateStaff_Details set name='" + name1.Text + "',mobileno='" + mob1.Text + "',officeno='" + office1.Text + "',homeno='" + home1.Text + "',address='" + address1.Text + "' where csid='" + ccid + "'";
                if (con.exec1(query) > 0)
                {
                    MessageBox.Show("Collectorate Staff details edited......");
                    fillgrid();
                    cleartab2();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "delete CollectorateStaff_Details where csid='" + ccid + "'";
                if (con.exec1(query) > 0)
                {
                    MessageBox.Show("Collectorate Staff details deleted......");
                    cleartab2();
                    fillgrid();
                }

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
