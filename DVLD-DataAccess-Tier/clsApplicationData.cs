using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsApplicationData
    {
        public static int Add( int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, int ApplicationStatus, DateTime LastStatusDate, double PaidFees, int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert Into Applications Values (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatus,@LastStatusDate,@PaidFees,@CreatedByUserID);
                          Select Scope_Identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out ApplicationID))
                {

                }


            }
            catch (Exception ex)
            {


            }

            finally { connection.Close(); }

            return ApplicationID;
        }
        public static double GetPaidFeesForApplicationType(int ApplicationTypeID)
        {
            double PaidFees = 0;
            SqlConnection connection= new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT ApplicationFees
FROM     ApplicationTypes Where ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (double.TryParse(result.ToString(), out PaidFees))
                {

                }

            }
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }


            return PaidFees;
        }



    }



}

