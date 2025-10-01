using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsDetainedLicense
    {
        public int DetainID {  get; set; }
        public int LicenseID {  get; set; }
        public DateTime DetainDate { get; set; }
        public double FineFees { get; set; }
        public int CreatedByUserID {  get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate {  get; set; }
        public int ReleasedByUserID { get; set; }
        public int ReleaseApplicationID { get; set; }
        public clsDetainedLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.MinValue;
            FineFees = -1;
            CreatedByUserID = -1; 
            IsReleased = false;
            ReleaseDate = DateTime.MinValue;
            ReleasedByUserID = -1;
            ReleaseApplicationID = -1;
        }

        clsDetainedLicense(int detainID, int licenseID, DateTime detainDate, double fineFees, int createdByUserID)
        {
            DetainID = detainID;
            LicenseID = licenseID;
            DetainDate = detainDate;
            FineFees = fineFees;
            CreatedByUserID = createdByUserID;
            IsReleased = false;
        }

        public  bool DetainLicense()
        { DetainID= clsDetainedLicenseData.DetainLicense(LicenseID, DetainDate, FineFees, CreatedByUserID);

            return DetainID != -1;   }

        public static clsDetainedLicense FindDetainLicense(int licenseID)
        {
            int detainID = -1,CreatedByUserID=-1;
            double finefees = 0;
            DateTime DetainDate=DateTime.MinValue;

            if(clsDetainedLicenseData.FindDetainLicense(licenseID,ref  detainID,ref DetainDate,ref finefees,ref CreatedByUserID))
            {
                return new clsDetainedLicense(detainID, licenseID, DetainDate, finefees, CreatedByUserID);
            }
            return null; 


        }

        public bool ReleaseDetainLicense()
        {
            return clsDetainedLicenseData.ReleaseLicense(DetainID, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
        }

        public static DataTable AllDetainedLicenses()
        {
            return clsDetainedLicenseData.DetainedLicensesList();
        }
        public static DataTable FilterByDetainID(int DetainID)
        {
     return       clsDetainedLicenseData.FilterByDetainID(DetainID);
        }
        public static DataTable FilterByNationalNO(string NationalNo)
        {
            return clsDetainedLicenseData.FilterByNationalNO(NationalNo);
        }
        public static DataTable FilterByFullName(string FullName)
        {
            return clsDetainedLicenseData.FilterByFullName(FullName);
        }
        public static DataTable FilterByIsReleased(bool IsReleased)
        {
            return clsDetainedLicenseData.FilterByIsReleased(IsReleased);
        }

        public static DataTable FilterByReleaseAppID(int ReleaseAppID)
        {
            return clsDetainedLicenseData.FilterByReleaseAppID(ReleaseAppID);
        }

    }
}
