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
using System.Web.Management;
using System.Windows.Forms;

namespace DVLD.Drivers
{
    public partial class frmDriversList : Form
    {
        public frmDriversList()
        {
            InitializeComponent();
        }
        enum enFilter { eNone,eDriverID,ePersonID,eNationalNo,eFullName}

        void RefreshFrom()
        {
            dgvDriversList.DataSource=clsDriver.GetAllDrivers();
            lblRecordsCount.Text=dgvDriversList.Rows.Count.ToString();
            txtFilter.Visible=false;
            cbFilter.SelectedIndex=0;
            txtFilter.Clear();
      
        }
        private void frmDriversList_Load(object sender, EventArgs e)
        {
            RefreshFrom();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void DoFiler(enFilter Filter)
        {
            if(string.IsNullOrWhiteSpace(txtFilter.Text))
            {
               dgvDriversList.DataSource = clsDriver.GetAllDrivers();
                lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();

                return;
            }

            DataTable Clone=((DataTable)dgvDriversList.DataSource).Clone();
            switch (Filter)
            {
                case enFilter.eDriverID:
                    {
                        dgvDriversList.DataSource = clsDriver.GetDriverByDriverID(Convert.ToInt32(txtFilter.Text));
                        lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();
                        if(dgvDriversList.Rows.Count==0)
                        {
                            dgvDriversList.DataSource = Clone;
                       
                        }
                        return;

                    }
                case enFilter.ePersonID:
                    {
                        dgvDriversList.DataSource = clsDriver.GetDriverByPersonID(Convert.ToInt32(txtFilter.Text));
                        lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();

                        if (dgvDriversList.Rows.Count == 0)
                        {
                            dgvDriversList.DataSource = Clone;

                        }
                        return;
                    }
                case enFilter.eNationalNo:
                    {
                        dgvDriversList.DataSource = clsDriver.GetDriverByNationalNo(Convert.ToString(txtFilter.Text));
                        lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();

                        if (dgvDriversList.Rows.Count == 0)
                        {
                            dgvDriversList.DataSource = Clone;

                        }
                        return;
                    }

                case enFilter.eFullName:
                    {
                        dgvDriversList.DataSource = clsDriver.GetDriverByFullName(Convert.ToString(txtFilter.Text));
                        lblRecordsCount.Text = dgvDriversList.Rows.Count.ToString();

                        if (dgvDriversList.Rows.Count == 0)
                        {
                            dgvDriversList.DataSource = Clone;

                        }
                        return;
                    }
                default:
                    {
                        RefreshFrom();
                        break;
                    }


            }




        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilter.SelectedIndex == 0)
            {
                RefreshFrom();
                return;
            }
            txtFilter.Visible = true;
            txtFilter.Clear();

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DoFiler((enFilter)cbFilter.SelectedIndex);
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            enFilter filter = (enFilter)cbFilter.SelectedIndex;
            if (filter == enFilter.ePersonID||filter==enFilter.eDriverID)
            {
                if(!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled= false;
                }






            }
            else
            {
                e.Handled= false;
            }


        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvDriversList.CurrentRow.Cells["PersonID"].Value);
            frmPersonDetails frm = new frmPersonDetails(PersonID);
            frm.ctrlPersonInfo1.OnSave += Frm_OnEditPerson;
            frm.ShowDialog();
        }

        
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvDriversList.CurrentRow.Cells["PersonID"].Value);
            frmLicenseHistory frm = new frmLicenseHistory(PersonID);
            frm.OnEditPerson += Frm_OnEditPerson;
            frm.ShowDialog();
        }

        private void Frm_OnEditPerson()
        {
            RefreshFrom();
        }
    }
}
