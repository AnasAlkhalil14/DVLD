using System;
using System.Collections.Generic;
using System.Data;
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

        public static int AddTest(int TestAppointmentID, int TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;
        SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert into Tests values(@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                               Select Scope_Identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if(Notes!="")
            command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out TestID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return TestID;
        }

        public static bool FindTestByAppointmentID(int TestAppointmentID,ref int TestID,ref int TestResult,ref string Notes,ref int CreatedByUserID)
        {
           bool IsFound=false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT [TestID]
      ,[TestResult]
      ,[Notes]
      ,[CreatedByUserID]
  FROM [DVLD].[dbo].[Tests]
 where TestAppointmentID=@TestAppointmentID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestID = Convert.ToInt32(reader["TestID"]);
                    TestResult = Convert.ToInt32(reader["TestResult"]);
                    Notes = Convert.ToString(reader["Notes"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);

                    IsFound = true;
                }
                reader.Close();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
            return IsFound;

        }

        public static bool UpdateNotes(int TestID,string Notes)
        {
            int RowAffected = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Update Tests set Notes=@Notes where TestID=@TestID";
            SqlCommand command = new SqlCommand(query, connection);
            if (Notes != "")
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);

            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch(Exception ex) { }
            finally { connection.Close(); }
            return RowAffected > 0;
        }
    }
}
