using DVLD.Applications.International_Driving_Licenses;
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

namespace DVLD.Licenses
{
    public partial class frmLicenseHistory : Form
    {
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            this.PersonID = PersonID;
          
        }
        int DriverID;
        int PersonID;
        public event Action OnEditPerson;

        void SetUpForm()
        {
            DriverID = clsDriver.GetDriverIDByPersonID(PersonID);
            ctrlFilterPerson1.gbFilters.Enabled = false;
            ctrlFilterPerson1.ctrlPersonInfo1.LoadPersonInfo(PersonID);
            ctrlFilterPerson1.cbFilterBy.SelectedIndex = 1;
            ctrlFilterPerson1.txtFilterValue.Text = PersonID.ToString();
            ctrlFilterPerson1.ctrlPersonInfo1.OnSave += CtrlPersonInfo1_OnSave;
            dgvLocalLicenses.DataSource = clsLicense.GetLicensesHistory(DriverID);
            lblLocalCount.Text = dgvLocalLicenses.Rows.Count.ToString();


            dgvInternationalLicenses.DataSource = clsInternationalDrivingLicenseApplications.GetDriverLicensesHistory(DriverID);
            lblInternationCount.Text = dgvInternationalLicenses.Rows.Count.ToString();
        }

        private void CtrlPersonInfo1_OnSave()
        {
            dgvLocalLicenses.DataSource = clsLicense.GetLicensesHistory(DriverID);
            OnEditPerson?.Invoke();
        }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            SetUpForm();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID=Convert.ToInt32(dgvLocalLicenses.CurrentRow.Cells[0].Value);
            frmLicenseInfo frm=new frmLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void tpInternational_Click(object sender, EventArgs e)
        {
           
        }

        private void tpLocal_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            int IntLecenseID = Convert.ToInt32(dgvInternationalLicenses.CurrentRow.Cells["Int.License ID"].Value);
            frmIntDLApplicationDetails frm = new frmIntDLApplicationDetails(IntLecenseID);
            frm.ShowDialog();
        }
    }
}
