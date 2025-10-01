using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsDetainedLicenseData
    {
       public static int DetainLicense(int LicenseID,DateTime DetainDate,double FineFees,int CreatedByUserID)
        {
            int DetainId = -1;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"INSERT INTO [dbo].[DetainedLicenses]
           ([LicenseID]
           ,[DetainDate]
           ,[FineFees]
           ,[CreatedByUserID]
           ,[IsReleased])
            
     VALUES
           (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0);
Select  Scope_Identity();
";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result==null)
                {
                    connection.Close();
                    return -1;
                }
                else
                {
                    int.TryParse(result.ToString(), out DetainId);
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return DetainId;

        }
        public static bool FindDetainLicense(int LicenseID,ref int DetainID,ref DateTime DetainDate,ref double FineFees,ref int CreatedByUserID)
        {
            bool IsFound=false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT [DetainID]
      ,[LicenseID]
      ,[DetainDate]
      ,[FineFees]
      ,[CreatedByUserID]

  FROM [DVLD].[dbo].[DetainedLicenses]
  where LicenseID=@LicenseID and IsReleased=0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    FineFees = Convert.ToDouble(reader["FineFees"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);

                    IsFound = true;
                }
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;


        }

        public static bool ReleaseLicense(int DetainID,DateTime ReleaseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            int RowAffected = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Update DetainedLicenses set IsReleased=1,ReleaseDate=@ReleaseDate,ReleasedByUserID=@ReleasedByUserID,ReleaseApplicationID=@ReleaseApplicationID
    where DetainID=@DetainID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReleaseDate", @ReleaseDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {  connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { } finally { connection.Close(); }
            return RowAffected > 0;
        }
        
        public static DataTable DetainedLicensesList()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM DetainedLicenses_View

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
        public static DataTable FilterByDetainID(int DetainID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM DetainedLicenses_View D
  where D.DetainID=@DetainID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
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
        public static DataTable FilterByReleaseAppID(int ReleaseAppID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM DetainedLicenses_View D
  where D.ReleaseApplicationID=@ReleaseAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReleaseAppID", ReleaseAppID);
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
        public static DataTable FilterByIsReleased(bool IsReleased)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM DetainedLicenses_View D
  where D.IsReleased=@IsReleased";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
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

        public static DataTable FilterByFullName(string FullName)

        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM DetainedLicenses_View
 Where FullName like @FullName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", FullName + "%");
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
        public static DataTable FilterByNationalNO(string NationalNo)

        {
            DataTable dt = new DataTable();
           
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM DetainedLicenses_View
 Where NationalNo like @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo + "%");
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

    }
}
