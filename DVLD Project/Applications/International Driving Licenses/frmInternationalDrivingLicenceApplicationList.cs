using DVLD.Licenses;
using DVLD.People;
using DVLD_Business_Tier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_Driving_Licenses
{
    public partial class frmInternationalDrivingLicenceApplicationList : Form
    {
        public frmInternationalDrivingLicenceApplicationList()
        {
            InitializeComponent();
        }
        enum enFilter { eNone, eInternationalLicenseID, eApplicationID, eDriverID , eLocalLicenseID, eIsActive }
        enum enIsActive { eAll,eYes,eNo}
        enFilter Filter;
enIsActive IsActive;



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInternationalDrivingLicenceApplicationList_Load(object sender, EventArgs e)
        {
            RefreshFrom();
        }

        void DoFilterOnTxtFilter()
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                DownLoadAllApp();
                return;
            }


            DataTable Clone=((DataTable)dgvInternationalLicenseApplicationsList.DataSource).Clone();
            int FilterBy=Convert.ToInt32(txtFilter.Text);
            switch(Filter)
            {
                case enFilter.eInternationalLicenseID:
                    {
                        dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetByInternationalLicenseID(FilterBy);
                       lblRecordsCount.Text=dgvInternationalLicenseApplicationsList.Rows.Count.ToString();
                        break;

                    }
                    case enFilter.eApplicationID:
                    {
                        dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetByApplicationID(FilterBy);
                        lblRecordsCount.Text = dgvInternationalLicenseApplicationsList.Rows.Count.ToString();

                        break
                                       ;
                    }
                case enFilter.eDriverID:
                    {
                        dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetByDriverID(FilterBy);
                        lblRecordsCount.Text = dgvInternationalLicenseApplicationsList.Rows.Count.ToString();
                        break
                                                        ;
                    }
                case enFilter.eLocalLicenseID:
                    {
                        dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetByLocalLicenseID(FilterBy);
                        lblRecordsCount.Text = dgvInternationalLicenseApplicationsList.Rows.Count.ToString();
                        break
                                                           ;
                    }

                default:
                    {
                        RefreshFrom();
                        return;
                    }
            }
            if(dgvInternationalLicenseApplicationsList.Rows.Count==0)
            {
                dgvInternationalLicenseApplicationsList.DataSource = Clone;

            }



        }
        


        void DownLoadAllApp()
        {
            dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetAllInternationalLicenseApplications();
            lblRecordsCount.Text = dgvInternationalLicenseApplicationsList.Rows.Count.ToString();
        }
        void RefreshFrom()
        {
            DownLoadAllApp();
            cbIsActive.Visible = false;
            txtFilter.Visible = false;
            txtFilter.Clear();
            cbIsActive.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;
        }
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
             Filter=(enFilter)cbFilter.SelectedIndex;
            if (Filter==enFilter.eNone)
            {
                RefreshFrom();
                return;
            }

            if(Filter==enFilter.eIsActive)
            {
                cbIsActive.SelectedIndex = (int)enIsActive.eAll;
                txtFilter.Visible=false;
                cbIsActive.Visible = true;
            }
            else
            {
                txtFilter.Visible=true;
                cbIsActive.Visible = false;
                txtFilter.Clear();
            }

            DownLoadAllApp();

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DoFilterOnTxtFilter();
        }
        void DoFilterCbIsActive()
        {
            DataTable Clone = ((DataTable)dgvInternationalLicenseApplicationsList.DataSource).Clone();

            IsActive = (enIsActive)cbIsActive.SelectedIndex;
            switch (IsActive)
            {
                case enIsActive.eAll:
                    {
                        DownLoadAllApp();
                        return;
                    }
                case enIsActive.eYes:
                    {
                        dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetByIsActive(true);
                        lblRecordsCount.Text = dgvInternationalLicenseApplicationsList.Rows.Count.ToString();
                        break;
                    }
                case enIsActive.eNo:
                    {
                        dgvInternationalLicenseApplicationsList.DataSource = clsInternationalDrivingLicenseApplications.GetByIsActive(false);
                        lblRecordsCount.Text = dgvInternationalLicenseApplicationsList.Rows.Count.ToString();
                        break;
                    }
                default:
                    {
                        DownLoadAllApp();
                        return;
                    }


            }
            if (dgvInternationalLicenseApplicationsList.Rows.Count == 0)
            {
                dgvInternationalLicenseApplicationsList.DataSource = Clone;

            }
        }
        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            DoFilterCbIsActive();

        }

        private void tsmShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenseApplicationsList.CurrentRow.Cells["DriverID"].Value);
           int PersonID=clsDriver.GetPersonIDByDriverID(DriverID);
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ShowDialog();

        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int DriverID = Convert.ToInt32(dgvInternationalLicenseApplicationsList.CurrentRow.Cells["DriverID"].Value);
            int PersonID = clsDriver.GetPersonIDByDriverID(DriverID);
           frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            int IntLecenseID = Convert.ToInt32(dgvInternationalLicenseApplicationsList.CurrentRow.Cells["Int.License ID"].Value);
            frmIntDLApplicationDetails frm = new frmIntDLApplicationDetails(IntLecenseID);
            frm.ShowDialog();
        }
        
        private void btnAddApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalDrivingApplication frm=new frmNewInternationalDrivingApplication();
            frm.ShowDialog();
        }
    }
}
