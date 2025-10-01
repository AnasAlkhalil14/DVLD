using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    //we made it at late time ,there are functions should be here but now in clsApplication

    public class clsLicenseClass
    {


        public static int ClassIDByLDLAppID(int LDLAppID)
        {
            return clsLicenseClassData.ClassIDByLDLAppID(LDLAppID);
        }
        public static double ClassFees(int LicenseClassID)
        {
            return clsLicenseClassData.GetClassFees(LicenseClassID);
        }
        public static int DefaultValidityLength(int LicenseClassID)
        {
            return clsLicenseClassData.GetValidityLength(LicenseClassID);
        }


    }
}
