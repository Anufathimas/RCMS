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
    public partial class Admin_Items : Form
    {
        BaseConnection con = new BaseConnection();
        public static string cid = "";
        public Admin_Items()
        {
            InitializeComponent();
            fillcid();
            fillgrid();

        }
        public void fillcid()
        {
            try
            {
                string query = "select isnull(max(catid),100)+1 from Category_Details";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    cid = dr[0].ToString();
                    ID.Text = dr[0].ToString();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        public void cleartab1()
        {
            ID.Text = "";
            name.Text = "";
            items.Text = "";
            Units.Text = "";

        }

        public void fillgrid()
        {
            try
            {
                string query = "select * from Category_Details order by catid asc";
                DataSet ds = con.ret_ds(query);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

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
               string query = "insert into Category_Details values(" + cid + ",'"+name.Text+"','"+items.Text+"','"+Units.Text+"')";
            if (con.exec1(query) > 0)
            {
                MessageBox.Show("Category details added......");

                cleartab1();
                fillgrid();
                fillcid();
            }
            else
            {
                MessageBox.Show("Problem encountered while adding Category details.....");
                cleartab1();
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
