using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsApplicationTypesData
    {
        public static bool Update(int ApplicationTypeID,string Title,double Fees)
        {
            int RowAffected = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
UPDATE ApplicationTypes
   SET ApplicationTypeTitle =@Title
      ,ApplicationFees = @Fees
 WHERE ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@Title", Title);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }

            return RowAffected > 0;

        }
        public static DataTable AllApplicationType()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT * FROM ApplicationTypes;";
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
catch(Exception ex)
            {

            }

            finally { connection.Close(); }
            return dt;

        }

        public static bool FindApplicationType(int ID,ref string Title,ref double Fees)
        {
            bool isFound=false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT * FROM ApplicationTypes Where ApplicationTypeID=@ID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    isFound = true;
                    Title = reader["ApplicationTypeTitle"].ToString();
                    Fees = Convert.ToDouble(reader["ApplicationFees"]);
                }
                reader.Close();

            }
            catch(Exception ex)
            {

            }
            finally{ connection.Close(); }

            return isFound;
        }

    }
}
