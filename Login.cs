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
    public partial class Login : Form
    {
        BaseConnection con = new BaseConnection();
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username.Text == "admin" && password.Text == "pwd")
            {
                Admin_Home obj = new Admin_Home();
                ActiveForm.Hide();
                obj.Show();
            }
            else
            {
                string query = "select * from login where username='" + username.Text + "' and password='" + password.Text + "'";
                SqlDataReader dr = con.ret_dr(query);
                if (dr.Read())
                {
                    Program.uid = dr[0].ToString();
                    Program.utype = dr[3].ToString();
                    if (Program.utype == "Collector")
                    {
                        string query1 = "select district from Collector_Details where userid='" + Program.uid + "'";
                        SqlDataReader dr1 = con.ret_dr(query1);
                        if (dr1.Read())
                        {
                            Program.district = dr1[0].ToString();
                            Collector_Home obj = new Collector_Home();
                            ActiveForm.Hide();
                            obj.Show();
                        }

                    }
                    else if (Program.utype == "CollectorateStaff")
                    {
                        string query1 = "select district from CollectorateStaff_Details where userid='" + Program.uid + "'";
                        SqlDataReader dr1 = con.ret_dr(query1);
                        if (dr1.Read())
                        {
                            Program.district = dr1[0].ToString();
                            Staff_Home obj = new Staff_Home();
                            ActiveForm.Hide();
                            obj.Show();
                        }
                    }
                }
            }
        }
    }
}
