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
    public partial class Collector_Camp : Form
    {
        BaseConnection con = new BaseConnection();
        public static string cid = "";
        public Collector_Camp()
        {
            InitializeComponent();
            fillcampid();
            fillinstaff();
            district.Text = Program.district;
            fillgrid();
            fillcampidcombo();
        }

        

        public void fillinstaff()
        {
            try
            {
                string query = "select csid,name from CollectorateStaff_Details where District='"+Program.district+"'";
                DataSet ds = con.ret_ds(query);
                dgstaff.DataSource = ds.Tables[0].DefaultView;
                incharge.Items.Clear();
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {incharge.Items.Add(dr[0].ToString());
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
                combocid.Items.Clear();
                string query = "select CampId from ReliefCamp_Details where district='"+Program.district+"'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {combocid.Items.Add(dr[0].ToString());
                   
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillcampid()
        {
            try
            {
                string query = "select isnull(max(CampId),100)+1 from ReliefCamp_Details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    cid = dr[0].ToString();
                    id.Text = dr[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void incharge_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select Mobileno from CollectorateStaff_Details where csid="+incharge.Text+"";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    contact.Text = dr[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        public void cleartab1()
        {
            fillcampid();
            taluk.Text = "";
            name.Text = "";
            address.Text = "";
            incharge.Text = "";
            contact.Text = "";
        }
        public void fillgrid()
        { 
             try
             {
                 string status = "Open";
                string query = "select * from ReliefCamp_Details where district='"+Program.district+"' and status='"+status+"'";
                DataSet ds = con.ret_ds(query);
                dgOpen.DataSource = ds.Tables[0].DefaultView;



                string status1 = "Closed";
                string query1 = "select * from ReliefCamp_Details where district='" + Program.district + "' and status='" + status1 + "'";
                DataSet ds1 = con.ret_ds(query1);

                dgClosed.DataSource = ds1.Tables[0].DefaultView;


               
                string query2 = "select * from ReliefCamp_Details where district='" + Program.district + "'";
                DataSet ds2 = con.ret_ds(query2);

                dataGridView1.DataSource = ds2.Tables[0].DefaultView;
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
                string status = "Open";
                string query = "insert into ReliefCamp_Details values(" + cid + ",'" + district.Text + "','" + taluk.Text + "','" + name.Text + "','" + address.Text + "','" + incharge.Text + "','" +contact.Text+"','"+status+"')";
                if (con.exec1(query) > 0)
                {
                    MessageBox.Show("Relief Camp details added......");

                    cleartab1();
                    fillgrid();
                    fillcampid();
                    fillcampidcombo();
                }
                else
                {
                    MessageBox.Show("Problem encountered while adding Relief Camp.....");
                    cleartab1();
                    fillgrid();
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
                string query = "update ReliefCamp_Details set status='" + cbstatus.Text + "' where Campid='" + combocid.Text + "'";
                if (con.exec1(query) > 0)
                {
                    MessageBox.Show("Relief Camp details Updated......");
                    fillgrid();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cleartab1();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
