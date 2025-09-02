using DVLD_DataAccess_Tier;
using System;
using System.Data;


namespace DVLD_Business_Tier
{
    public class clsUser
    {
         public clsPerson Person {  get; set; }
       public int UserID {  get; set; }
        public string UserName { get; set; }    
        public string Password { get; set; }   
        public bool IsActive {  get; set; }

        private enum enMode { eAdd,eUpdate}
        private enMode Mode=enMode.eAdd;
        public clsUser()
        {

            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = false;
            Mode = enMode.eAdd;
            Person = new clsPerson();
        }
        

        private clsUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            Person=clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;
            Mode=enMode.eUpdate; 
        }

        public static clsUser Find(string UserName,string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
            if(clsUserData.FindUserByUserNameAndPassword(UserName,Password,ref UserID,ref PersonID,ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;


        }
        public static clsUser Find(int UserID)
        {
            string UserName = "";
            string Password = "";
            int PersonID = -1;
            bool IsActive = false;
            if (clsUserData.FindUserByUserID(ref UserName, ref Password,   UserID, ref PersonID, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            return null;


        }



        public static DataTable AllUsers()
        {
            return clsUserData.GetAllPeople();
        }

        public static bool IsPersonUser(int PersonID)
        {
            return clsUserData.IsPersonUser(PersonID);
        }
        private bool _Add()
        {
            UserID=clsUserData.AddUser(Person.PersonID,UserName,Password,IsActive);
            return UserID!=-1 ;
        }
        private bool _Update()
        {
            return clsUserData.UpdateUser(UserID, Person.PersonID, UserName, Password, IsActive);
        }
        public static bool Delete(int UserID)
        {
            return clsUserData.Delete(UserID);
        }
        public bool Save()
        {
            switch (Mode)
            {
               case enMode.eAdd:
                    {
                        if(_Add())
                        {
                            Mode = enMode.eUpdate;
                            return true;
                        }
                     else
                            return false;
                    }

                    case enMode.eUpdate:
                    {
                        return _Update();
                    }



            }

            return false;


        }

        public static DataTable FilterByUserName(string UserName)
        {
            return clsUserData.GetUserByUserName(UserName);
        }

        public static DataTable FilterByUserID(int UserID)
        {
            return clsUserData.GetUserByUserID(UserID);
        }
        public static DataTable FilterByPersonID(int PersonID)
        {
            return clsUserData.GetUserByPersonID(PersonID);
        }
        public static DataTable FilterByActiveState(bool Active)
        {
            return clsUserData.GetUserByActiveState(Active);
        }
        public static DataTable FilterByFullName(string FullName)
        {
            return clsUserData.GetUserByFullName(FullName);
        }
        public static bool UpdatePassword(int UserID,string NewPassword)
        {
            return clsUserData.UpdateUserPassword(UserID, NewPassword);
        }



    }
}
