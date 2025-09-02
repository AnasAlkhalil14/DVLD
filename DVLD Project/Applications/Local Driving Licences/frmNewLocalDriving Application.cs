using DVLD.People;
using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_Driving_Licences
{
    public partial class frmNewLocalDriving_Application : Form
    {
        
        public frmNewLocalDriving_Application()
        {
            InitializeComponent();
            Application=new clsApplication();
            LocalDrivingLicenseApplications=new clsLocalDrivingLicenseApplications();
        }
        clsApplication Application;
        clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications;

        public event Action OnSave;

        private void tpApplicationInfo_Click(object sender, EventArgs e)
        {

        }

        private void tcApplicationInfo_Enter(object sender, EventArgs e)
        {
            
        }
        private void FormInfoAndSettingsOnLoad()
        {

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            cbLicenceClass.DataSource = clsLocalDrivingLicenseApplications.GetAllLicenceClasses();
            cbLicenceClass.DisplayMember = "ClassName";
            cbLicenceClass.ValueMember = "LicenseClassID";
            cbLicenceClass.SelectedValue = 3;

            Application.ApplicationTypeID = 1;
            Application.ApplicationDate = DateTime.Now;
            Application.LastStatusDate = DateTime.Now;
            Application.CreatedByUserID = clsGlobal.User.UserID;
            Application.ApplicationStatus = 1;

            lblApplicationFees.Text = Application.PaidFees.ToString();
            lblCreatedBy.Text = clsGlobal.User.UserName;

        }
        private void frmNewLocalDriving_Application_Load(object sender, EventArgs e)
        {
            ctrlFilterPerson1.OnFind += CtrlFilterPerson1_OnFind;

            FormInfoAndSettingsOnLoad();
        }

        private void CtrlFilterPerson1_OnFind()
        {
           Application.ApplicantPersonID=ctrlFilterPerson1.PersonID;
           
        }
        bool CheckEveryThingBeforeSave()
        {
            if(Application.ApplicantPersonID<=0)
            {
                MessageBox.Show("You must choose the person who apply this Application First.","Wrong",MessageBoxButtons.OK,MessageBoxIcon.Error);  
                return false;
            }
            if(!clsLocalDrivingLicenseApplications.CheckIfPersonCanApplyNewLocalLicenceApplication(Application.ApplicantPersonID, Convert.ToInt32(cbLicenceClass.SelectedValue.ToString())))
            {
                MessageBox.Show("Person with id=" + Application.ApplicantPersonID + " can not apply for a new local licence of this class because he has a complited application or new application already", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            return  true;



        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(CheckEveryThingBeforeSave())
            {
                if(Application.Add())
                {
                    LocalDrivingLicenseApplications.ApplicationID = Application.ApplicationID;
                    LocalDrivingLicenseApplications.LicenseClassID = Convert.ToInt32(cbLicenceClass.SelectedValue.ToString());
                    if(LocalDrivingLicenseApplications.Add())
                    {
                        OnSave?.Invoke();

                        MessageBox.Show("Local Driving Licence Application added successfully with id=" + LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID + " for person with id=" + Application.ApplicantPersonID, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     }
                    else
                    {
                        MessageBox.Show("Some thing wrong happen call the help center", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Some thing wrong happen call the help center", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              


            }
           

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcApplicationInfo.SelectedIndex = 1;
        }

        private void cbLicenceClass_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
