using DVLD.Properties;
using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrlInternationalLicenseInfo : UserControl
    {
        public ctrlInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        void LoadImage(string ImagePath,string Gendor)
        {
            if(string.IsNullOrWhiteSpace(ImagePath))
            {
                if (Gendor == "Male") pbPersonImage.Image = Resources.Male_512;
                else pbPersonImage.Image = Resources.Female_512;

                return;

            }


            if(File.Exists(ImagePath))
            {
                pbPersonImage.Image=Image.FromFile(ImagePath);  
            }
            else
            {
                if (Gendor == "Male") pbPersonImage.Image = Resources.Male_512;
                else pbPersonImage.Image = Resources.Female_512;


            }


        }

      public void LoadData(int IntLicenseID)
        {
            DataRow row =clsInternationalDrivingLicenseApplications.InternationalLicenseInfo(IntLicenseID);
            lblApplicationID.Text = row["ApplicationID"].ToString();
            lblDateOfBirth.Text = Convert.ToDateTime(row["DateOfBirth"]).ToShortDateString();
            lblDriverID.Text =row["DriverID"].ToString();
            lblExpirationDate.Text= Convert.ToDateTime(row["ExpirationDate"]).ToShortDateString();
            lblIssueDate.Text= Convert.ToDateTime(row["IssueDate"]).ToShortDateString();
            lblLicenseID.Text= row["LicenseID"].ToString();
            lblName.Text = row["FullName"].ToString() ;
            lblNationalNo.Text = row["NationalNo"].ToString();
            lblIntLicenseID.Text = row["InternationalLicenseID"].ToString();
            lblGendor.Text = row["Gendor"].ToString();
            if (Convert.ToBoolean(row["IsActive"]))
            {
                lblIsActive.Text = "Yes";
            }
            else lblIsActive.Text = "No";



            LoadImage(row["ImagePath"].ToString(),lblGendor.Text);  



        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
