using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsTestAppointmentData
    {
        public static bool ChickIfHasWaitingAppointment(int LDLAppID, int TestType)
        {
            bool isLocked = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Select 1 from TestAppointments TA where TA.LocalDrivingLicenseApplicationID=@LDLAppID and TA.IsLocked=0 and TA.TestTypeID=@TestType";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@TestType", TestType);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    isLocked = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return isLocked;

        }

        public static bool ChickForTestResult(int LDLAppID, int TestType, int TestResult)
        {
            bool Chick = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select top 1 1 from TestAppointments TA join Tests T on TA.TestAppointmentID=T.TestAppointmentID
                           Where TA.LocalDrivingLicenseApplicationID=@LDLAppID and TA.TestTypeID=@TestType and T.TestResult=@TestResult";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@TestType", TestType);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Chick = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return Chick;

        }

        public static int AddTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, double PaidFees, int CreatedByUserID, int IsLocked, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert into TestAppointments values
(@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked,@RetakeTestApplicationID)
 SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            if (RetakeTestApplicationID > 0)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);


            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (int.TryParse(result.ToString(), out TestAppointmentID))
                {
                }

            }
            catch (Exception ex) { }
            finally
            {
                connection.Close();
            }

            return TestAppointmentID;
        }

        public static DataTable GetAllAppointments(int LDLAppID, int TestType)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
Select TA.TestAppointmentID,TA.AppointmentDate,Ta.PaidFees,TA.IsLocked
from TestAppointments TA
where Ta.LocalDrivingLicenseApplicationID=@LDLAppID and TA.TestTypeID=@TestType;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            command.Parameters.AddWithValue("@TestType", TestType);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return dt;

        }

        public static bool EditAppointmentDate(int TestAppointmentID, DateTime Date)
        {
            int RowAffected = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Update TestAppointments set AppointmentDate=@Date where TestAppointmentID=@TestAppointmentID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@Date", Date);
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
