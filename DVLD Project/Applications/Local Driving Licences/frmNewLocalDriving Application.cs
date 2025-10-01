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
            Mode = enMode.eAdd;

        }
        public frmNewLocalDriving_Application(int LDLAppID)
        {
            InitializeComponent();
            this.LDLAppID = LDLAppID;
            Mode = enMode.eUpdate;



        }
        int LDLAppID;
        clsApplication Application;
        clsLocalDrivingLicenseApplications LocalDrivingLicenseApplications;

        enum enMode { eAdd,eUpdate}
        enMode Mode = enMode.eAdd;

        public event Action OnSave;


        private void SettingsOnLoadForUpdate()
        {


            Application = clsApplication.Find(clsLocalDrivingLicenseApplications.AppID(LDLAppID));
            lblMode.Text = "Update";
            lblApplicationDate.Text = Application.ApplicationDate.ToShortDateString();
            cbLicenceClass.SelectedValue =clsLicenseClass.ClassIDByLDLAppID(LDLAppID);
            int PersonID = clsLocalDrivingLicenseApplications.PersonIDByLocalDLAppID(LDLAppID);
            ctrlFilterPerson1.ctrlPersonInfo1.LoadPersonInfo(PersonID);
            ctrlFilterPerson1.gbFilters.Enabled = false;
            ctrlFilterPerson1.cbFilterBy.SelectedIndex = 1;
            ctrlFilterPerson1.txtFilterValue.Text = PersonID.ToString();


        }


        private void SettingsOnLoadForAdd()
        {
            Application = new clsApplication();
            LocalDrivingLicenseApplications = new clsLocalDrivingLicenseApplications();
            lblMode.Text = "   New";

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
           
            cbLicenceClass.SelectedValue = 3;

            Application.ApplicationTypeID = 1;
            Application.ApplicationDate = DateTime.Now;
            Application.LastStatusDate = DateTime.Now;
            Application.CreatedByUserID = clsGlobal.User.UserID;
            Application.ApplicationStatus = 1;

          

        }
        private void frmNewLocalDriving_Application_Load(object sender, EventArgs e)
        {
            cbLicenceClass.DataSource = clsLocalDrivingLicenseApplications.GetAllLicenceClasses();
            cbLicenceClass.DisplayMember = "ClassName";
            cbLicenceClass.ValueMember = "LicenseClassID";
            if (Mode==enMode.eUpdate) SettingsOnLoadForUpdate();
            else SettingsOnLoadForAdd();




            ctrlFilterPerson1.OnFind += CtrlFilterPerson1_OnFind;


         
            lblApplicationFees.Text = Application.PaidFees.ToString();
            lblCreatedBy.Text = clsGlobal.User.UserName;


            ctrlFilterPerson1.ctrlPersonInfo1.OnSave += CtrlPersonInfo1_OnSave;

        }

        private void CtrlPersonInfo1_OnSave()
        {
            OnSave?.Invoke();
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
            switch (Mode)
            {
                case enMode.eAdd:
                    {
                        if (CheckEveryThingBeforeSave())
                        {
                            if (Application.Add())
                            {
                                LocalDrivingLicenseApplications.ApplicationID = Application.ApplicationID;
                                LocalDrivingLicenseApplications.LicenseClassID = Convert.ToInt32(cbLicenceClass.SelectedValue.ToString());
                                if (LocalDrivingLicenseApplications.Add())
                                {
                                    OnSave?.Invoke();

                                    MessageBox.Show("Local Driving License Application added successfully with id=" + LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID + " for person with id=" + Application.ApplicantPersonID, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        break;
                    }
                case enMode.eUpdate:
                    {
                         if(clsLocalDrivingLicenseApplications.Update(LDLAppID, Convert.ToInt32(cbLicenceClass.SelectedValue.ToString())))
                        {
                            MessageBox.Show("Local Driving License Application Updated successfully with id=" + LDLAppID + " for person with id=" + Application.ApplicantPersonID, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            OnSave?.Invoke();

                        }
                        else
                        {
                            MessageBox.Show("Some thing wrong happen call the help center", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                        break;
                    }

            }

           
           

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcApplicationInfo.SelectedIndex = 1;
        }

        

       
    }
}
