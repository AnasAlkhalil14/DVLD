using DVLD.Applications.Tests;
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
            if (dgvLocalLicenseApplicationsList.SelectedRows.Count <= 0) return; 
            string AppState = dgvLocalLicenseApplicationsList.CurrentRow.Cells["Status"].Value.ToString();
            if(AppState.ToLower() =="cancelled")
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = false;
                tsmScheduleStreetTest.Enabled = false;
                return;
            }
            int PassedTestCount = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["PassedTestCount"].Value);
             if(PassedTestCount== 0)
            {
                tsmScheduleVisionTest.Enabled=true;
                tsmScheduleWrittenTest.Enabled=false;
                tsmScheduleStreetTest.Enabled=false;
                return;
            }
            if (PassedTestCount == 1)
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = true;
                tsmScheduleStreetTest.Enabled = false;

                return;
              }
            if (PassedTestCount == 2)
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = false;
                tsmScheduleStreetTest.Enabled = true ;
            }
            if (PassedTestCount == 3)
            {
                tsmScheduleVisionTest.Enabled = false;
                tsmScheduleWrittenTest.Enabled = false;
                tsmScheduleStreetTest.Enabled = false;
            }




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
            string Status= dgvLocalLicenseApplicationsList.CurrentRow.Cells["Status"].Value.ToString();
            if(Status!="New")
            {
                MessageBox.Show("Status is "+ Status+" so it can not be cancelled","Worng",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;

            }


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

        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLAppID = Convert.ToInt32(dgvLocalLicenseApplicationsList.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);
            frmVisionTestAppointment frm = new frmVisionTestAppointment(LDLAppID);
            frm.ShowDialog();

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            SetEnaplityForTests();

        }
    }
}
