using DVLD.Properties;
using DVLD_Business_Tier;
using System;
using System.Windows.Forms;

namespace DVLD.Applications.Tests
{
    public partial class frmScheduleTest : Form
    {
        int LDLAppIDD;
        bool IsRetakeTest;
        bool IsUpdate;
        clsTestAppointment Appointment;
        int TestTypeID;
        public frmScheduleTest(int LDLAppID,bool IsRetakeTest,int TestTypeID)
        {
            InitializeComponent();
            this.LDLAppIDD = LDLAppID;
            this.IsRetakeTest = IsRetakeTest;
            this.IsUpdate = false;
            this.TestTypeID = TestTypeID;
            if (IsRetakeTest)
            {
                dtpDate.MinDate=clsTestAppointment.GetLastAppointmentDate(LDLAppID,1).AddDays(1);
                dtpDate.MaxDate=dtpDate.MinDate.AddMonths(1);
                dtpDate.Value = dtpDate.MinDate;
            }
            else
            {
                dtpDate.MinDate =DateTime.Now;
                dtpDate.MaxDate = dtpDate.MinDate.AddMonths(1);
                dtpDate.Value = dtpDate.MinDate;

            }

        }

        public frmScheduleTest(int LDLAppID, bool IsRetakeTest,int AppointmentID,bool IsLock)
        {
            InitializeComponent();
            Appointment=clsTestAppointment.Find(AppointmentID);
            TestTypeID=Appointment.TestTypeID;
            this.LDLAppIDD = LDLAppID;
            if(Appointment.RetakeTestApplicationID==-1)
            this.IsRetakeTest =false;
            else
                this.IsRetakeTest=true;

                this.IsUpdate = true;
       
if(IsLock)
            {
              
                lblLockNote.Visible = true;
                dtpDate.Enabled= false;
                btnSave.Enabled= false;
            }
else if (IsRetakeTest)
            {
               ;
                dtpDate.MinDate = clsTestAppointment.GetLastAppointmentDate(LDLAppID,Appointment.TestTypeID).AddDays(1);
                dtpDate.MaxDate = dtpDate.MinDate.AddMonths(1);

            }
else
            {
                dtpDate.MinDate = DateTime.Now;
                dtpDate.MaxDate = dtpDate.MinDate.AddMonths(1);
                if (Appointment.AppointmentDate < DateTime.Now || Appointment.AppointmentDate > dtpDate.MaxDate)
                {
                    MessageBox.Show("Your old date is no longer valid,choose another one", "Wrong old Date", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ;dtpDate.Value=DateTime.Now;
                    return;
                }


            }
            dtpDate.Value = Appointment.AppointmentDate;

            
        }



        public event Action OnSave;

        void LoadData()
        {
            lblLDLAppID.Text=LDLAppIDD.ToString();
            lblLicenseClass.Text=clsLocalDrivingLicenseApplications.LicenseClassString(LDLAppIDD);
            lblFullName.Text = clsLocalDrivingLicenseApplications.FullName(LDLAppIDD);
            lblTrial.Text = clsTest.TrialCount(LDLAppIDD,TestTypeID).ToString();
           
            lblFees.Text = clsTestTypes.TestFees(TestTypeID).ToString();
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
                lblScheduleTest.Text = "         Schedule Test";
                lblRTestAppFees.Text = ".....";
                lblRTestAppFees.Tag = 0;
            }
            lblTotalFees.Text = (Convert.ToDouble(lblFees.Text) + Convert.ToDouble(lblRTestAppFees.Tag)).ToString();
            

        }
        void SetHeaderFormByTestType()
        {
            if (TestTypeID == 1)
            {
                
                pbTestType.Image = Resources.Vision_512;
            }
            else if (TestTypeID == 2)
            {
                pbTestType.Image = Resources.Written_Test_512;
            }
            else
            {
                pbTestType.Image = Resources.driving_test_512; ;

            }



        }
        private void frmScheduleVisionTest_Load(object sender, EventArgs e)
        {
          
           
            LoadData();
            SetHeaderFormByTestType();

        }

        bool SaveAppointment()
        {
            if (IsUpdate)
            {
                if(clsTestAppointment.EditAppointmentDate(Appointment.TestAppointmentID, dtpDate.Value))
                return true;
                else
                    return false;


            }
            clsTestAppointment appointment = new clsTestAppointment();
            appointment.TestTypeID = TestTypeID;
            appointment.AppointmentDate = dtpDate.Value;
            appointment.LocalDrivingLicenseApplicationID = LDLAppIDD;
            appointment.PaidFees = clsTestTypes.TestFees(TestTypeID);
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

        private void gbVisionTest_Enter(object sender, EventArgs e)
        {

        }
    }
}
