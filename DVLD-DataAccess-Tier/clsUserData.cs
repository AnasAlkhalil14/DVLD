using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;



namespace DVLD_DataAccess_Tier
{
    public class clsUserData
    {

    static public bool FindUserByUserNameAndPassword(string UserName,string Password,ref int UserID,ref int PersonID,ref bool IsActive)
        {
            bool IsFound=false;
          SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  
      UserID
      ,PersonID
      ,UserName
      ,Password
      ,IsActive
       FROM Users
     where Password=@Password and UserName=@UserName;";
            SqlCommand command=new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    if (Convert.ToInt32(reader["IsActive"]) == 1)
                    {
                        IsActive = true;
                    }
                    else
                    {
                        IsActive = false;
                    }

                }
                else
                    IsFound = false;


                        reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


return IsFound;


        }
        static public bool FindUserByUserID(ref string UserName,ref string Password,  int UserID, ref int PersonID, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT  
      UserID
      ,PersonID
      ,UserName
      ,Password
      ,IsActive
       FROM Users
     where UserID=@UserID ;";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);
       

            try
            {
                connection.Open(); 
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    IsFound = true;
                    Password = reader["Password"].ToString();
                    UserName = reader["UserName"].ToString();
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    if (Convert.ToInt32(reader["IsActive"]) == 1)
                    {
                        IsActive = true;
                    }
                    else
                    {
                        IsActive = false;
                    }

                }
                else
                    IsFound = false;


                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }


            return IsFound;


        }

        static public DataTable GetUserByFullName(string FullName)

        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT  
       UserID   
      ,Users.PersonID 
	  ,FullName =People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
      ,UserName 
      ,Password 
      ,IsActive 
  FROM Users join People on Users.PersonID=People.PersonID
WHERE   People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName like @FullName";
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



            if (result.Rows.Count > 0)
                return result;
            else return null;


        }
        public static DataTable GetUserByUserName(string UserName)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT  
       UserID   
      ,Users.PersonID 
	  ,FullName =People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
      ,UserName 
      ,Password 
      ,IsActive 
  FROM Users join People on Users.PersonID=People.PersonID
WHERE  UserName like @UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName + "%");
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



            if (result.Rows.Count > 0)
                return result;
            else return null;


        }
        public static DataTable GetUserByUserID( int UserID)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT  
       UserID   
      ,Users.PersonID 
	  ,FullName =People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
      ,UserName 
      ,Password 
      ,IsActive 
  FROM Users join People on Users.PersonID=People.PersonID
WHERE  UserID =@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
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


            if(result.Rows.Count > 0)
            return result;
            else return null;   


        }
        public static DataTable GetUserByPersonID(int PersonID)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT  
       UserID   
      ,Users.PersonID 
	  ,FullName =People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
      ,UserName 
      ,Password 
      ,IsActive 
  FROM Users join People on Users.PersonID=People.PersonID
WHERE  PersonID =@PersonID";
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



            if (result.Rows.Count > 0)
                return result;
            else return null;


        }
        public static DataTable GetUserByActiveState(bool IsActive)
        {

            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT  
       UserID   
      ,Users.PersonID 
	  ,FullName =People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
      ,UserName 
      ,Password 
      ,IsActive 
  FROM Users join People on Users.PersonID=People.PersonID
WHERE  IsActive =@IsActive";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@IsActive", IsActive);
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



            if (result.Rows.Count > 0)
                return result;
            else return null;


        }

        public static DataTable GetAllPeople()
        {
            DataTable result = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
SELECT  
       UserID   
      ,Users.PersonID 
	  ,FullName =People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName
      ,UserName 
      ,Password 
      ,IsActive 
  FROM Users join People on Users.PersonID=People.PersonID;
";
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


            if (result.Rows.Count > 0)
                return result;
            else return null;


        }
        
        public static bool IsPersonUser(int PersonID)
        {
            bool Answer=false;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Select 1 from Users where PersonID=@PersonID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    Answer = true;
                }
                else
                    Answer = false;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return Answer;

        }

       public static bool Delete(int UserID)
        {
            int RowAffected = 0;
         SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = "Delete From Users Where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return RowAffected > 0;

        }
        public static int AddUser( int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection= new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
INSERT INTO [dbo].[Users]
           ([PersonID]
           ,[UserName]
           ,[Password]
           ,[IsActive])
     VALUES
           (@PersonID,@UserName,@Password,@IsActive);
 SELECT SCOPE_IDENTITY();";
SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if(int.TryParse(result.ToString(), out UserID))
                {

                }


            }
            catch (Exception ex)
            {

            }
            finally {  connection.Close(); }
            return UserID;

        }
        public static bool UpdateUser(int UserID,int PersonID, string UserName, string Password, bool IsActive)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
UPDATE [dbo].[Users]
   SET [PersonID] = @PersonID
      ,[UserName] = @UserName
      ,[Password] = @Password
      ,[IsActive] = @IsActive
 WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            finally { connection.Close(); }

            return RowAffected > 0;

        }

        public static bool UpdateUserPassword(int UserID, string Password)
        {
            int RowAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
UPDATE [dbo].[Users]
   SET 
      [Password] = @Password
      
 WHERE UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
          
            
            command.Parameters.AddWithValue("@Password", Password);
      
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }

            finally { connection.Close(); }

            return RowAffected > 0;

        }

    }
}
