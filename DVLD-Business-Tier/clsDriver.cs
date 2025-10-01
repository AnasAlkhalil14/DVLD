using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsDriver
    {
    public int DriverID {  get; set; }
    public    int PersonID {  get; set; }
        public int CreatedByUserID {  get; set; }
        public DateTime CreatedDate { get; set; }
        public clsDriver() { }
        private clsDriver(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.CreatedDate = CreatedDate;
         this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
        }

        public bool Add()
        {
            int DriverID =clsDriverData.AddDriver(PersonID,CreatedByUserID,CreatedDate);
            return DriverID != -1;
        }
        public static bool IsDriver(int PersonID)
        {
            //int DriverID=clsDriverData.GetDriverIDByPersonID(PersonID);
            //return DriverID != -1;


            return clsDriverData.IsPersonDriver(PersonID);
        }

        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1,CreatedByUserID=-1;
            DateTime CreatedDate=DateTime.MinValue;
            if(clsDriverData.FindByPersonID(ref DriverID,PersonID,ref CreatedByUserID,ref CreatedDate))
            { 
                return new clsDriver(DriverID,PersonID,CreatedByUserID,CreatedDate);
            }
            return null;

        }
        public static int GetPersonIDByDriverID(int DriverID)
        {
            return clsDriverData.GetPersonIDByDriverID(DriverID);
        }
        public static int GetDriverIDByPersonID(int PersonID)
        {
            return clsDriverData.GetDriverIDByPersonID (PersonID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public static DataTable GetDriverByDriverID(int DriverID)
        {
            return clsDriverData.GetDriverByDriverID (DriverID);
        }
        public static DataTable GetDriverByPersonID(int PersonID)
        {
            return clsDriverData.GetDriverByPersonID (PersonID);
        }
        public static DataTable GetDriverByNationalNo(string NationalNo)
        {
            return clsDriverData.GetDriverByNationalNo (NationalNo);
        }
        public static DataTable GetDriverByFullName(string FullName)
        {
            return clsDriverData.GetDriverByFullName (FullName);
        }

    }
}
