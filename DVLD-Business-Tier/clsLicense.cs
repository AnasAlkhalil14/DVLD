using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsLicense
    {
       public int LicenseID {  get; set; }
        public int ApplicationID {  get; set; }
        public int DriverID {  get; set; }
        private int _LicenseClass;
        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            _LicenseClass = 0;

        }
        public int LicenseClass { get { return _LicenseClass; } set { _LicenseClass = value; } }
        public DateTime IssueDate {  get; set; }
        private void _SetExpirationDate()
        {
            _ExpirationDate=IssueDate.AddYears(clsLicenseClass.DefaultValidityLength(LicenseClass));

        }
        private DateTime _ExpirationDate;
        public DateTime ExpirationDate { get { return _ExpirationDate; } }
        public string Notes { get; set; }
        public double PaidFees {  get; set; }

        public bool IsActive {  get; set; }

        public int IssueReason {  get; set; }
        public int CreatedByUserID { get; set; }
        public bool Add()
        {
            PaidFees = clsLicenseClass.ClassFees(LicenseClass);
            _SetExpirationDate(); 

            LicenseID=clsLicenseData.AddLicense(ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees,IsActive,IssueReason,CreatedByUserID);

            return LicenseID != -1;
        }


        public static DataRow LicenseInfoRow(int LicenseID)
        {
            return clsLicenseData.GetLicenseInfo(LicenseID);
        }
        public static int GetLicenseIDByAppID(int AppID)
        {
            return clsLicenseData.GetLicenseIDByAppID(AppID);
        }
        public static DataTable GetLicensesHistory(int DriverID)
        {
            return clsLicenseData.GetLicensesHistory(DriverID);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            return clsLicenseData.IsLicenseExist(LicenseID);
        }

        public static int LicenseClassIDByLicenseID(int LicenseID)
        {
            return clsLicenseData.LicenseClassIDByLicenseID(LicenseID);
        }

        public static bool ChangeLicenseIsActive(int LicenseID, bool NewIsActive)
        {
            return clsLicenseData.ChangeLicenseIsActive(LicenseID, NewIsActive);
        }
    }
}
