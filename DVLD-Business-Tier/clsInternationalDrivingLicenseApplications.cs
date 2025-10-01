using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsInternationalDrivingLicenseApplications
    {
        public static int InternationalActiveLicenseID(int DriverID)
        {
            return clsInternationalDrivingLicenseApplicationsData.InternationalActiveLicenseID(DriverID);
        }
        public static DataTable GetAllInternationalLicenseApplications()
        {
            return clsInternationalDrivingLicenseApplicationsData.GetAllInternationalLicenseApplications();
        }
        public static DataTable GetByInternationalLicenseID(int IntLicenseID)
        {
            return clsInternationalDrivingLicenseApplicationsData.GetByInternationalLicenseID(IntLicenseID);
        }
        public static DataTable GetByLocalLicenseID(int LocalLicenseID)
        {
            return clsInternationalDrivingLicenseApplicationsData.GetByLocalLicenseID(LocalLicenseID);
        }
        public static DataTable GetByDriverID(int DriverID)
        {
            return clsInternationalDrivingLicenseApplicationsData.GetByDriverID(DriverID);
        }

        public static DataTable GetByApplicationID(int ApplicationID)
        {
            return clsInternationalDrivingLicenseApplicationsData.GetByApplicationID(ApplicationID);
        }
        public static DataTable GetByIsActive(bool IsActive)
        {
            return clsInternationalDrivingLicenseApplicationsData.GetByIsActive(IsActive);
        }

        public static DataRow InternationalLicenseInfo(int IntLicenseID)
        {
            return clsInternationalDrivingLicenseApplicationsData.InternationalLicenseInfo(IntLicenseID);
        }
        public static DataTable GetDriverLicensesHistory(int DriverID)
        {
            return clsInternationalDrivingLicenseApplicationsData.GetDriverLicensesHistory(DriverID);
        }

        public static int Add(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            return clsInternationalDrivingLicenseApplicationsData.AddIntLicense(ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }
    }
}
