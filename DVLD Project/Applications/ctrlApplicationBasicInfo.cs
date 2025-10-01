using DVLD.People;
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
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }
        clsApplication Application;
        public event Action OnEditPerson;
        public void LoadDataApplicationInfo(int AppID)
        {
            Application=clsApplication.Find(AppID);
            if (Application == null) return;
            lblAppID.Text= AppID.ToString();
            lblCreatedBy.Text = Properties.Settings.Default.UserName;
            lblDate.Text=Application.ApplicationDate.ToShortDateString();
            lblFees.Text=Application.PaidFees.ToString();
            lblFullName.Text = clsPerson.FullName(Application.ApplicantPersonID);
            switch (Application.ApplicationStatus)
            {
                case 1:
                    {
                        lblStatus.Tag = 1;
                        lblStatus.Text = "New";
                        break;
                    }
                case 2:
                    {
                        lblStatus.Tag = 2;
                        lblStatus.Text = "Cancelled";
                        break;
                    }
                case 3:
                    {
                        lblStatus.Tag = 3;
                        lblStatus.Text = "Completed";
                    break ;
                    }
            }
            lblStatusDate.Text = Application.LastStatusDate.ToShortDateString();
            lblType.Text = Application.ApplicationTypeString;

        }
       
        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {

        }

        private void lnkViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmPersonDetails frm = new frmPersonDetails(Application.ApplicantPersonID);
            frm.ctrlPersonInfo1.OnSave += CtrlPersonInfo1_OnSave;
            frm.ShowDialog();
        }

        private void CtrlPersonInfo1_OnSave()
        {
            lblFullName.Text=clsPerson.FullName(Application.ApplicantPersonID);
            OnEditPerson?.Invoke();
        }

        public int ApplicationID
        {
            get { return Application.ApplicationID; }
        }
        public int PersonID { get { return Application.ApplicantPersonID; } }
      

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
