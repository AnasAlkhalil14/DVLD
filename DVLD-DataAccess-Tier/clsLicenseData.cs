using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsLicenseData
    {
        public static int AddLicense(int ApplicationID,int DriverID,int LicenseClass,DateTime IssueDate,
            DateTime ExpirationDate,string Notes,double PaidFees,bool IsActive,int IssueReason,int CreatedByUserID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert into Licenses values(@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,
@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID);
Select Scope_Identity()";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if(!string.IsNullOrWhiteSpace(Notes))
            command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);



            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out LicenseID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return LicenseID;

        }


        public static DataRow GetLicenseInfo(int  LicenseID)
        {

            DataTable dt=new DataTable();
            SqlConnection connection= new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select * from LicensesInfo where LicenseID=@LicenseID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
            return dt.Rows[0];

        }

        public static int GetLicenseIDByAppID(int AppID)
        {
            int LicenseID = -1;
        SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select LicenseID from Licenses where ApplicationID=@AppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@AppID", AppID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out LicenseID);

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return LicenseID;
        }

        public static DataTable GetLicensesHistory(int DriverID)
        {
            DataTable dataTable = new DataTable();


            SqlConnection connection= new SqlConnection(clsConnectionString.ConnectionString); 
            string query = @"SELECT [Lic.ID]=LicenseID
      ,[App.ID]=ApplicationID
      ,[Class Name]=lc.ClassName
      ,[Issue Date]=L.IssueDate
      ,[Expiration Date]=ExpirationDate
       
      ,[Is Active]=IsActive
       
  FROM Licenses L join LicenseClasses LC on L.LicenseClass=LC.LicenseClassID
 where L.DriverID=@DriverID
";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
            return dataTable;


        }

        public static bool IsLicenseExist(int LicenseID)
        {
            bool isFound=false;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select 1 from Licenses where LicenseID=@LicenseID ";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();

                object result= command.ExecuteScalar();
                
                if(result!=null)
                {
                    isFound = true;
                }
                

            }
            catch (Exception ex)
            { }
            finally{ connection.Close(); }
            return isFound;
        }

        public static int LicenseClassIDByLicenseID(int LicenseID)
        {
            int ClassID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select LicenseClass from Licenses where LicenseID=@LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out ClassID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ClassID;
        }

        public static bool ChangeLicenseIsActive(int LicenseID,bool NewIsActive)
        {
            int RowAffected = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Update Licenses set IsActive=@NewIsActive where LicenseID=@LicenseID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@NewIsActive", NewIsActive);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return RowAffected >0;

        }



    }
}
