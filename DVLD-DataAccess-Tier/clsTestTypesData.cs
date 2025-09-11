using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsTestTypesData
    {
        public static bool Update(int TestID, string Title,string Description, double Fees)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
UPDATE TestTypes
   SET TestTypeTitle =@Title
      ,TestTypeFees = @Fees
      ,TestTypeDescription=@Description
 WHERE TestTypeID=@TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@Fees", Fees);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
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
        public static DataTable AllTestType()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT * FROM TestTypes;";
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

        public static bool FindTestType(int ID, ref string Title,ref string Description, ref double Fees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT * FROM TestTypes Where TestTypeID=@ID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    Title = reader["TestTypeTitle"].ToString();
                    Fees = Convert.ToDouble(reader["TestTypeFees"]);
                    Description = reader["TestTypeDescription"].ToString();
                }
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }

            return isFound;
        }

        public static double GetTestFees(int TestTypeID)
        {
            double Fees = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Select TestTypeFees from TestTypes where TestTypeID=@TestTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                Fees = Convert.ToDouble(command.ExecuteScalar());


            }
            catch(Exception ex) { }
            finally { connection.Close(); }
            return Fees;


        }




    }
}
