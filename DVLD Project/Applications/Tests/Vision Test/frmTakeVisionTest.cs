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

namespace DVLD.Applications.Tests.Vision_Test
{
    public partial class frmTakeTest : Form
    {
        clsTest Test;
        clsTestAppointment Appointment;
        public event Action OnSave;
        public event Action OnPass;
        public frmTakeTest(int AppointmentID)
        {
            InitializeComponent();
            Appointment = clsTestAppointment.Find(AppointmentID);
        }
        void FillFormData()
        {
            lblDate.Text=Appointment.AppointmentDate.ToShortDateString();
            lblFees.Text = Appointment.PaidFees.ToString();
            lblFullName.Text=clsLocalDrivingLicenseApplications.FullName(Appointment.LocalDrivingLicenseApplicationID);
            lblLDLAppID.Text = Appointment.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClass.Text=clsLocalDrivingLicenseApplications.LicenseClassString(Appointment.LocalDrivingLicenseApplicationID);
            lblTrial.Text = clsTest.TrialCount(Appointment.LocalDrivingLicenseApplicationID,Appointment.TestTypeID).ToString();
            
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Test != null)
            {
                if(clsTest.UpdateNotes(Test.TestID,txtNotes.Text))
                {
                    MessageBox.Show("Notes Updated Successfully","Result",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Some ting wrong happen,try again later", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);


                return;
            }
            Test = new clsTest();

            if(MessageBox.Show("Are you sure you want to save this result,after save it you can not change it","Confirm Adding",MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk)== DialogResult.Yes)
            {
                Test.TestAppointmentID = Appointment.TestAppointmentID;
                Test.Notes = txtNotes.Text;
                Test.CreatedByUserID=clsGlobal.User.UserID;
                if (rbFail.Checked) Test.TestResult = 0;
                else Test.TestResult = 1;
                if(Test.Add())
                {
                    Appointment.MakeAppointmentLock();
                    if(rbPass.Checked)
                    { OnPass?.Invoke(); }
                    OnSave?.Invoke();
                    MessageBox.Show("Test result saved successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbFail.Enabled = false;
                    rbPass.Enabled = false;
                    lblCantNotChangeTheResult.Visible = true;
                    lblTestID.Text = Test.TestID.ToString();

                }
                else
                    MessageBox.Show("Some ting wrong happen,try again later","Result",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void ChickForTakenTest()
        {
            Test=clsTest.FindTestByAppointmentID(Appointment.TestAppointmentID);
            if (Test == null) return;
            lblTestID.Text = Test.TestID.ToString();
            if (Test.TestResult == 1)
                rbPass.Checked = true;
            else
                rbFail.Checked = true;

            txtNotes.Text = Test.Notes;
            rbFail.Enabled = false;rbPass.Enabled = false;
            lblCantNotChangeTheResult.Visible = true;

        }

        void SetHeaderFormByTestType()
        {
            if (Appointment.TestTypeID == 1)
            {

                pbTestType.Image = Resources.Vision_512;
            }
            else if (Appointment.TestTypeID == 2)
            {
                pbTestType.Image = Resources.Written_Test_512;
            }
            else
            {
                pbTestType.Image = Resources.driving_test_512; ;

            }



        }

        private void frmTakeVisionTest_Load(object sender, EventArgs e)
        {
            SetHeaderFormByTestType();
            FillFormData();
            ChickForTakenTest();
        }

      
    }
}
