using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Tests
{
    public partial class frmVisionTestAppointment : Form
    {
        public frmVisionTestAppointment(int LDLAppID)
        {
            InitializeComponent();
        ctrlDrivingLicenseApplicationInfo1.LoadDataInfo(LDLAppID);
        this.LDLAppID = LDLAppID;
        }
        int LDLAppID;
        bool IsRetakeTestApp=false;

        void RefreshForm()
        {
            dgvAppointmentList.DataSource = clsTestAppointment.AllTestAppointment(LDLAppID, 1);
            lblRecords.Text = dgvAppointmentList.Rows.Count.ToString();
        }
        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {

        }
       bool ChickIfAllowAddAppointment()
        {

            if(clsTestAppointment.ChickIfHasWaitingAppointment(LDLAppID,1))
            {
                IsRetakeTestApp = false;

                MessageBox.Show("Person already has an active appointment for this test,you cannot add new appointment", "Nor Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            if(clsTestAppointment.ChickForTestResult(LDLAppID,1,1))
            {
                IsRetakeTestApp = false;

                MessageBox.Show("This person already passed this test before,you can only retake failed test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            if(clsTestAppointment.ChickForTestResult(LDLAppID,1,0))
            {
                IsRetakeTestApp = true;
                return true;
            }

            IsRetakeTestApp = false;
            return true;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(ChickIfAllowAddAppointment())
            {
                frmScheduleVisionTest frm=new frmScheduleVisionTest(LDLAppID,IsRetakeTestApp);
                frm.OnSave += Frm_OnSave;
                frm.ShowDialog();


            }
        }

        private void Frm_OnSave()
        {
           RefreshForm();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool IsLocked = Convert.ToBoolean(dgvAppointmentList.CurrentRow.Cells["IsLocked"].Value);
            int TestAppointmentID = Convert.ToInt32(dgvAppointmentList.CurrentRow.Cells["TestAppointmentID"].Value);
            frmScheduleVisionTest frm = new frmScheduleVisionTest(LDLAppID,IsRetakeTestApp,TestAppointmentID,IsLocked);
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }
    }
}
