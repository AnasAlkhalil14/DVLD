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

namespace DVLD.Applications.Local_Driving_Licences
{
    public partial class frmRenewLDLicense : Form
    {
        public frmRenewLDLicense()
        {
            InitializeComponent();
        }

        int OldLicenseID;
        int RenewLicenseID;
        int LicenseClassID;
        void FormSettingOnLoad()
        {
            lnkLicenseHistory.Enabled = false;
            lnkLicenseInfo.Enabled = false;
            btnRenew.Enabled = false;

            lblAppDate.Text=DateTime.Now.ToShortDateString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = clsApplication.ApplicationFees(2).ToString();
            lblCreatedBy.Text = clsGlobal.User.UserName;
        }

        private void frmRenewLDLicense_Load(object sender, EventArgs e)
        {

            FormSettingOnLoad();

            ctrlLDLicenseWithFilter1.OnFind += CtrlLDLicenseWithFilter1_OnFind;


        }
        bool ChickIfCanRenewOnDate()
        {
            return DateTime.Now > ctrlLDLicenseWithFilter1.ExpirationDate;
        }

       void OnFindSetting()
        {
            lblApplicationID.Text = "[???]";
            lblRenewLicenseID.Text = "[???]";

              LicenseClassID = clsLicense.LicenseClassIDByLicenseID(OldLicenseID);
            lblOldLicenseID.Text = OldLicenseID.ToString();
            lnkLicenseHistory.Enabled = true;
            lblExpirationDate.Text = DateTime.Now.AddYears(clsLicenseClass.DefaultValidityLength(LicenseClassID)).ToShortDateString();
            lblLicenseFees.Text = clsLicenseClass.ClassFees(LicenseClassID).ToString();
            lblTotalFees.Text = (Convert.ToDouble(lblLicenseFees.Text) + Convert.ToDouble(lblApplicationFees.Text)).ToString();
            lnkLicenseInfo.Enabled = false;

        }


        int AddApplication()
        {
clsApplication application = new clsApplication();
            application.ApplicationStatus = 3;
            application.ApplicationDate = DateTime.Now;
            application.ApplicantPersonID = clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
application.LastStatusDate = DateTime.Now;
            application.ApplicationTypeID = 2;
            application.CreatedByUserID=clsGlobal.User.UserID;
            application.Add();
          if(application.ApplicationID!=-1)
            {
                lblApplicationID.Text = application.ApplicationID.ToString();
            }
            return application.ApplicationID;
        }

        bool RenewLicense()
        {
            clsLicense license = new clsLicense();
            license.ApplicationID=AddApplication();
            if (license.ApplicationID == -1) return false;

            license.Notes=txtNotes.Text.Trim();
            license.LicenseClass = LicenseClassID;
            license.CreatedByUserID= clsGlobal.User.UserID;
            license.DriverID = ctrlLDLicenseWithFilter1.DriverID;
            license.PaidFees =clsLicenseClass.ClassFees(LicenseClassID);
            license.IssueReason = 2;
            license.IsActive=true;
            license.IssueDate = DateTime.Now;
        license.Add();
            if(license.LicenseID==-1||!clsLicense.ChangeLicenseIsActive(OldLicenseID,false))
            {
                return false;
            
            }

            lblRenewLicenseID.Text = license.LicenseID.ToString();
           RenewLicenseID=license.LicenseID;
            return true;

        }


        void ChickLicenseOnFind()
        {
            if (ChickIfCanRenewOnDate())
            {
                if (ctrlLDLicenseWithFilter1.IsActive)
                {

                    btnRenew.Enabled = true;

                }
                else
                {
                    btnRenew.Enabled = false;
                    MessageBox.Show("Selected License is not Active,choose an active one ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                btnRenew.Enabled = false;

                MessageBox.Show("Selected License is not yet expire,it will expire on:" + ctrlLDLicenseWithFilter1.ExpirationDate.ToShortDateString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void CtrlLDLicenseWithFilter1_OnFind(int obj)
        {
            OldLicenseID = obj;

            OnFindSetting();
            ChickLicenseOnFind();


        }

        private void lnkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            int PersonID = clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.OnEditPerson += Frm_OnEditPerson;
            frm.ShowDialog();
        }

        private void Frm_OnEditPerson()
        {
            ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.LoadData(OldLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlLDLicenseWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void btnRenew_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("Are you sure you want to renew this license?","Confirm:",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {

                if(RenewLicense())
                {
                    ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.lblIsActive.Text = "False";
                    btnRenew.Enabled = false;
                    lnkLicenseInfo.Enabled = true;
                    MessageBox.Show("License Renewed Successfully with id=" + RenewLicenseID, "result:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("License Renewed failed ", "result:", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }


            }
            

        }

        private void lnkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(RenewLicenseID);
            frm.ShowDialog();
        }
    }
}
