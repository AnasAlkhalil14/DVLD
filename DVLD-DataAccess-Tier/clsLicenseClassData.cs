using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsLicenseClassData
    {
        public static int GetValidityLength(int LicenseClassID)
        {
            int Length = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select DefaultValidityLength from LicenseClasses where LicenseClassID=@LicenseClassID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out Length);

            }
           catch(Exception ex) { }
            finally { connection.Close(); }
            return Length;

        }
        public static double GetClassFees(int LicenseClassID)
        {
            double ClassFees = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select ClassFees from LicenseClasses where LicenseClassID=@LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                double.TryParse(command.ExecuteScalar().ToString(), out ClassFees);

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ClassFees;

        }

        public static int ClassIDByLDLAppID(int LDLAppID)
        {
            int ClassID = -1;    
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
  select LDL.LicenseClassID from LocalDrivingLicenseApplications LDL where LDL.LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID",LDLAppID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out ClassID);
            }
            catch { } finally { connection.Close(); }
            return ClassID;
        }



    }
}
