using DVLD_DataAccess_Tier;
using System;
using System.Data;
using System.Net;
using System.Security.Policy;

namespace DVLD_Business_Tier
{
    public class clsPerson
    {
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Gendor { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }

        public string ImagePath { get; set; }

        public DateTime DateOfBirth { get; set; }  

        private enum enMode { eAdd,eUpdate}
        private enMode _Mode=enMode.eAdd;

        public clsPerson()
        {
            PersonID = -1;
            NationalNo ="";
            FirstName ="";
            SecondName ="";
            ThirdName ="";
            LastName = "";
            Gendor = "";
            Address = "";
            Phone="";
            Email = "";
            Nationality = "";
            ImagePath = "";
            DateOfBirth = DateTime.Now;
            _Mode = enMode.eAdd;
        }

        private clsPerson(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, string Gendor, string Address, string Phone, string Email, string Nationality, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.Nationality = Nationality;
            this.ImagePath = ImagePath;
            this.DateOfBirth = DateOfBirth;
            _Mode = enMode.eUpdate;
             
        }

        private bool _Add()
        {
            PersonID= clsPersonData.AddPerson(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, Nationality, ImagePath);
            return PersonID != -1;
        }
        private bool _Update()
        {
            return clsPersonData.UpdatePerson(PersonID,NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, Nationality, ImagePath);
        }

        public string FullName()
        {
            return FirstName+" "+SecondName+" "+ThirdName+" "+LastName;
        }
        public static string FullName(int PersonID)
        {
            return clsPersonData.GetFullName(PersonID);
        }
        public bool Save()
        {
            switch( _Mode )
            {
                   case enMode.eUpdate:
                    {
                        return _Update();
                    }
                    case enMode.eAdd:
                    {
                       if( _Add() )
                        {
                            _Mode=enMode.eUpdate;
                            return true;
                        }
                       else
                            return false;

                    }
 
            }


            return false;






        }
        
        public static clsPerson Find(int PersonID)
        {
           
           string NationalNo = "";
           string FirstName = "";
           string SecondName = "";
           string ThirdName = "";
           string LastName = "";
           string Gendor = "";
           string Address = "";
           string Phone = "";
           string Email = "";
           string Nationality = "";
           string ImagePath = "";
          DateTime  DateOfBirth = DateTime.Now;
           
            if(clsPersonData.GetPersonByID(PersonID,ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref Nationality, ref ImagePath))
            {
                return new clsPerson(PersonID,NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, Nationality, ImagePath);
            }

            return null;




        }
        public static clsPerson Find(string NationalNo)
        {

            int PersonID = -1;
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            string Gendor = "";
            string Address = "";
            string Phone = "";
            string Email = "";
            string Nationality = "";
            string ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPersonData.GetPersonByNationalNo(ref PersonID,  NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref Nationality, ref ImagePath))
            {
                return new clsPerson(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, Nationality, ImagePath);
            }

            return null;




        }
        public static bool Delete(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static DataTable AllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static DataTable FilterByPersonID(int PersonID)
        {
            return clsPersonData.GetPersonByPersonID(PersonID);
        }

        public static DataTable FilterByNationalNo(string NationalNo)
        {
            return clsPersonData.GetPersonByNationalNo(NationalNo);
        }
        public static DataTable FilterByFirstName(string FirstName)
        {
            return clsPersonData.GetPersonByFirstName(FirstName);
        }
        public static DataTable FilterBySecondName(string SecondName)
        {
            return clsPersonData.GetPersonBySecondName(SecondName);
        }
         public static DataTable FilterByThirdName(string ThirdName)
        {
            return clsPersonData.GetPersonByThirdName(ThirdName);
        }
        public static DataTable FilterByLastName(string LastName)
        {
            return clsPersonData.GetPersonByLastName(LastName);
        }
        public static DataTable FilterByGendor(int Gendor)
        {
            return clsPersonData.GetPersonByGendor(Gendor);
        }
        public static DataTable FilterByPhone(string Phone)
        {
            return clsPersonData.GetPersonByPhone(Phone);
        }
        public static DataTable FilterByEmail(string Email)
        {
            return clsPersonData.GetPersonByEmail(Email);
        }
        public static DataTable FilterByNationality(string Nationality)
        {
            return clsPersonData.GetPersonByCountryName(Nationality);
        }

        public static DataTable AllCounties()
        {
            return clsPersonData.GetAllCountries();
        }

        public static bool IsThisNationalNoExist(string NationalNo)
        {
            return clsPersonData.IsThisNationalNoExistForPerson(NationalNo);
        }


    }




}
