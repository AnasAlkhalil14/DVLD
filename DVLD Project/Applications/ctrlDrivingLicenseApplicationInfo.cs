using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadDataInfo(int LDLAppID)
        {
            lblDLAppID.Text = LDLAppID.ToString();
            lblLicenseClass.Text=clsLocalDrivingLicenseApplications.LicenseClassString(LDLAppID);
            lblPassedTests.Text = clsLocalDrivingLicenseApplications.PassedTestCount(LDLAppID).ToString();
            ctrlApplicationBasicInfo1.LoadDataApplicationInfo(clsLocalDrivingLicenseApplications.AppID(LDLAppID));


        }


        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
