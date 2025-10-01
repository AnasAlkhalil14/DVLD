using DVLD.Applications.Tests.Vision_Test;
using DVLD.Properties;
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
    public partial class frmTestAppointments : Form
    {
        public frmTestAppointments(int LDLAppID,int TestTypeID)
        {
            InitializeComponent();
        ctrlDrivingLicenseApplicationInfo1.LoadDataInfo(LDLAppID);
        this.LDLAppID = LDLAppID;
          this.TestTypeID = TestTypeID;
        }
        int TestTypeID;
        int LDLAppID;
        bool IsRetakeTestApp=false;
        public event Action OnPass;
        public event Action OnEditPerson;
        void RefreshForm()
        {
            dgvAppointmentList.DataSource = clsTestAppointment.AllTestAppointment(LDLAppID, TestTypeID);
            lblRecords.Text = dgvAppointmentList.Rows.Count.ToString();
        }
        private void frmVisionTestAppointment_Load(object sender, EventArgs e)
        {
            RefreshForm();
            SetHeaderFormByTestType();
            ctrlDrivingLicenseApplicationInfo1.OnEditPerson += CtrlDrivingLicenseApplicationInfo1_OnEditPerson;
        }

        private void CtrlDrivingLicenseApplicationInfo1_OnEditPerson()
        {
            OnEditPerson?.Invoke();
        }

        void SetHeaderFormByTestType()
        {
          if(TestTypeID==1)
            {
                lblTestType.Text = "Vision Test Appointment";
                pbTestType.Image = Resources.Vision_512;
            }
          else if(TestTypeID==2)
            {
                lblTestType.Text = "Written Test Appointment";
                pbTestType.Image = Resources.Written_Test_512;
            }
          else
            {
                lblTestType.Text = "Street Test Appointment";
                pbTestType.Image = Resources.driving_test_512; ;

            }
 


        }


        
        private void button2_Click(object sender, EventArgs e)
        {

        }
       bool ChickIfAllowAddAppointment()
        {

            if(clsTestAppointment.ChickIfHasWaitingAppointment(LDLAppID,TestTypeID))
            {
                IsRetakeTestApp = false;

                MessageBox.Show("Person already has an active appointment for this test,you cannot add new appointment", "Nor Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            if(clsTestAppointment.ChickForTestResult(LDLAppID,TestTypeID,1))
            {
                IsRetakeTestApp = false;

                MessageBox.Show("This person already passed this test before,you can only retake failed test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }
            if(clsTestAppointment.ChickForTestResult(LDLAppID,TestTypeID,0))
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
                frmScheduleTest frm=new frmScheduleTest(LDLAppID,IsRetakeTestApp,TestTypeID);
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
            int TestAppointmentID = Convert.ToInt32(dgvAppointmentList.CurrentRow.Cells["TestAppointmentID"].Value);

            frmTakeTest frm = new frmTakeTest(TestAppointmentID);
            frm.OnPass += Frm_OnPass;
          frm.OnSave+= Frm_OnSave;
            frm.ShowDialog();
        }

        private void Frm_OnPass()
        {
            this.OnPass?.Invoke();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {




            bool IsLocked = Convert.ToBoolean(dgvAppointmentList.CurrentRow.Cells["IsLocked"].Value);
            int TestAppointmentID = Convert.ToInt32(dgvAppointmentList.CurrentRow.Cells["TestAppointmentID"].Value);
            IsRetakeTestApp = false;
            frmScheduleTest frm = new frmScheduleTest(LDLAppID,IsRetakeTestApp,TestAppointmentID,IsLocked);
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }
    }
}
