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
    public partial class Staff_CampMembers : Form
    {
        BaseConnection con = new BaseConnection();
        public static string mid = "";
        public Staff_CampMembers()
        {
            InitializeComponent();
            district.Text = Program.district;
            fillcampidcombo();
            fillmemberid();
        }

        public void fillcampidcombo()
        {
            try
            {
                combocid.Items.Clear();
                cccid.Items.Clear();
                comboBox2.Items.Clear();
                ccccid.Items.Clear();
                string query = "select CampId from ReliefCamp_Details where district='" + Program.district + "'";
                SqlDataReader dr = con.ret_dr(query);
                while (dr.Read())
                {
                    combocid.Items.Add(dr[0].ToString());
                    cccid.Items.Add(dr[0].ToString());
                    ccccid.Items.Add(dr[0].ToString());
                    comboBox2.Items.Add(dr[0].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }
        public void fillmemberid()
        {
            try
            {
                string query = "select isnull(max(mid),100)+1 from ReliefCamp_Members";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    mid = dr[0].ToString();
                    memid.Text = mid;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        public void cleartab1()
        {
            fillmemberid();
            name.Text = "";
            age.Text = "";
            address.Text = "";
            contact.Text = "";
            gender.Text = "";
        }

        public void fillgrid()
        {
            try
            { 

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
                string status="Present";
                string q = "insert into ReliefCamp_Members values(" + memid.Text + ",'" + combocid.Text + "','" + district.Text + "','" + name.Text + "'," + age.Text + ",'" + gender.Text + "','" + address.Text + "','" + contact.Text + "','" + status + "','" + Program.csid + "')";
                if (con.exec1(q) > 0)
                {
                    MessageBox.Show("Members details added......");

                    cleartab1();

                    count(combocid.Text);
                    
                    fillcampidcombo();
                }
                else
                {
                    MessageBox.Show("Problem encountered while adding Members details.....");
                    cleartab1();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void cccid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select * from ReliefCamp_Members where CampId='"+cccid.Text+"'";
            DataSet ds = con.ret_ds(query);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }

        private void ccccid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select mid,name from ReliefCamp_Members where CampId='" + ccccid.Text + "'";
            DataSet ds = con.ret_ds(query);
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
            memberid.Items.Clear();
             SqlDataReader dr = con.ret_dr(query);
             while (dr.Read())
             {
                 memberid.Items.Add(dr[0].ToString());
             }

        }

        private void memberid_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select name from ReliefCamp_Members where mid=" + memberid.Text + "";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {
                textBox2.Text = dr[0].ToString();
             
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            count(comboBox2.Text);
        }

        public void count(string cid)
        { try
        {   //total count
            string query = "select count(mid) from ReliefCamp_Members where campid='"+cid+"'";
            SqlDataReader dr = con.ret_dr(query);
            if (dr.Read())
            {

                ltotal.Text = dr[0].ToString();
                ltotal2.Text = dr[0].ToString();
            }
            //inmates count
            string s = "Present";
            string query1 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='"+s+"'";
            SqlDataReader dr1 = con.ret_dr(query1);
            if (dr1.Read())
            {

                lin.Text = dr1[0].ToString();
                lin1.Text = dr1[0].ToString();
            }
            //left count
            string s1 = "Left";
            string query2 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "'";
            SqlDataReader dr2 = con.ret_dr(query2);
            if (dr2.Read())
            {
               
                lleft.Text = dr2[0].ToString();
                lleft1.Text = dr2[0].ToString();
            }
            
            //male count
            string s2 = "Present";
            string g = "Male";
            string query3= "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s2 + "' and sex='"+g+"'";
            SqlDataReader dr3 = con.ret_dr(query3);
            if (dr3.Read())
            {

                mp.Text = dr3[0].ToString();
                
            }

           
            
            string query4 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "' and sex='" + g + "'";
            SqlDataReader dr4 = con.ret_dr(query4);
            if (dr4.Read())
            {

                ml.Text = dr4[0].ToString();

            }


            string query5 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and sex='" + g + "'";
            SqlDataReader dr5 = con.ret_dr(query5);
            if (dr5.Read())
            {

                mt.Text = dr5[0].ToString();

            }

            //female count


         
            string g1 = "Female";
            string query6 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s2 + "' and sex='" + g1 + "'";
            SqlDataReader dr6 = con.ret_dr(query6);
            if (dr6.Read())
            {

                fp.Text = dr6[0].ToString();

            }



            string query7 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "' and sex='" + g1 + "'";
            SqlDataReader dr7 = con.ret_dr(query7);
            if (dr7.Read())
            {

                fl.Text = dr7[0].ToString();

            }


            string query8 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and sex='" + g1 + "'";
            SqlDataReader dr8 = con.ret_dr(query8);
            if (dr8.Read())
            {

                ft.Text = dr8[0].ToString();

            }

            //others count...

            string g2 = "Others";
            string query9 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s2 + "' and sex='" + g2 + "'";
            SqlDataReader dr9 = con.ret_dr(query9);
            if (dr9.Read())
            {

                op.Text = dr9[0].ToString();

            }



            string query10 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "' and sex='" + g2 + "'";
            SqlDataReader dr10 = con.ret_dr(query10);
            if (dr10.Read())
            {

                ol.Text = dr10[0].ToString();

            }


            string query11 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and sex='" + g2 + "'";
            SqlDataReader dr11 = con.ret_dr(query11);
            if (dr11.Read())
            {

                ot.Text = dr11[0].ToString();

            }


            //children count...

            string  age="18" ;
            string agel = "50";
          string query12 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s2 + "' and age<'" + age + "'";
            SqlDataReader dr12 = con.ret_dr(query12);
            if (dr12.Read())
            {

                cp.Text = dr12[0].ToString();

            }
            string query13 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "' and age<'" + age + "'";
            SqlDataReader dr13 = con.ret_dr(query13);
            if (dr13.Read())
            {

                cl.Text = dr13[0].ToString();

            }

            string query18 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and age<'" + age + "'";
            SqlDataReader dr18 = con.ret_dr(query18);
            if (dr18.Read())
            {

                ct.Text = dr18[0].ToString();

            }
            string query14 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "' and (age between 18 and 50)";
            SqlDataReader dr14 = con.ret_dr(query14);
            if (dr14.Read())
            {

                al.Text = dr14[0].ToString();

            }
            string query15 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s2 + "' and (age between 18 and 50)";
            SqlDataReader dr15 = con.ret_dr(query15);
            if (dr15.Read())
            {

                ap.Text = dr15[0].ToString();

            }
            string query19 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and (age between 18 and 50)";
            SqlDataReader dr19 = con.ret_dr(query19);
            if (dr19.Read())
            {

                at.Text = dr19[0].ToString();

            }

            string query16 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s2 + "' and age<'" + agel + "'";
            SqlDataReader dr16 = con.ret_dr(query16);
            if (dr16.Read())
            {

                gp.Text = dr16[0].ToString();

            }
            string query17 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and status='" + s1 + "' and age<'" + agel + "'";
            SqlDataReader dr17 = con.ret_dr(query17);
            if (dr17.Read())
            {

                gl.Text = dr17[0].ToString();

            }

            string query20 = "select count(mid) from ReliefCamp_Members where campid='" + cid + "' and age<'" + agel + "'";
            SqlDataReader dr20 = con.ret_dr(query20);
            if (dr20.Read())
            {

                gt.Text = dr20[0].ToString();

            }

          
             }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured....");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "update ReliefCamp_Members set status='"+comboBox1.Text+"' where mid='"+memberid.Text+"' ";
            if (con.exec1(query) > 0)
            {
                MessageBox.Show("Members details updated......");
                count(ccccid.Text);
                comboBox1.Text = "";
                memberid.Text = "";
                textBox2.Text = "";



                fillcampidcombo();
               
            }
            else
            {
                MessageBox.Show("Problem encountered while editing Members details.....");
               

            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void combocid_SelectedIndexChanged(object sender, EventArgs e)
        {
            count(combocid.Text);
        }
    }
}
