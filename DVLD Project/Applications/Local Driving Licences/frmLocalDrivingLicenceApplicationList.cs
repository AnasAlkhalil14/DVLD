using DVLD.Applications.Tests;
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
    public partial class frmLocalDrivingLicenceApplicationList : Form
    {
        public frmLocalDrivingLicenceApplicationList()
        {
            InitializeComponent();
        }
        DataTable ForClone;
        enum enFilter {
            None,LDLAppID,NationalNo,FullName,Status
        }
        enFilter Filter;
        private void button1_Click(object sender, EventArgs e)
        {
            frmNewLocalDriving_Application frm=new frmNewLocalDriving_Application();
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }

        private void Frm_OnSave()
        {
          RefreshForm();
        }
        void SetEnaplityForTests()
        {
           
            int PassedTestCount = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["PassedTestCount"].Value);
             if(PassedTestCount== 0)
            {
                tsmEditApplication.Enabled = true;

                tsmScheduleVisionTest.Enabled=true;
                tsmScheduleWrittenTest.Enabled=false;
                tsmScheduleStreetTest.Enabled=false;
                int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
                if (clsTestAppointment.AllTestAppointment(LDLAppID, 1).Rows.Count == 0)
                {
                    tsmDeleteApplication.Enabled = true;
                }
                else
                {
                    
                    tsmDeleteApplication.Enabled = false;
                }
                tsmIssueDrivingLicense.Enabled = false;
                



                    return;
            }
            tsmEditApplication.Enabled = false;
            tsmDeleteApplication.Enabled = false;
            if (PassedTestCount == 1)
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = true;
                tsmScheduleStreetTest.Enabled = false;

                tsmIssueDrivingLicense.Enabled = false;
               
                return;
              }
            if (PassedTestCount == 2)
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = false;
                tsmScheduleStreetTest.Enabled = true ;

                tsmIssueDrivingLicense.Enabled = false;
              
            }
            if (PassedTestCount == 3)
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = false;
                tsmScheduleStreetTest.Enabled = false;
                tsmScheduleTest.Enabled = false;
                tsmIssueDrivingLicense.Enabled = true;
               

            }




        }
        void SetEnaplityForCanceleCase()
        {
             tsmCanceleApplication.Enabled= false;
            tsmDeleteApplication.Enabled= false;
            tsmEditApplication.Enabled= false;
            tsmIssueDrivingLicense.Enabled= false;
            tsmScheduleTest.Enabled = false;
            tsmShowLicense.Enabled = false;



        }
        void SetEnaplityForCompletedCase()
        {
            tsmCanceleApplication.Enabled = false;
            tsmDeleteApplication.Enabled = false;
            tsmEditApplication.Enabled = false;
            tsmIssueDrivingLicense.Enabled = false;
            tsmScheduleTest.Enabled = false;
            tsmShowLicense.Enabled = true;



        }
        void SetEnaplityForNewCase()
        {
            tsmScheduleTest.Enabled = true;
            tsmCanceleApplication.Enabled = true;

            tsmShowLicense.Enabled = false;
            SetEnaplityForTests();

        }
        void SetEnaplityForMenueItems()
        {

            if (dgvLocalLicenseApplicationsList.SelectedRows.Count <= 0) return;

            string AppState = dgvLocalLicenseApplicationsList.CurrentRow.Cells["Status"].Value.ToString();
            if (AppState == "Cancelled") { SetEnaplityForCanceleCase(); return; }
            if (AppState == "Completed") { SetEnaplityForCompletedCase();return; }


            SetEnaplityForNewCase();






        }


        void RefreshForm()
        {
            DataTable AllLocalLicenseApplication = clsLocalDrivingLicenseApplications.GetAllLocalLicenseApplication();
            ForClone=AllLocalLicenseApplication.Clone();
            dgvLocalLicenseApplicationsList.DataSource = AllLocalLicenseApplication;
            
            lblRecordsCount.Text=dgvLocalLicenseApplicationsList.Rows.Count.ToString(); 
        }
        private void frmLocalDrivingLicenceApplicationList_Load(object sender, EventArgs e)
        {
            RefreshForm();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            Filter=(enFilter)cbFilter.SelectedIndex;
            if(Filter==enFilter.None)
            {
           
                txtFilter.Visible=false;
                cbStatus.Visible=false;

            }
            else if(Filter==enFilter.Status)
            {
                txtFilter.Visible = false;
                cbStatus.Visible=true;
            }
            else
            {
                txtFilter.Visible = true;
                cbStatus.Visible=false;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
            }

                RefreshForm();
        }
        void DoFilter()
        {
            switch (Filter)
            {
                case enFilter.LDLAppID:
                    {
                        dgvLocalLicenseApplicationsList.DataSource=clsLocalDrivingLicenseApplications.FilterByLDLAppID(Convert.ToInt32(txtFilter.Text)); 
                        break;
                    }
                case enFilter.FullName:
                    {

                        dgvLocalLicenseApplicationsList.DataSource = clsLocalDrivingLicenseApplications.FilterByFullName(txtFilter.Text.Trim());
                        break;

                    }
                case enFilter.Status:
                    {

                        dgvLocalLicenseApplicationsList.DataSource = clsLocalDrivingLicenseApplications.FilterByStatus(cbStatus.Text.Trim());
                        break;



                    }
                case enFilter.NationalNo:
                    {
                        dgvLocalLicenseApplicationsList.DataSource = clsLocalDrivingLicenseApplications.FilterByNationalNo(txtFilter.Text.Trim());
                        break;
                    }



            } 
                
           if(dgvLocalLicenseApplicationsList.Rows.Count==0)
            {
                dgvLocalLicenseApplicationsList.DataSource=ForClone;
            }

           lblRecordsCount.Text=dgvLocalLicenseApplicationsList.Rows.Count.ToString();



        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                RefreshForm();
                return;
            }
            DoFilter();

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(Filter==enFilter.LDLAppID)
            {
                if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }

            }



        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoFilter();
        }

        private void canceleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


            int LDLApplicationID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            if (MessageBox.Show("Are You Sure You Want to Cancel Application with LDLApplicationID=" + LDLApplicationID + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(clsLocalDrivingLicenseApplications.CanceleApplication(LDLApplicationID))
                {
                    MessageBox.Show("Canceled Successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshForm();
                }
                else
                {
                    MessageBox.Show("Canceled Failed", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
                    
         }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sechToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

            frmNewLocalDriving_Application frm = new frmNewLocalDriving_Application(LDLAppID);
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }

        

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            frmTestAppointments frm = new frmTestAppointments(LDLAppID,1);
            frm.OnPass += Frm_OnPass;
            frm.OnEditPerson += Frm_OnEditPerson;
            frm.ShowDialog();

        }

        private void Frm_OnPass()
        {
           RefreshForm();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            frmTestAppointments frm = new frmTestAppointments(LDLAppID,2);
            frm.OnPass += Frm_OnPass;
            frm.OnEditPerson += Frm_OnEditPerson;

            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            SetEnaplityForMenueItems();
 
        }

        private void tsmScheduleStreetTest_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            frmTestAppointments frm = new frmTestAppointments(LDLAppID, 3);
            frm.OnPass += Frm_OnPass;
            frm.OnEditPerson += Frm_OnEditPerson;

            frm.ShowDialog();
        }

        private void tsmIssueDrivingLicense_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

            frmIssueDrivingLicenseForFirstTime frm = new frmIssueDrivingLicenseForFirstTime(LDLAppID);
            frm.OnSave += Frm_OnIssueLicense;
            frm.ShowDialog();

        }
        private void Frm_OnIssueLicense(int LDLAppID)
        {
             clsLocalDrivingLicenseApplications.MakeApplicationComplited(LDLAppID);
            RefreshForm();
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

            int LicenseID =clsLicense.GetLicenseIDByAppID(clsLocalDrivingLicenseApplications.AppID(LDLAppID));
           frmLicenseInfo frm=new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void tsmShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

            frmLDLApplicationDetails frm = new frmLDLApplicationDetails(LDLAppID);
            frm.OnEditPerson += Frm_OnEditPerson;
            frm.ShowDialog();
        }

        private void Frm_OnEditPerson()
        {
           RefreshForm();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
int PersonID=clsLocalDrivingLicenseApplications.PersonIDByLocalDLAppID(LDLAppID);
            frmLicenseHistory frm=new frmLicenseHistory(PersonID);
            frm.OnEditPerson += Frm_OnEditPerson;
            frm.ShowDialog();
        }

        private void tsmDeleteApplication_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this L.D.L.App?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {

                int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                if (clsLocalDrivingLicenseApplications.Delete(LDLAppID))
                {
                    MessageBox.Show("Done Successfully", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshForm();
                }
                else
                {
                    MessageBox.Show("Some thing wrong happen,may be this Application has data link to it", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }




            }




        }
    }
}
