using DVLD.Properties;
using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Licenses
{
    public partial class ctrlLicenseInfo : UserControl
    {
        public ctrlLicenseInfo()
        {
            InitializeComponent();
        }
        void SetDefaultImage(int Gendor)
        {
            if (Gendor == 1)
                pbPersonImage.Image = Resources.Female_512;
            else pbPersonImage.Image = Resources.Male_512;

        }
   public     void LoadData(int LicenseID)
        {
            DataRow row =clsLicense.LicenseInfoRow(LicenseID);
            lblClass.Text = row["ClassName"].ToString();
            lblIssueReason.Text = row["IssueReason"].ToString();
            lblIssueDate.Text = Convert.ToDateTime(row["IssueDate"]).ToShortDateString();
            lblIsDetained.Text = row["IsDetained"].ToString();
            lblIsActive.Text = row["IsActive"].ToString();
            if(Convert.ToInt32(row["Gendor"])==0)
            lblGendor.Text = "Male";
            else
                lblGendor.Text = "Female";

            lblExpirationDate.Text = Convert.ToDateTime(row["ExpirationDate"]).ToShortDateString();
            lblDriverID.Text = row["DriverID"].ToString();
            lblLicenseID.Text = row["LicenseID"].ToString();
            lblName.Text = row["FullName"].ToString();
            if (string.IsNullOrWhiteSpace(row["Notes"].ToString()))
                lblNotes.Text = "No Notes";
            else
                lblNotes.Text = row["Notes"].ToString();
            lblNationalNo.Text = row["NationalNo"].ToString();
            lblDateOfBirth.Text = Convert.ToDateTime(row["DateOfBirth"]).ToShortDateString();
            string ImagePath= row["ImagePath"].ToString();
            if(!string.IsNullOrWhiteSpace(ImagePath))
            {
                if(File.Exists(ImagePath))
                {
                    pbPersonImage.Image=Image.FromFile(ImagePath);
                }
                else
                {
                    SetDefaultImage(Convert.ToInt32(row["Gendor"]));

                }



            }
            else
                SetDefaultImage(Convert.ToInt32(row["Gendor"])); 

        }
        public DateTime IssueDate { get { return Convert.ToDateTime(lblExpirationDate.Text); } }

        public DateTime ExpirationDate { get { return Convert.ToDateTime(lblExpirationDate.Text); } }
       public int DriverID { get { return Convert.ToInt32(lblDriverID.Text); } }
        public bool IsDetain { get { return Convert.ToBoolean(lblIsDetained.Text); } }

        public bool IsActive { get { return Convert.ToBoolean(lblIsActive.Text); } }
        public string Notes { get { return lblNotes.Text; } }
        private void ctrlLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void gbDriverLicenseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
