using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsDriverData
    {

        public static int AddDriver(int PersonID,int CreatedByUserID,DateTime  CreatedDate)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert into Drivers Values(@PersonID,@CreatedByUserID,@CreatedDate);
                            Select Scope_Identity()";
        SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                int.TryParse(result.ToString(), out DriverID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return DriverID;



        }

        public static bool IsPersonDriver(int PersonID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"select 1 from Drivers where PersonID=@PersonID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null) IsFound = true;
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return IsFound;

        }

        public static bool FindByPersonID(ref int DriverID,int PersonID,ref int CreatedByUserID,ref DateTime CreatedDate)
        {
            bool isFound=false;
            SqlConnection connection= new SqlConnection(clsConnectionString.ConnectionString);
            string query= @"Select * from Drivers where PersonID=@PersonID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
    try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID= Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                    isFound = true;
                }


            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return isFound;

        }

        public static int GetDriverIDByPersonID(int PersonID)
        {
            int DriverID=-1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"select DriverID from Drivers where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result == null||result==DBNull.Value)
                {
                    connection.Close();
                    return -1;
                }
                int.TryParse(command.ExecuteScalar().ToString(), out DriverID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return DriverID;

        }

        public static int GetPersonIDByDriverID(int DriverID)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"select PersonID from Drivers where DriverID=@DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out PersonID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return PersonID;

        }


        public static DataTable GetAllDrivers()
        {
            DataTable dt=new DataTable();
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select * from Drivers_View2";
            SqlCommand command=new SqlCommand(query, connection);   
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return dt;

        }

        public static DataTable GetDriverByDriverID(int DriverID)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select * from Drivers_View2
                   Where DriverID=@DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result.Load(reader);


                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }



            return result;


        }
        public static DataTable GetDriverByPersonID(int PersonID)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select * from Drivers_View2
                   Where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result.Load(reader);


                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }



            return result;


        }
        public static DataTable GetDriverByNationalNo(string NationalNo)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select * from Drivers_View2
                   Where NationalNo like @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo + "%");
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result.Load(reader);


                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }



            return result;


        }
        public static DataTable GetDriverByFullName(string FullName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select * from Drivers_View2
                   Where FullName like @FullName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", FullName + "%");
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    result.Load(reader);


                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }



            return result;


        }



    }
}
