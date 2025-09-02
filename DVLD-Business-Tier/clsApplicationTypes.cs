using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsApplicationTypes
    {
        public int ID {  get; set; }
        public string Title { get; set; }

        public double Fees { get; set; }
        private clsApplicationTypes(int ID,string Title,double Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static clsApplicationTypes Find(int ID)
        {
            string Title = "";
            double Fees=-1;
            if (clsApplicationTypesData.FindApplicationType(ID, ref Title, ref Fees))
            {
                return new clsApplicationTypes(ID, Title, Fees);
            }
            else
                return null;

        }
        public  static DataTable AllAplicationType()
        {
            return clsApplicationTypesData.AllApplicationType();
        }
        public  bool Update()
        {
            return clsApplicationTypesData.Update(ID, Title, Fees);
        }
        public  static bool Update(int ApplicationID,string Title,double Fees)
        {
            return clsApplicationTypesData.Update(ApplicationID, Title, Fees);
        }


    }
}
