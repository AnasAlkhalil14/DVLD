using DVLD.Licenses;
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

namespace DVLD.Applications.Detain_Release_License
{
    public partial class frmReleaseDetainedLicense : Form
    {
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();
            Mode = enMode.eForGeneral;
        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
          
        }

        enum enMode { eForSpecificLicense,eForGeneral}
        enMode Mode;
        int LicenseID;
        clsDetainedLicense detainedLicense;
        public event Action OnSave;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

            lblApplicationFees.Text = clsApplication.ApplicationFees(5).ToString();
            ctrlLDLicenseWithFilter1.OnFind += CtrlLDLicenseWithFilter1_OnFind;
            if (Mode == enMode.eForSpecificLicense)
            {
                Mode = enMode.eForSpecificLicense;
                ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.LoadData(LicenseID);
                ctrlLDLicenseWithFilter1.gbFilter.Enabled = false;
                ctrlLDLicenseWithFilter1.txtLicenseID.Text = LicenseID.ToString();
                CtrlLDLicenseWithFilter1_OnFind(LicenseID);

            }
            else
            {
                btnRelease.Enabled = false;
            }
                
        }

        void OnFindCheck()
        {
            lblApplicationID.Text = "[???]";

            if (!ctrlLDLicenseWithFilter1.IsDetain)
            {
                btnRelease.Enabled=false;
                lblFineFees.Text = "[???]";
                lblDetainDate.Text = "[???]";
                lblDetainID.Text = "[???]";
                lblTotalFees.Text = "[???]";
                lblCreatedBy.Text = "[???]";
                MessageBox.Show("Selected License is not detained,choose a detained one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            btnRelease.Enabled = true;

            detainedLicense = clsDetainedLicense.FindDetainLicense(LicenseID);
            clsUser User = clsUser.Find(detainedLicense.CreatedByUserID);
            lblCreatedBy.Text = User.UserName;
            lblDetainDate.Text = detainedLicense.DetainDate.ToShortDateString();
            lblFineFees.Text=detainedLicense.FineFees.ToString();
            lblDetainID.Text=detainedLicense.DetainID.ToString();
            lblTotalFees.Text=(detainedLicense.FineFees+Convert.ToDouble(lblApplicationFees.Text)).ToString();
        }
        private void CtrlLDLicenseWithFilter1_OnFind(int obj)
        {
            LicenseID = obj;
            lblLicenseID.Text = LicenseID.ToString();
            lnkLicenseHistory.Enabled= true;
            lnkLicenseInfo.Enabled= true;

            OnFindCheck();
        }

        private void lnkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm=new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void lnkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID=clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            frmLicenseHistory frm=new frmLicenseHistory(PersonID);
            frm.OnEditPerson += Frm_OnEditPerson;
        }

        private void Frm_OnEditPerson()
        {
ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.LoadData(LicenseID);
        }

        private void ctrlLDLicenseWithFilter1_Load(object sender, EventArgs e)
        {

        }

        int AddApplication()
        {
            clsApplication application = new clsApplication();
            application.ApplicationStatus = 3;
            application.ApplicantPersonID = clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            application.ApplicationDate=DateTime.Now;
            application.LastStatusDate=DateTime.Now;
            application.ApplicationTypeID = 5;
            application.CreatedByUserID = clsGlobal.User.UserID;
            application.Add();
            return application.ApplicationID;
        }
        void RenewLicense()
        {
           detainedLicense.ReleaseApplicationID=AddApplication();
            if(detainedLicense.ReleaseApplicationID==-1)
            {
                MessageBox.Show("Failed to add the application","Wrong",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            detainedLicense.ReleasedByUserID=clsGlobal.User.UserID;
            detainedLicense.ReleaseDate=DateTime.Now;
            detainedLicense.IsReleased=true;
            if (detainedLicense.ReleaseDetainLicense())
            {
                OnSave?.Invoke();
                lblApplicationID.Text=detainedLicense.ReleaseApplicationID.ToString();
                btnRelease.Enabled=false;
                ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.lblIsDetained.Text = "False";
            MessageBox.Show("License Released Successfully", "Result", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("License Released failed", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }
        
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to renew this license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                RenewLicense();
            }

        }
    }
}
