using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess_Tier
{
    public class clsLocalDrivingLicenseApplicationsData
    {

        public static int Add(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Insert Into LocalDrivingLicenseApplications Values (@ApplicationID,@LicenseClassID);
                          Select Scope_Identity()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out LocalDrivingLicenseApplicationID))
                {

                }


            }
            catch (Exception ex)
            {


            }

            finally { connection.Close(); }

            return LocalDrivingLicenseApplicationID;
        }
        public static DataTable GetAllLicenceClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT LicenseClassID, ClassName
FROM     LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    dt.Load(reader);

            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            return dt;

        }

        public static bool CheckIfCanMakeApplicationForNewLocalLicence(int PersonID, int LicenceClassID)
        {
            bool Result = false;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
 SELECT 1
   FROM Applications join LocalDrivingLicenseApplications on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID
  join LicenseClasses on LicenseClasses.LicenseClassID=LocalDrivingLicenseApplications.LicenseClassID

  where Applications.ApplicationTypeID=1 and( Applications.ApplicationStatus in(1,3)) and LicenseClasses.LicenseClassID=@LicenceClassID and Applications.ApplicantPersonID=@PersonID;

  ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenceClassID", LicenceClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    Result = false;

                }
                else
                {
                    Result = true;
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return Result;


        }

        public static DataTable GetAllLocalLicenseApplications()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM LocalDrivingLicenseApplications_View";
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

        public static DataTable GetAllLocalLicenseApplicationsByByID(int LDLAppID)

        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM LocalDrivingLicenseApplications_View
 Where LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
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

        public static DataTable GetAllLocalLicenseApplicationsByFullName(string FullName)

        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM LocalDrivingLicenseApplications_View
 Where FullName like @FullName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", FullName+"%");
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
        public static DataTable GetAllLocalLicenseApplicationsByStatus(string Status)

        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM LocalDrivingLicenseApplications_View
 Where Status=@Status";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Status", Status);
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
        public static DataTable GetAllLocalLicenseApplicationsByNationalNo(string NationalNo)

        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT *
  FROM LocalDrivingLicenseApplications_View
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

        public static bool CanceleApplication(int LDLApplicationID)
        {
          
            int RowAffected = 0;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Update a set a.ApplicationStatus=2 ,a.LastStatusDate=@Now
from Applications a join LocalDrivingLicenseApplications l on a.ApplicationID=l.ApplicationID
                           where
   l.LocalDrivingLicenseApplicationID=@LDLApplicationID and a.ApplicationStatus=1";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLApplicationID", LDLApplicationID);
            command.Parameters.AddWithValue("@Now",DateTime.Now);
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

        public static string GetLicenseClassName(int LocalDrivingLicenseApplicationID)
        {
            string ClassName = "";
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT LicenseClasses.ClassName
  FROM [DVLD].[dbo].[LocalDrivingLicenseApplications]
  join LicenseClasses on LocalDrivingLicenseApplications.LicenseClassID=LicenseClasses.LicenseClassID
  where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                ClassName = command.ExecuteScalar().ToString();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return ClassName;

        }

        public static string GetFullName(int LDLAppID)
        {
            string FullName = "";
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"SELECT FullName=concat_ws(' ',People.FirstName,People.SecondName,People.ThirdName,People.LastName)
from LocalDrivingLicenseApplications LDLA join Applications A on LDLA.ApplicationID=A.ApplicationID
join People on A.ApplicantPersonID =People.PersonID
where LDLA.LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                FullName = command.ExecuteScalar().ToString();

            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return FullName;
        }

        public static int GetPassedTestCount(int LDLAppID)
        {
            int PassedTestCount = -1;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"Select count(*) from LocalDrivingLicenseApplications LDLA
join TestAppointments TA on LDLA.LocalDrivingLicenseApplicationID=TA.LocalDrivingLicenseApplicationID
join Tests T on T.TestAppointmentID=TA.TestAppointmentID
where LDLA.LocalDrivingLicenseApplicationID=@LDLAppID and T.TestResult=1;";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                PassedTestCount = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) { }
            finally { connection.Close(); }
            return PassedTestCount;

        }

        public static int GetApplicationID(int LDLAppID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
Select ApplicationID  from LocalDrivingLicenseApplications LDLA
where LDLA.LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command=new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                ApplicationID = Convert.ToInt32(command.ExecuteScalar());
            }catch (Exception ex) { }
            finally { connection.Close(); }
            return ApplicationID;


        }

        public static int GetPersonIDByLocalDLAppID(int LDLAppID)
        {
            int PersonID = -1;
            SqlConnection connection=new SqlConnection(clsConnectionString.ConnectionString);
            string query = @"
select A.ApplicantPersonID from Applications A join LocalDrivingLicenseApplications LDL
on A.ApplicationID=LDL.ApplicationID
where LDL.LocalDrivingLicenseApplicationID=@LDLAppID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LDLAppID", LDLAppID);
            try
            {
                connection.Open();
                int.TryParse(command.ExecuteScalar().ToString(), out PersonID);
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

        return PersonID;
        }


    }


}