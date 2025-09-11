using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsTestAppointment
    {

      public  int TestAppointmentID {  get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID {  get; set; }
        public DateTime AppointmentDate {  get; set; }
      public  double PaidFees { get; set; }
     private   int CreatedByUserID {  get; set; }
   public     int IsLocked {  get; set; }
       public int RetakeTestApplicationID {  get; set; }
        private enum enMode { eAdd,eUpdate}
        enMode Mode;
        private bool _Add()
        {
            TestAppointmentID = clsTestAppointmentData.AddTestAppointment(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees,clsGlobal.User.UserID,IsLocked,RetakeTestApplicationID);
            return TestAppointmentID != -1;
        }
        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            IsLocked = 0;
            RetakeTestApplicationID = 0;
            Mode = enMode.eAdd;
        }
        private clsTestAppointment(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,DateTime AppointmentDate,double PaidFees,int CreatedByUserID,int IsLocked,int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees= PaidFees;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID= RetakeTestApplicationID;
            this.CreatedByUserID = CreatedByUserID;
  

            Mode = enMode.eUpdate;
        }



  public static bool ChickIfHasWaitingAppointment(int LDLAppID,int TestTypeID)
        {
            return clsTestAppointmentData.ChickIfHasWaitingAppointment(LDLAppID, TestTypeID);
        }
  public static bool ChickForTestResult(int LDLAppID,int TestTypeID,int TestResult)
        {
            return clsTestAppointmentData.ChickForTestResult(LDLAppID , TestTypeID, TestResult);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.eAdd:
                    {
                        if(_Add())
                        {
                            Mode = enMode.eUpdate;
                            return true;
                        }
                        break;
                    }
                case enMode.eUpdate:
                    {
                        break;
                    }
            }
            return false;
        }
        
        public static DataTable AllTestAppointment(int LDLAppID,int TestTypeID)
        {
            return clsTestAppointmentData.GetAllAppointments(LDLAppID, TestTypeID);
        }

        public static bool EditAppointmentDate(int TestAppointmentID,DateTime Date)
        {
            return clsTestAppointmentData.EditAppointmentDate(TestAppointmentID,Date);
        }


    }
}
