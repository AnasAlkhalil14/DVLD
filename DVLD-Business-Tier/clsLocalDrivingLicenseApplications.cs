using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsLocalDrivingLicenseApplications
    {
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public static string LicenseClassString(int LDLAppID) {  return clsLocalDrivingLicenseApplicationsData.GetLicenseClassName(LDLAppID); } 
        public clsLocalDrivingLicenseApplications()
        {
            ApplicationID = -1;
            LicenseClassID = -1;
            LocalDrivingLicenseApplicationID = -1;

        }

        public bool Add()
        {
            LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.Add(ApplicationID, LicenseClassID);
            return LocalDrivingLicenseApplicationID != -1;
        }

        public static DataTable GetAllLicenceClasses()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLicenceClasses();
        }

        public static bool CheckIfPersonCanApplyNewLocalLicenceApplication(int PersonID,int LicenceClassID)
        {
            return clsLocalDrivingLicenseApplicationsData.CheckIfCanMakeApplicationForNewLocalLicence(PersonID,LicenceClassID);


        }

        public static int PassedTestCount(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetPassedTestCount(LDLAppID); 

        }
        public static int AppID(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetApplicationID(LDLAppID);

        }
        public static DataTable GetAllLocalLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalLicenseApplications();
        }
        public static DataTable FilterByLDLAppID(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalLicenseApplicationsByByID(LDLAppID);
        }
        public static DataTable FilterByFullName(string FullName)
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalLicenseApplicationsByFullName(FullName);
        }
        public static DataTable FilterByStatus(string Status)
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalLicenseApplicationsByStatus(Status);
        }
        public static DataTable FilterByNationalNo(string NationalNo)
        {
            return clsLocalDrivingLicenseApplicationsData.GetAllLocalLicenseApplicationsByNationalNo(NationalNo);
        }
        public static bool CanceleApplication(int LDLApplicationID)
        {
            return clsLocalDrivingLicenseApplicationsData.CanceleApplication(LDLApplicationID);

        }
        public static string FullName(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetFullName(LDLAppID);
        }

        public static int PersonIDByLocalDLAppID(int LDLAppID)
        {
            return clsLocalDrivingLicenseApplicationsData.GetPersonIDByLocalDLAppID(LDLAppID) ;
        }



    }


}
