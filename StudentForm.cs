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
    public partial class StudentForm : Form
    {

        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public StudentForm()
        {
            InitializeComponent();
        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.StudentsTBL' table. You can move, or remove it, as needed.
            this.studentsTBLTableAdapter.Fill(this.dataSet1.StudentsTBL);
            labelClassID.Text = ClassID.ToString();
            labelClassName.Text = ClassName.ToString();

        }

        //add entered student information to database
        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.studentsTBLBindingSource.EndEdit();
            this.studentsTBLTableAdapter.Update(dataSet1.StudentsTBL);
        }
    }
}
