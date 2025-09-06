using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;



namespace DVLD_DataAccess_Tier
{

    public class clsPersonData
    {
       
        public static int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, string Gendor, string Address, string Phone, string Email, string CountryName, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
INSERT INTO [dbo].[People]
           ([NationalNo]
           ,[FirstName]
           ,[SecondName]
           ,[ThirdName]
           ,[LastName]
           ,[DateOfBirth]
           ,[Gendor]
           ,[Address]
           ,[Phone]
           ,[Email]
           ,[NationalityCountryID]
           ,[ImagePath])
     VALUES
           (@NationalNo
           ,@FirstName
           ,@SecondName
           ,@ThirdName
           ,@LastName
           ,@DateOfBirth
           ,(case when @Gendor='Male' then 0 else 1 end)
           ,@Address
           ,@Phone
           ,@Email
           ,(Select CountryID from Countries where CountryName=@CountryName)
           ,@ImagePath);
          SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (!string.IsNullOrWhiteSpace(Email))
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (!string.IsNullOrWhiteSpace(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (int.TryParse(result.ToString(), out PersonID))
                    {

                    }



                }
                catch (Exception ex)
                {

                }
                finally { connection.Close(); }


            return PersonID;

        }

        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, string Gendor, string Address, string Phone, string Email, string CountryName, string ImagePath)

        {
            int RowAffect = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
UPDATE [dbo].[People]
   SET [NationalNo]           = @NationalNo
      ,[FirstName]            = @FirstName
      ,[SecondName]           = @SecondName
      ,[ThirdName]            = @ThirdName
      ,[LastName]             = @LastName
      ,[DateOfBirth]          = @DateOfBirth
      ,[Gendor]               = (case when @Gendor='Male' then 0 else 1 end)
      ,[Address]              = @Address
      ,[Phone]                = @Phone
      ,[Email]                = @Email
      ,[NationalityCountryID] = (Select CountryID from Countries where CountryName=@CountryName)
      ,[ImagePath]            = @ImagePath
 WHERE PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (!string.IsNullOrWhiteSpace(Email))
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (!string.IsNullOrWhiteSpace(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);



            try
            {

                connection.Open();
                RowAffect = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return RowAffect > 0;


        }

        public static bool DeletePerson(int ID)
        {
            int RowAffect = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Delete from People Where PersonID=@ID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("ID", ID);
            try
            {

                connection.Open();
                RowAffect = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return RowAffect > 0;


        }
        public static bool GetPersonByNationalNo( ref int PersonID,  string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref string Gendor, ref string Address, ref string Phone, ref string Email, ref string CountryName, ref string ImagePath)

        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, People.ImagePath, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.NationalNo=@NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"].ToString();
                    LastName = reader["LastName"].ToString();

                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    if (reader["Email"] == DBNull.Value)
                    {
                        Email = "";
                    }
                    else
                    {
                        Email = reader["Email"].ToString();
                    }

                    CountryName = reader["CountryName"].ToString();

                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }

                    Gendor = reader["Gendor"].ToString();





                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }



            return isFound;


        }
        public static bool GetPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref string Gendor, ref string Address, ref string Phone, ref string Email, ref string CountryName, ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, People.ImagePath, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                   
                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"].ToString();
                    LastName = reader["LastName"].ToString();

                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    if (reader["Email"] ==DBNull.Value)
                    {
                        Email = "";
                    }
                    else
                    {
                        Email = reader["Email"].ToString();
                    }

                    CountryName = reader["CountryName"].ToString();

                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        ImagePath = "";
                    }
                    else
                    {
                        ImagePath = reader["ImagePath"].ToString();
                    }

                    Gendor= reader["Gendor"].ToString() ;   
                    
                



                }

                reader.Close(); 

            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }



            return isFound;


        }
        public static DataTable GetPersonByPersonID(int PersonID)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email,
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.PersonID=@PersonID";
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
        public static DataTable GetPersonByNationalNo( string NationalNo)
        {
            
           DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.NationalNo like @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo+"%");
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

        public static DataTable GetPersonByFirstName(string FirstName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.FirstName like @FirstName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName+"%");
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
        public static DataTable GetPersonBySecondName(string SecondName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email,  
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.SecondName like @SecondName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SecondName", SecondName + "%");
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

        public static DataTable GetPersonByThirdName(string ThirdName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.ThirdName like @ThirdName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ThirdName", ThirdName + "%");
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
        public static DataTable GetPersonByLastName(string LastName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.LastName like @LastName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LastName", LastName + "%");
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
        public static DataTable GetPersonByCountryName(string CountryName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email,
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where Countries.CountryName like @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName + "%");
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
        public static DataTable GetPersonByGendor(int Gendor)
        {

            


                DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email,
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.Gendor=@Gendor";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Gendor", Gendor);
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
        public static DataTable GetPersonByPhone(string Phone)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.Phone like @Phone";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", Phone + "%");
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
        public static DataTable GetPersonByEmail(string Email)

        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email,
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID
                   Where People.Email like @Email";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email + "%");
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
         
        public static DataTable GetAllPeople()
        {
            DataTable result = new DataTable();
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, People.DateOfBirth,case when People.Gendor=0 then 'Male' else 'Female' end as Gendor, People.Address, People.Phone, People.Email, 
                  Countries.CountryName
FROM     People INNER JOIN
                  Countries ON People.NationalityCountryID = Countries.CountryID";
            SqlCommand command=new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    result.Load(reader);
                }

                reader.Close();
 
            }
            catch (Exception ex)
            { } 
            finally
            {
                connection.Close();
            }


            return result; 


        }
      
        public static DataTable GetAllCountries()
        {
            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT [CountryID]
      ,[CountryName]
  FROM [dbo].[Countries]";
            SqlCommand command = new SqlCommand(query, connection);

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
            { }
            finally
            {
                connection.Close();
            }


            return result;

        }

        public static bool IsThisNationalNoExistForPerson(string NationalNo)
        {
            bool IsExist=false;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Select 3 from People where NationalNo=@NationalNo";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if(result!=null)
                {
                    IsExist=true;   
                }
                

            }
            catch(Exception ex)  
            {

            }
            finally
            {
                connection.Close();
            }


            return IsExist;
        }

        public static string GetFullName(int PersonID)
        {
            string FullName = "";
SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Select FullName=FirstName+' '+SecondName+' '+ThirdName+' '+LastName from People where PersonID=@PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                FullName = command.ExecuteScalar().ToString();


            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return FullName;
        }


    }
}