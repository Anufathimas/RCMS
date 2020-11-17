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
    public partial class Admin_Collector : Form
    {
        BaseConnection con = new BaseConnection();
        public static string cid="";
        public static string ccid = "";
        public static string uid = "";
        public static string utype = "Collector";
        public Admin_Collector()
        {
            InitializeComponent();
            fillgrid();
        }

        public void fillcollectorid()
        {
            try
            {
                string query = "select isnull(max(cid),100)+1 from Collector_Details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    cid = dr[0].ToString();
                }

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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                      fillcollectorid();
                      filluserid();
                      string query1 = "insert into Collector_Details values(" + cid + ",'" + name.Text + "','" + district.Text + "','" + mob.Text + "','" + office.Text + "','" + home.Text + "','" + address.Text + "','" + mailid.Text + "'," + uid + ")";
                      string query2 = "insert into login values(" + uid + ",'" + username.Text + "','" + password.Text + "','" + utype + "')";

                      if (con.exec1(query1) > 0)
                      {
                          if (con.exec1(query2) > 0)
                          {
                              MessageBox.Show("Collector details added......");

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
                          MessageBox.Show("Problem encountered while adding employee details.....");
                          cleartab1();
                      }
                  
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured while inserting data....");
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
        }

        public void cleartab2()
        {
            name1.Text = "";
            district1.Text = "";
            office1.Text = "";
            mailid1.Text = "";
            address1.Text = "";
           
            home1.Text = "";
            mob1.Text = "";
        }
        public void fillgrid()
        {
            try
            {
                string query = "select * from Collector_Details";
                DataSet ds = con.ret_ds(query);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             string query = "select * from Collector_Details where District='" + district1.Text + "'";
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update Collector_Details set Collectorname='"+name1.Text+"',mobileno='"+mob1.Text+"',officeno='"+office1.Text+"',homeno='"+home1.Text+"',address='"+address1.Text+"' where cid='" + ccid + "'";
                if (con.exec1(query) > 0)
                {
                    MessageBox.Show("Collector details edited......");
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
                string query = "delete Collector_Details where cid='" + ccid + "'";
                if (con.exec1(query) > 0)
                {
                    MessageBox.Show("Collector details deleted......");
                    cleartab2();
                    fillgrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cleartab2();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
