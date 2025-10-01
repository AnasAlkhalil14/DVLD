using DVLD.Licenses;
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

namespace DVLD.Applications.Detain_Release_License
{
    public partial class frmDetainedLicensesList : Form
    {
        public frmDetainedLicensesList()
        {
            InitializeComponent();
        }
        enum enIsReleas { eAll,eYes,eNo }
        enIsReleas IsRelease;
        enum enFilter { eNone, eDetainID,eIsRelease,eNationalNo,eFullName,eAppID};
        enFilter Filter;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }

        

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense();
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }

        private void Frm_OnSave()
        {
            RefreshForm();
        }

        void RefreshForm()
        {
           
           
            txtFilter.Clear();
            dgvDetainedLicensesList.DataSource = clsDetainedLicense.AllDetainedLicenses();
            lblRecordsCount.Text=dgvDetainedLicensesList.Rows.Count.ToString();
        }

        private void frmDetainedLicensesList_Load(object sender, EventArgs e)
        {
            RefreshForm();

        }

        private void tsmShowPersonDetails_Click(object sender, EventArgs e)
        {
            DataRow row = clsLicense.LicenseInfoRow(Convert.ToInt32(dgvDetainedLicensesList.CurrentRow.Cells["LicenseID"].Value));
            frmPersonDetails frm = new frmPersonDetails(clsDriver.GetPersonIDByDriverID(Convert.ToInt32(row["DriverID"])));
            frm.ShowDialog();
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            frmLicenseInfo frm = new frmLicenseInfo(Convert.ToInt32(dgvDetainedLicensesList.CurrentRow.Cells["LicenseID"].Value));
            frm.ShowDialog();
        }

        private void tsmShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            DataRow row = clsLicense.LicenseInfoRow(Convert.ToInt32(dgvDetainedLicensesList.CurrentRow.Cells["LicenseID"].Value));
            frmLicenseHistory frm = new frmLicenseHistory(clsDriver.GetPersonIDByDriverID(Convert.ToInt32(row["DriverID"])));
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool IsReleased = Convert.ToBoolean(dgvDetainedLicensesList.CurrentRow.Cells["IsReleased"].Value);
            if(!IsReleased)
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense(Convert.ToInt32(dgvDetainedLicensesList.CurrentRow.Cells["LicenseID"].Value));
            frm.OnSave += Frm_OnSave;
            frm.ShowDialog();
        }
        void DoFilterOnTxt()
        {
DataTable Clone=((DataTable)dgvDetainedLicensesList.DataSource).Clone();

if(string.IsNullOrWhiteSpace(txtFilter.Text))
            {
                RefreshForm();
                return;
            }


            switch(Filter)
            {
                case enFilter.eDetainID:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.FilterByDetainID(Convert.ToInt32(txtFilter.Text));
                        lblRecordsCount.Text=dgvDetainedLicensesList.Rows.Count.ToString();
                        if(dgvDetainedLicensesList.Rows.Count==0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }
                        break;
                    }
                case enFilter.eNationalNo:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.FilterByNationalNO(txtFilter.Text);
                        lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                        if (dgvDetainedLicensesList.Rows.Count == 0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }
                        break;
                    }
                case enFilter.eAppID:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.FilterByReleaseAppID(Convert.ToInt32(txtFilter.Text));
                        lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                        if (dgvDetainedLicensesList.Rows.Count == 0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }
                        break;
                    }
                case enFilter.eFullName:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.FilterByFullName(txtFilter.Text);
                        lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                        if (dgvDetainedLicensesList.Rows.Count == 0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }
                        break;
                    }
              
                    }





            }
        void DoFilterOnCb()
        {
            DataTable Clone = ((DataTable)dgvDetainedLicensesList.DataSource).Clone();
            switch(IsRelease)
            {
                case enIsReleas.eAll:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.AllDetainedLicenses();
                        lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                        if (dgvDetainedLicensesList.Rows.Count == 0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }
                      
                        break;
                    }
                case enIsReleas.eNo:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.FilterByIsReleased(false);
                        lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                        if (dgvDetainedLicensesList.Rows.Count == 0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }

                        break;
                    }
                case enIsReleas.eYes:
                    {
                        dgvDetainedLicensesList.DataSource = clsDetainedLicense.FilterByIsReleased(true);
                        lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
                        if (dgvDetainedLicensesList.Rows.Count == 0)
                        {
                            dgvDetainedLicensesList.DataSource = Clone;
                        }

                        break;
                    }



            }





        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            RefreshForm();
            Filter=(enFilter)cbFilter.SelectedIndex;
            if(Filter==enFilter.eNone)
            {
                txtFilter.Visible = false;
                cbIsReleased.Visible = false;
                return;
            }
            if(Filter==enFilter.eIsRelease)
            {
                cbIsReleased.SelectedIndex = 0;

                txtFilter.Visible = false;
                cbIsReleased.Visible = true;
                return;
            }
            
            txtFilter.Visible = true;
            cbIsReleased.Visible = false;


        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DoFilterOnTxt();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            IsRelease=(enIsReleas)cbIsReleased.SelectedIndex;
            DoFilterOnCb();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            { e.Handled = true; return; }
        }
    }
     
    






    }

