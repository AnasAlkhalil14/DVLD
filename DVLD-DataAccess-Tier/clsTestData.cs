using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsTestData
    {
        public static int GetTrialCount(int LDLAppID,int TestType)
        {
            int Count = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
select count(*) from TestAppointments TA join Tests T on Ta.TestAppointmentID = T.TestAppointmentID
where TA.LocalDrivingLicenseApplicationID=@LDLAppID and TA.TestTypeID=@TestType and Ta.IsLocked=1";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@TestType", TestType);
            try
            {
                connection.Open();
                Count = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return Count;



        }
    }
}
