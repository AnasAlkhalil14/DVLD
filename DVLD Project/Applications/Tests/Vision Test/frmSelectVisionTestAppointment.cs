using DVLD_Business_Tier;
using System;
using System.Windows.Forms;

namespace DVLD.Applications.Tests
{
    public partial class frmScheduleVisionTest : Form
    {
        int LDLAppIDD;
        bool IsRetakeTest;
        bool IsUpdate;
        int AppointmentID;
        public frmScheduleVisionTest(int LDLAppID,bool IsRetakeTest)
        {
            InitializeComponent();
            this.LDLAppIDD = LDLAppID;
            this.IsRetakeTest = IsRetakeTest;
            this.IsUpdate = false;
        }

        public frmScheduleVisionTest(int LDLAppID, bool IsRetakeTest,int AppointmentID,bool IsLock)
        {
            InitializeComponent();
            this.LDLAppIDD = LDLAppID;
            this.IsRetakeTest = IsRetakeTest;
            this.IsUpdate = true ;
            this.AppointmentID = AppointmentID;
if(IsLock)
            {
                lblLockNote.Visible = true;
                dtpDate.Enabled= false;
                btnSave.Enabled= false;
            }


        }



        public event Action OnSave;

        void LoadData()
        {
            lblLDLAppID.Text=LDLAppIDD.ToString();
            lblLicenseClass.Text=clsLocalDrivingLicenseApplications.LicenseClassString(LDLAppIDD);
            lblFullName.Text = clsLocalDrivingLicenseApplications.FullName(LDLAppIDD);
            lblTrial.Text = clsTest.TrialCount(LDLAppIDD,1).ToString();
            dtpDate.Value = DateTime.Now;
            lblFees.Text = clsTestTypes.TestFees(1).ToString();
            if(IsRetakeTest)
            {
                gbRetakeTestInfo.Enabled = true;
                lblRTestAppFees.Text=clsApplication.ApplicationFees(7).ToString();
                lblRTestAppFees.Tag = lblRTestAppFees.Text;
                lblScheduleTest.Text = "Schedule Retake Test";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblScheduleTest.Text = "Schedule Test";
                lblRTestAppFees.Text = ".....";
                lblRTestAppFees.Tag = 0;
            }
                lblTotalFees.Text = (Convert.ToDouble(lblFees.Text) + Convert.ToDouble(lblRTestAppFees.Tag)).ToString();

        }

        private void frmScheduleVisionTest_Load(object sender, EventArgs e)
        {
            dtpDate.MinDate=DateTime.Now;
            dtpDate.MaxDate=DateTime.Now.AddMonths(1);
            LoadData();

        }

        bool SaveAppointment()
        {
            if (IsUpdate)
            {
                if(clsTestAppointment.EditAppointmentDate(AppointmentID,dtpDate.Value))
                return true;
                else
                    return false;


            }
            clsTestAppointment appointment = new clsTestAppointment();
            appointment.TestTypeID = 1;
            appointment.AppointmentDate = dtpDate.Value;
            appointment.LocalDrivingLicenseApplicationID = LDLAppIDD;
            appointment.PaidFees = clsTestTypes.TestFees(1);
            appointment.IsLocked = 0;
      if(IsRetakeTest)
            {
                clsApplication application = new clsApplication();
                application.ApplicationStatus = 3;
                application.ApplicationTypeID = 7;
                application.LastStatusDate=DateTime.Now;
                application.ApplicationDate=DateTime.Now;
                application.CreatedByUserID = clsGlobal.User.UserID;
                application.ApplicantPersonID = clsLocalDrivingLicenseApplications.PersonIDByLocalDLAppID(LDLAppIDD);
                if(application.Add())
                {
                    appointment.RetakeTestApplicationID=application.ApplicationID;
                }
            }
      if(appointment.Save())
            {
                return true;
            }
      return false;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(SaveAppointment())
            {
                OnSave?.Invoke();
                MessageBox.Show("Data Saved Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Some thing wrong happen,failed save","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
