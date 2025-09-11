using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsTest
    {
        public static int TrialCount(int LDLAppID,int TestTypeID)
        {
            return clsTestData.GetTrialCount(LDLAppID, TestTypeID);
        }


    }
}
