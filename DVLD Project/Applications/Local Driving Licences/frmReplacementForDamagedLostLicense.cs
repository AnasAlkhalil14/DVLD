using DVLD.Licenses;
using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_Licences
{
    public partial class frmReplacementForDamagedLostLicense : Form
    {
        public frmReplacementForDamagedLostLicense()
        {
            InitializeComponent();
        }
        int AppType;
        int OldLicenseID;
        int ReplacementLicenseID;
        private void frmReplacementForDamagedLostLicense_Load(object sender, EventArgs e)
        {
            lnkLicenseHistory.Enabled = false;
            lnkLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
            AppType = 4;
            lblApplicationFees.Text=clsApplication.ApplicationFees(AppType).ToString();
            lblAppDate.Text=DateTime.Now.ToShortDateString();
            lblCreatedBy.Text=clsGlobal.User.UserName;
            ctrlLDLicenseWithFilter1.OnFind += CtrlLDLicenseWithFilter1_OnFind;
        }


        void SettingOnFind()
        {
            lblApplicationID.Text = "[???]";
            lblReplacedLicenseID.Text = "[???]";
            lblOldLicenseID.Text=OldLicenseID.ToString();
            lnkLicenseHistory.Enabled = true;
            lnkLicenseInfo.Enabled = false;

            if (!ctrlLDLicenseWithFilter1.IsActive)
            {
                MessageBox.Show("Selected License is not active, choose an active license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
               btnIssue.Enabled= false;
              
                return;
            }
            btnIssue.Enabled= true;



        }
        private void CtrlLDLicenseWithFilter1_OnFind(int obj)
        {
          OldLicenseID = obj;
            SettingOnFind();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            if(rbLostLicense.Checked)
            {
                AppType = 3;
                lblApplicationFees.Text=clsApplication.ApplicationFees(AppType).ToString();
                lblAppKind.Text = "Replacement For Lost License";
            }

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                AppType = 4;
                lblApplicationFees.Text = clsApplication.ApplicationFees(AppType).ToString();

                lblAppKind.Text = "Replacement For Damaged License";
            }
        }

        private void lnkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID=clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.OnEditPerson += Frm_OnEditPerson;
                frm.ShowDialog();
        }

        private void Frm_OnEditPerson()
        {
            ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.LoadData(OldLicenseID);        }

        int AddApplication()
        {
            clsApplication application = new clsApplication();
            application.ApplicationStatus = 3;
            application.ApplicantPersonID = clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            application.ApplicationDate = DateTime.Now;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationTypeID = AppType;
            application.CreatedByUserID=clsGlobal.User.UserID;
            application.Add();
            lblApplicationID.Text=application.ApplicationID.ToString();
            return application.ApplicationID;
        }
        bool IssueReplacementLicense()
        {
            clsLicense license = new clsLicense();
            license.ApplicationID = AddApplication();
            if (license.ApplicationID == -1)
            {
                return false;
            }

            license.IsActive = true;
            license.LicenseClass = clsLicense.LicenseClassIDByLicenseID(OldLicenseID);
            license.DriverID = ctrlLDLicenseWithFilter1.DriverID;
            license.IssueDate = ctrlLDLicenseWithFilter1.IssueDate;
            license.Notes = ctrlLDLicenseWithFilter1.Notes;
            license.PaidFees = clsLicenseClass.ClassFees(license.LicenseClass);
            license.IssueReason = rbDamagedLicense.Checked ? 3 : 4;
            license.CreatedByUserID = clsGlobal.User.UserID;
            license.Add();
            ReplacementLicenseID = license.LicenseID;
            if (ReplacementLicenseID != -1)
            {
                if (clsLicense.ChangeLicenseIsActive(OldLicenseID, false))
                { lblReplacedLicenseID.Text = ReplacementLicenseID.ToString();
            return true; }
            }

            return false;
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to issue a replacement for this license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                if(IssueReplacementLicense())
                {
                    lnkLicenseInfo.Enabled = true;
                    MessageBox.Show("License replaced successfully with id=" + ReplacementLicenseID, "License issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("License replaced failed.", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lnkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm=new frmLicenseInfo(ReplacementLicenseID);
            frm.ShowDialog();
        }
    }
}
