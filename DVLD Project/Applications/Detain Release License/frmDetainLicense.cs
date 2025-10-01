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
    public partial class frmDetainLicense : Form
    {
        public frmDetainLicense()
        {
            InitializeComponent();
        }

        int LicenseID;
        public event Action OnSave;
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text=DateTime.Now.ToShortDateString();
            lblCreatedBy.Text=clsGlobal.User.UserName;
            lnkLicenseInfo.Enabled = false;
            lnkLicenseHistory.Enabled = false;
            btnDetain.Enabled = false;
            ctrlLDLicenseWithFilter1.OnFind += CtrlLDLicenseWithFilter1_OnFind;
        }

        void OnFindCheck()
        {

            if (Convert.ToBoolean(!ctrlLDLicenseWithFilter1.IsActive))
            {
                btnDetain.Enabled = false;
                
                MessageBox.Show("This License is not active,Choose an active license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            if(Convert.ToBoolean(ctrlLDLicenseWithFilter1.IsDetain))
            {
                btnDetain.Enabled = false;
                MessageBox.Show("This License is already detained.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

btnDetain.Enabled = true;


        }
        private void CtrlLDLicenseWithFilter1_OnFind(int obj)
        {
            lblDetainID.Text = "[???]";
            lblLicenseID.Text=obj.ToString();
            LicenseID = obj;
            lnkLicenseHistory.Enabled = true;
            lnkLicenseInfo.Enabled = true;
            OnFindCheck();
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
ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.LoadData(LicenseID);
        }

        private void lnkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicenseInfo frm=new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar =='.')
            {if( txtFineFees.Text.Contains("."))
                {
                    e.Handled = true;
                    return;
                }
                
                return;

            }


            if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled= true;
            }
             
        }

        void DetainLicense()
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.FineFees=Convert.ToDouble(txtFineFees.Text);
            detainedLicense.DetainDate=DateTime.Now;
            detainedLicense.CreatedByUserID=clsGlobal.User.UserID;
            detainedLicense.LicenseID=Convert.ToInt32(lblLicenseID.Text);
            detainedLicense.IsReleased = false;
          if  (detainedLicense.DetainLicense())
            {
                OnSave?.Invoke();
                btnDetain.Enabled = false;
                ctrlLDLicenseWithFilter1.ctrlLicenseInfo1.lblIsDetained.Text = "True";
                lblDetainID.Text = detainedLicense.DetainID.ToString();
                MessageBox.Show("License Detained successfully with id=" + detainedLicense.DetainID, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("License detained failed", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFineFees.Text.Trim()))
            {
                MessageBox.Show("You should set the fine fees first","Not allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                errorProvider1.SetError(txtFineFees, "This field is required");

                return;

            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");


            }

            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DetainLicense();

            }

        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFineFees.Text.Trim()))
            {
                errorProvider1.SetError(txtFineFees, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtFineFees, "");


            }
        }
    }
}
