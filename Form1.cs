using Attendance.DataSet1TableAdapters; // REMOVE?
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
    public partial class Form1 : Form
    {
        public int loggedin { get; set; }
        public int UserID { get; set; }
        public Form1()
        {
            InitializeComponent();
            loggedin = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           


        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if(loggedin == 0)
            {
                //log in
                LoginForm NewLogin = new LoginForm();
                NewLogin.ShowDialog();

                //If user login is not valid Close Application
                //If user login is valid then open main application
                if (NewLogin.loginFlag == false)
                {
                    Close();
                }
                else
                {
                    UserID = NewLogin.UserID;
                    statLblUser.Text = UserID.ToString();
                    loggedin = 1;

                    this.classesTBLTableAdapter.Fill(this.dataSet1.ClassesTBL);
                    classesTBLBindingSource.Filter = "UserID = '" + UserID.ToString() + "'";
                }
            }
          
        }

        //Add Class Button
        private void metroButton3_Click(object sender, EventArgs e)
        {
            FormAddClass addclass = new FormAddClass();
            addclass.UserID = this.UserID;
            addclass.ShowDialog();
        }

        //Add students Button
        private void metroButton4_Click(object sender, EventArgs e)
        {
            //add students to a class
            //TODO crash occurs when user tries to add student without a class selected,should add error notification
            StudentForm students = new StudentForm();
            students.ClassName = metroComboBox1.Text;
            students.ClassID = (int)metroComboBox1.SelectedValue;

            students.ShowDialog();

        }

        //'Get Values' Button
        private void metroButtonGet_Click(object sender, EventArgs e)
        {

            //Check if records exist, load for editing or create a record for each student if none exists

            AttendanceTBLTableAdapter ada = new AttendanceTBLTableAdapter();

            DataTable dt = ada.GetDataBy((int)metroComboBox1.SelectedValue,dateTimePicker1.Text);

            if(dt.Rows.Count > 0)
            {
                //records Exist
                DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;
            }
            else
            {
                //create record for each
                //get class students list
                StudentsTBLTableAdapter students_Adapter = new StudentsTBLTableAdapter();

                DataTable dt_Students = students_Adapter.GetDataByClassID((int)metroComboBox1.SelectedValue);

                foreach (DataRow row in dt_Students.Rows)
                {
                    //insert new record
                        ada.InsertQuery((int)row[0],
                        (int)metroComboBox1.SelectedValue,
                        dateTimePicker1.Text, "",
                        row[1].ToString(), 
                        metroComboBox1.Text);
                }

                DataTable dt_new= ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;

            }
            /*TODO Delete, Probably not needed:
             this.attendanceTBLTableAdapter.Fill(this.dataSet1.AttendanceTBL); */
            
        }

        //TODO: Sould add text box w/confirmation for saved/wiped records, not clear if it worked for user
        //Save Record button
        private void metroButton1_Click(object sender, EventArgs e)
        {
            AttendanceTBLTableAdapter ada = new AttendanceTBLTableAdapter();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    ada.UpdateQuery(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString(), (int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
                }
                

            }

            DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
            dataGridView1.DataSource = dt_new;
        }

        //Clear Records Button
        private void metroButton2_Click(object sender, EventArgs e)
        {
            AttendanceTBLTableAdapter ada = new AttendanceTBLTableAdapter();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    ada.UpdateQuery(" ", row.Cells[0].Value.ToString(), (int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
                }


            }

            DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
            dataGridView1.DataSource = dt_new;
        }

        //Student records Function, Crashes Program right now, needs to be fixed
        private void metroButton5_Click(object sender, EventArgs e)
        {
            StudentsTBLTableAdapter students_Adapter = new StudentsTBLTableAdapter();

            DataTable dt_Students = students_Adapter.GetDataByClassID((int)metroComboBox2.SelectedValue);

            AttendanceTBLTableAdapter ada = new AttendanceTBLTableAdapter();
            
            /*
            int P = 0;
            int A = 0;
            int L = 0;
            int E = 0;
            */
            //loop databse and get values for PALE

            //foreach (DataRow row in dt_Students.Rows)
           // {


                //present
                //P = (int)ada.GetDataByReport(dateTimePicker2.Value.Month, row[1].ToString(), "present").Rows[0][6];

                /*/absent
                A = (int)ada.GetDataByReport(dateTimePicker2.Value.Month,
                   row[1].ToString(),
                   "Absent").Rows[0][6];
                //late
                L = (int)ada.GetDataByReport(dateTimePicker2.Value.Month,
                   row[1].ToString(),
                   "Late").Rows[0][6];
                //excused
                E = (int)ada.GetDataByReport(dateTimePicker2.Value.Month,
                   row[1].ToString(),
                   "Excused").Rows[0][6];
                   */
              //  ListViewItem litem = new ListViewItem();

               // litem.Text = row[1].ToString();
                //litem.SubItems.Add(P.ToString());
                //litem.SubItems.Add(A.ToString());
                //litem.SubItems.Add(L.ToString());
                //litem.SubItems.Add(E.ToString());
                //listView1.Items.Add(litem);


           // }
        }
    }
}
