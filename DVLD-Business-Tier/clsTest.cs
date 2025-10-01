using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsTest
    {

        public int TestID {  get; set; }
        public int TestAppointmentID {  get; set; }
        public int TestResult {  get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set;}

        public clsTest()
        {
            TestID = -1;TestAppointmentID = -1;TestResult = -1;Notes = "";CreatedByUserID = -1;
        }

        private clsTest(int testID, int testAppointmentID, int testResult, string notes, int createdByUserID)
        {
            TestID = testID;
            TestAppointmentID = testAppointmentID;
            TestResult = testResult;
            Notes = notes;
            CreatedByUserID = createdByUserID;
        }

        public bool Add()
        {
            TestID=clsTestData.AddTest(TestAppointmentID,TestResult,Notes,CreatedByUserID);
            return TestID != -1;
        }

        public static clsTest FindTestByAppointmentID(int TestAppointmentID)
        {
            int TestID = 0, TestResult = 0, CreatedByUserID = 0;

            string Notes = "";
           if (clsTestData.FindTestByAppointmentID(TestAppointmentID,ref TestID,ref TestResult,ref Notes,ref CreatedByUserID))
            {

                    return new clsTest(TestID, TestAppointmentID, TestResult,Notes,CreatedByUserID);
            }
            return null;
        }
        public static int TrialCount(int LDLAppID,int TestTypeID)
        {
            return clsTestData.GetTrialCount(LDLAppID, TestTypeID);
        }

        public static bool UpdateNotes(int TestID,string Notes)
        {
            return clsTestData.UpdateNotes(TestID,Notes);
        }


    }
}
