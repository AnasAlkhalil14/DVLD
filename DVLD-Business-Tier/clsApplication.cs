using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Add()
        {
            ApplicationID = clsApplicationData.Add(ApplicantPersonID,ApplicationDate,ApplicationTypeID,ApplicationStatus,LastStatusDate,_PaidFees,CreatedByUserID);
            return ApplicationID!=-1;


        }




    }




}
