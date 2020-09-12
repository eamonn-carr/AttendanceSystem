using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attendance
{
    public partial class LoginForm : Form
    {
        public bool loginFlag { get; set; }
        public int UserID { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            loginFlag = true;
        }


        //Not needed? Can GEt rid of after testing
        /*
        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }
        */

        //Login Button Dunction
        private void metroButton1_Click(object sender, EventArgs e)
        {
            //LOGIN
            DataSet1TableAdapters.UsersTableAdapter userAdap = new DataSet1TableAdapters.UsersTableAdapter();

            DataTable dt = userAdap.GetDataByUsersAndPass(metroTextBoxUserName.Text, metroTextBoxPassword.Text);

            if(dt.Rows.Count > 0)
            {
                //if Valid log in
                MessageBox.Show("Login Successful");
                UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                loginFlag = true;

            }
            else
            {
                // if invalid log in
                MessageBox.Show("ACCESS DENIED");
                loginFlag = false;

            }

            Close();
        }
    }
}
