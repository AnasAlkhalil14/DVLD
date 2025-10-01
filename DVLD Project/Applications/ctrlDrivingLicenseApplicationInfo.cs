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
        public event Action OnEditPerson;
        public void LoadDataInfo(int LDLAppID)
        {
            lblDLAppID.Text = LDLAppID.ToString();
            lblLicenseClass.Text=clsLocalDrivingLicenseApplications.LicenseClassString(LDLAppID);
            lblPassedTests.Text = clsLocalDrivingLicenseApplications.PassedTestCount(LDLAppID).ToString();
            ctrlApplicationBasicInfo1.LoadDataApplicationInfo(clsLocalDrivingLicenseApplications.AppID(LDLAppID));


        }

        public int LDLAppID { get { return Convert.ToInt32(lblDLAppID.Text); } }
        public int ApplicationID { get { return ctrlApplicationBasicInfo1.ApplicationID; } }
        public int PersonID { get { return ctrlApplicationBasicInfo1.PersonID; } }
        private void ctrlDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlApplicationBasicInfo1.OnEditPerson += CtrlApplicationBasicInfo1_OnEditPerson;
        }

        private void CtrlApplicationBasicInfo1_OnEditPerson()
        {
            OnEditPerson?.Invoke();
        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
