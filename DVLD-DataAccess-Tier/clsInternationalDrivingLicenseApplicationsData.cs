using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsInternationalDrivingLicenseApplicationsData
    {
        public static DataTable GetAllInternationalLicenseApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
      ,[DriverID]
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses]
";
            SqlCommand command = new SqlCommand(query, connection);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;


        }
        public static DataTable GetByInternationalLicenseID(int IntLicenseID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
      ,[DriverID]
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses] where InternationalLicenseID=@IntLicenseID
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IntLicenseID", IntLicenseID);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;


        }
        public static DataTable GetByLocalLicenseID(int LocalLicenseID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
      ,[DriverID]
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses] where IssuedUsingLocalLicenseID=@LocalLicenseID
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;


        }

        public static DataTable GetByDriverID(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
      ,[DriverID]
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses] where DriverID=@DriverID
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;


        }

        public static DataTable GetDriverLicensesHistory(int DriverID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
  
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses] where DriverID=@DriverID
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;

        }
        public static DataTable GetByApplicationID(int ApplicationID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
      ,[DriverID]
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses] where ApplicationID=@ApplicationID
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;


        }
        public static DataTable GetByIsActive(bool IsActive)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  [InternationalLicenseID] as [Int.License ID]
      ,[ApplicationID]
      ,[DriverID]
      ,[IssuedUsingLocalLicenseID] as LicenseID
      ,[IssueDate]
      ,[ExpirationDate]
      ,[IsActive]
     
  FROM [DVLD].[dbo].[InternationalLicenses] where IsActive=@IsActive
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsActive", IsActive);
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
            catch (Exception ex)
            {
            }
            finally { connection.Close(); }
            return dt;


        }

        public static DataRow InternationalLicenseInfo(int IntLicenseID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
   Select FullName=Concat_ws(' ',P.FirstName,P.SecondName,p.ThirdName,P.LastName),
 InternationalLicenseID,L.LicenseID,P.NationalNo, case P.Gendor when 0 then 'Male' when 1 then 'Female' end as Gendor,
 I.IssueDate,I.ApplicationID,I.IsActive,P.DateOfBirth,I.DriverID,I.ExpirationDate,P.ImagePath
 from People P join Drivers D on P.PersonID =D.PersonID join Licenses L on L.DriverID=D.DriverID
 join InternationalLicenses I on I.IssuedUsingLocalLicenseID=L.LicenseID
 where InternationalLicenseID=@IntLicenseID
";

            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IntLicenseID", IntLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return dataTable.Rows[0];
        }

        public static int InternationalActiveLicenseID(int DriverID)
        {
            int intLicenseID = -1;
            SqlConnection connection= new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
  select InternationalLicenseID from InternationalLicenses
  where DriverID=@DriverID and IsActive=1 and (ExpirationDate>GetDate())";
            SqlCommand  command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                object result= command.ExecuteScalar();
                if(result==null ||result==DBNull.Value)
                {
                    connection.Close();
                    return -1;
                }
                int.TryParse(command.ExecuteScalar().ToString(), out intLicenseID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return intLicenseID;
        }

        public static int AddIntLicense(int ApplicationID,int DriverID,int IssuedUsingLocalLicenseID,DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            int intLicenseID= -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert into InternationalLicenses values(@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
Select Scope_Identity()";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out intLicenseID);
            }
            catch (Exception ex) { } finally { connection.Close(); }
            return intLicenseID;

        }



    }
}
