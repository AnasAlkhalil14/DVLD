using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Business_Tier
{
    

    public class clsApplication
    {
        private double _PaidFees;
        private int _ApplicationTypeID;
        public int ApplicationID {  get; set; }
        public int ApplicantPersonID {  get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { 
            get { return _ApplicationTypeID; } 
            set {
                _ApplicationTypeID = value;
                _PaidFees = clsApplicationData.GetPaidFeesForApplicationType(_ApplicationTypeID);
                return;
               }
        }
        public string ApplicationTypeString { get { return clsApplicationData.GetApplicationType(_ApplicationTypeID); } }

        public int ApplicationStatus {  get; set; } 
        public DateTime LastStatusDate {  get; set; }
        public double PaidFees { get { return _PaidFees; } } 
         
        public int CreatedByUserID {  get; set; }


        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            _ApplicationTypeID = -1;
            ApplicationStatus = -1;
            LastStatusDate = DateTime.Now;
            _PaidFees = 0;
            CreatedByUserID = -1;

        }
        public clsApplication(int ApplicationID,int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate,
            double PaidFees)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this._PaidFees=PaidFees;

        }
        public bool Add()
        {
            ApplicationID = clsApplicationData.Add(ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,_PaidFees,CreatedByUserID);
            return ApplicationID!=-1;


        }

        public static clsApplication Find(int AppID)
        {
            int ApplicantPersonID = -1; DateTime ApplicationDate = DateTime.Now;
            int ApplicationTypeID = -1; int ApplicationStatus = -1;
            DateTime LastStatusDate = DateTime.Now; double PaidFees = -1;
            if(clsApplicationData.GetApplicationInfo(AppID,ref ApplicantPersonID,ref ApplicationDate,ref ApplicationTypeID,ref ApplicationStatus,ref LastStatusDate,ref PaidFees) )
            {
                return new clsApplication(AppID,ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,PaidFees);
            }
            return null;

        }

        public static double ApplicationFees(int ApplicatinTypeID)
        {
            return clsApplicationData.GetPaidFeesForApplicationType(ApplicatinTypeID);
        }


    }




}
