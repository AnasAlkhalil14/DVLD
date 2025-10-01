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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Applications.International_Driving_Licenses
{
    public partial class frmNewInternationalDrivingApplication : Form
    {
        public frmNewInternationalDrivingApplication()
        {
            InitializeComponent();
        }
        int LicenseID;
        int intLicenseID;
        private void frmNewInternationalDrivingApplication_Load(object sender, EventArgs e)
        {
            ApplicationDefualtSetting();
            ctrlLDLicenseWithFilter1.OnFind += CtrlLDLicenseWithFilter1_OnFind;
        }
        void ToDoWhenFindLicense()
        {
            lnkLicenseHistory.Enabled = true;

            int LicenseClass=clsLicense.LicenseClassIDByLicenseID(LicenseID);
            if(LicenseClass != 3)
            {
                lblApplicationID.Text = "[???]";
                lblInternationalLicenseID.Text = "[???]";
                lblLocalLicenseID.Text = LicenseID.ToString();
                lnkLicenseInfo.Enabled = false;
                btnIssueLicense.Enabled = false;
                MessageBox.Show("Selected License class should be 3,{ordinary class license}", "Wrong Class", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            
            intLicenseID=clsInternationalDrivingLicenseApplications.InternationalActiveLicenseID(ctrlLDLicenseWithFilter1.DriverID);
            if(intLicenseID!=-1)
            {
                lblApplicationID.Text = "[???]";
                lblInternationalLicenseID.Text = intLicenseID.ToString();
                lblLocalLicenseID.Text = LicenseID.ToString();

                MessageBox.Show("Person already has an active international license with id=" + intLicenseID + ".", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lnkLicenseInfo.Enabled = true;
                btnIssueLicense.Enabled = false;
                return;
            }

            lblApplicationID.Text = "[???]";
            lblInternationalLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = LicenseID.ToString();

            lblLocalLicenseID.Text = LicenseID.ToString();
            btnIssueLicense.Enabled=true;
            lnkLicenseInfo.Enabled = false;

        }

        void ApplicationDefualtSetting()
        {
            lblAppDate.Text=DateTime.Now.ToShortDateString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblExpirationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text = clsApplication.ApplicationFees(6).ToString();
            lblCreatedBy.Text = clsGlobal.User.UserName;
        }


        private void CtrlLDLicenseWithFilter1_OnFind(int obj)
        {
            LicenseID = obj;

            ToDoWhenFindLicense();
        }

        private void lnkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmIntDLApplicationDetails frm = new frmIntDLApplicationDetails(intLicenseID);
            frm.ShowDialog();
        }

        private void lnkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID=clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            
            frmLicenseHistory frm= new frmLicenseHistory(PersonID);
            frm.OnEditPerson += Frm_OnEditPerson;
            frm.ShowDialog();
        }

        private void Frm_OnEditPerson()
        {
      ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.LoadData(LicenseID);
        }

        private void ctrlLDLicenseWithFilter1_Load(object sender, EventArgs e)
        {

        }
       void IssueLicense()
        {
            
            clsApplication application = new clsApplication();
            application.ApplicationStatus = 3;
            application.ApplicationDate = DateTime.Now;
            application.LastStatusDate = DateTime.Now;
            application.ApplicationTypeID = 6;
            application.ApplicantPersonID = clsDriver.GetPersonIDByDriverID(ctrlLDLicenseWithFilter1.DriverID);
            application.CreatedByUserID = clsGlobal.User.UserID;
            if(!application.Add())
            {
                MessageBox.Show("wrong action happen ,sorry application not added.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           intLicenseID= clsInternationalDrivingLicenseApplications.Add(application.ApplicationID, ctrlLDLicenseWithFilter1.DriverID, LicenseID, DateTime.Now, DateTime.Now.AddYears(1),true, clsGlobal.User.UserID);
            if(intLicenseID!=-1)
            {
               
                btnIssueLicense.Enabled=false;
                lnkLicenseInfo.Enabled = true;
                lblApplicationID.Text=application.ApplicationID.ToString();
                lblInternationalLicenseID.Text=intLicenseID.ToString();
                lblLocalLicenseID.Text=LicenseID.ToString();
                MessageBox.Show("International license issued successfully with id=" + intLicenseID, "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("wrong action happen ,sorry License not issued.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }
        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to issue the license?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                IssueLicense();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblIssueDate_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void lblFees_Click(object sender, EventArgs e)
        {

        }

        private void lblApplicationID_Click(object sender, EventArgs e)
        {

        }

        private void lblDate_Click(object sender, EventArgs e)
        {

        }

        private void lblAppDate_Click(object sender, EventArgs e)
        {

        }

        private void lblCreatedBy_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void lblExpirationDate_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void lblLocalLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void lblInternationalLicenseID_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
