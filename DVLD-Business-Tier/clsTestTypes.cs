using DVLD_DataAccess_Tier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business_Tier
{
    public class clsTestTypes
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public double Fees { get; set; }
        private clsTestTypes(int ID, string Title,string Description, double Fees)
        {
            this.Description = Description;
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
        }

        public static clsTestTypes Find(int ID)
        {
            string Description = "";
            string Title = "";
            double Fees = -1;
            if (clsTestTypesData.FindTestType(ID, ref Title,ref Description, ref Fees))
            {
                return new clsTestTypes(ID, Title,Description, Fees);
            }
            else
                return null;

        }
        public static DataTable AllTestType()
        {
            return clsTestTypesData.AllTestType();
        }
        public bool Update()
        {
            return clsTestTypesData.Update(ID, Title,Description, Fees);
        }
        public static bool Update(int ApplicationID, string Title,string Description, double Fees)
        {
            return clsTestTypesData.Update(ApplicationID, Title,Description, Fees);
        }









    }
}
